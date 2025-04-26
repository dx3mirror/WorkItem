using TaskExample.Domain.Events.Abstracts;

namespace TaskExample.Domain.Events
{
    /// <summary>
    /// Событие, возникающее при установке заголовка рабочего элемента.
    /// </summary>
    public class WorkItemTitleSetEvent(Guid workItemId, string title) : IDomainEvent
    {
        /// <summary>
        /// Идентификатор рабочего элемента.
        /// </summary>
        public Guid WorkItemId { get; } = workItemId;

        /// <summary>
        /// Установленный заголовок рабочего элемента.
        /// </summary>
        public string Title { get; } = title;
    }
}
