using TaskExample.Domain.Events.Abstracts;

namespace TaskExample.Domain.Events
{
    /// <summary>
    /// Событие, возникающее при завершении рабочего элемента.
    /// </summary>
    public class WorkItemCompletedEvent(Guid workItemId, DateTime completedDate) : IDomainEvent
    {
        /// <summary>
        /// Идентификатор рабочего элемента.
        /// </summary>
        public Guid WorkItemId { get; } = workItemId;

        /// <summary>
        /// Дата завершения рабочего элемента.
        /// </summary>
        public DateTime CompletedDate { get; } = completedDate;
    }
}
