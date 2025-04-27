using WorkItem.Domain.Entities;
using WorkItem.Domain.Enums;
using WorkItem.Domain.Events;
using WorkItem.Domain.Events.Abstracts;
using WorkItem.Domain.Exceptions;
using WorkItem.Domain.Utilities;
using WorkItem.Domain.ValueObjects;

namespace WorkItem.Domain.Aggregates
{
    /// <summary>
    /// Агрегатный корень, представляющий рабочий элемент.
    /// </summary>
    public class WorkItemEffect
    {
        private readonly List<IDomainEvent> _domainEvents = [];
        private readonly List<WorkItemComment> _comments = [];

        /// <summary>
        /// Идентификатор рабочего элемента.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Идентификатор пользователя, создавшего рабочий элемент.
        /// </summary>
        public Guid UserId { get; private set; }

        /// <summary>
        /// Дата создания рабочего элемента.
        /// </summary>
        public DateTime CreationDate { get; private set; }

        /// <summary>
        /// Заголовок рабочего элемента.
        /// </summary>
        public WorkItemTitle? Title { get; private set; }

        /// <summary>
        /// Описание рабочего элемента.
        /// </summary>
        public WorkItemDescription? Description { get; private set; }

        /// <summary>
        /// Дата завершения рабочего элемента.
        /// </summary>
        public DateTime? CompletedDate { get; private set; }

        /// <summary>
        /// Статус рабочего элемента.
        /// </summary>
        public WorkItemStatus Status { get; private set; }

        /// <summary>
        /// Коллекция комментариев к рабочему элементу.
        /// </summary>
        public IReadOnlyCollection<WorkItemComment> Comments => _comments.AsReadOnly();

        /// <summary>
        /// Коллекция доменных событий, связанных с рабочим элементом.
        /// </summary>
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        /// <summary>
        /// Конструктор для EF Core.
        /// </summary>
        protected WorkItemEffect() { }

        /// <summary>
        /// Создаёт новый рабочий элемент для указанного пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <exception cref="DomainValidationException">Если <paramref name="userId"/> пустой.</exception>
        public WorkItemEffect(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new DomainValidationException(nameof(userId), "UserId cannot be empty.");

            Id = IdGenerator.NewGuid();
            UserId = userId;
            CreationDate = SystemClock.UtcNow();
            Status = WorkItemStatus.New;
        }

        /// <summary>
        /// Устанавливает заголовок рабочего элемента.
        /// </summary>
        /// <param name="title">Заголовок.</param>
        /// <exception cref="DomainValidationException">Если <paramref name="title"/> равен null.</exception>
        public void SetTitle(WorkItemTitle title)
        {
            if (title is null)
                throw new DomainValidationException(nameof(title), "Title cannot be null.");

            Title = title;
            Status = WorkItemStatus.SetTitle;
            AddDomainEvent(new WorkItemTitleSetEvent(Id, Title.Value));
        }

        /// <summary>
        /// Устанавливает описание рабочего элемента.
        /// </summary>
        /// <param name="description">Описание.</param>
        /// <exception cref="DomainValidationException">Если <paramref name="description"/> равен null.</exception>
        public void SetDescription(WorkItemDescription description)
        {
            if (description is null)
                throw new DomainValidationException(nameof(description), "Description cannot be null.");

            Description = description;
            Status = WorkItemStatus.SetDescription;
            AddDomainEvent(new WorkItemDescriptionSetEvent(Id, Description.Value));
        }

        /// <summary>
        /// Добавляет комментарий к рабочему элементу.
        /// </summary>
        /// <param name="comment">Комментарий.</param>
        /// <exception cref="DomainValidationException">Если <paramref name="comment"/> равен null.</exception>
        public void AddComment(WorkItemComment comment)
        {
            if (comment is null)
                throw new DomainValidationException(nameof(comment), "Comment cannot be null.");

            _comments.Add(comment);
        }

        /// <summary>
        /// Завершает рабочий элемент.
        /// </summary>
        /// <param name="completedDate">Дата завершения.</param>
        /// <exception cref="DomainValidationException">Если <paramref name="completedDate"/> некорректная.</exception>
        public void Complete(DateTime completedDate)
        {
            if (completedDate == default)
                throw new DomainValidationException(nameof(completedDate), "Completed date must be a valid date.");

            CompletedDate = completedDate;
            Status = WorkItemStatus.SetCompletedData;
            AddDomainEvent(new WorkItemCompletedEvent(Id, CompletedDate.Value));
        }

        /// <summary>
        /// Очищает все доменные события.
        /// </summary>
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        /// <summary>
        /// Добавляет доменное событие в коллекцию событий.
        /// </summary>
        /// <param name="domainEvent">Доменное событие.</param>
        private void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}

