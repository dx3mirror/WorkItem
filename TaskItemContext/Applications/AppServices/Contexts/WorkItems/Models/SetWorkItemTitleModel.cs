using TaskExample.Domain.ValueObjects;

namespace WorkItem.Application.AppServices.Contexts.WorkItems.Models
{
    /// <summary>
    /// Модель запроса на установку заголовка рабочего элемента.
    /// </summary>
    public sealed record SetWorkItemTitleModel(Guid WorkItemId, WorkItemTitle Title);
}
