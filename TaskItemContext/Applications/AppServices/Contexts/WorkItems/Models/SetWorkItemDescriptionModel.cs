using WorkItem.Domain.ValueObjects;

namespace WorkItem.Application.AppServices.Contexts.WorkItems.Models
{
    /// <summary>
    /// Модель запроса на установку описания рабочего элемента.
    /// </summary>
    public sealed record SetWorkItemDescriptionModel(Guid WorkItemId, WorkItemDescription Description);
}
