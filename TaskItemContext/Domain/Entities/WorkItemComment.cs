using WorkItem.Domain.Exceptions;
using WorkItem.Domain.Utilities;

namespace WorkItem.Domain.Entities
{
    /// <summary>
    /// Комментарий к рабочему элементу.
    /// </summary>
    public class WorkItemComment
    {
        /// <summary>
        /// Идентификатор комментария.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Автор комментария.
        /// </summary>
        public string Author { get; private set; }

        /// <summary>
        /// Содержимое комментария.
        /// </summary>
        public string Content { get; private set; }

        /// <summary>
        /// Дата создания комментария.
        /// </summary>
        public DateTime CreatedAt { get; private set; }

        /// <summary>
        /// Конструктор для EF Core.
        /// </summary>
        protected WorkItemComment() { }

        /// <summary>
        /// Создаёт новый комментарий к рабочему элементу.
        /// </summary>
        /// <param name="author">Имя автора комментария.</param>
        /// <param name="content">Текст комментария.</param>
        /// <exception cref="DomainValidationException">Если <paramref name="author"/> или <paramref name="content"/> пустой или null.</exception>
        public WorkItemComment(string author, string content)
        {
            if (string.IsNullOrWhiteSpace(author))
                throw new DomainValidationException(nameof(author), "Author cannot be empty.");

            if (string.IsNullOrWhiteSpace(content))
                throw new DomainValidationException(nameof(content), "Content cannot be empty.");

            Id = IdGenerator.NewGuid();
            Author = author;
            Content = content;
            CreatedAt = SystemClock.UtcNow();
        }
    }
}
