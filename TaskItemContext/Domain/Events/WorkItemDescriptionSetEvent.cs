using TaskExample.Domain.Events.Abstracts;

namespace TaskExample.Domain.Events
{
    /// <summary>
    /// Событие, возникающее при установке описания рабочего элемента.
    /// </summary>
    public class WorkItemDescriptionSetEvent(Guid workItemId, string description) : IDomainEvent
    {
        /// <summary>
        /// Идентификатор рабочего элемента.
        /// </summary>
        public Guid WorkItemId { get; } = workItemId;

        /// <summary>
        /// Установленное описание рабочего элемента.
        /// </summary>
        public string Description { get; } = description;
    }
}
