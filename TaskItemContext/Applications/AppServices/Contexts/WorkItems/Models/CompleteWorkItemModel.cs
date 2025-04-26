namespace WorkItem.Application.AppServices.Contexts.WorkItems.Models
{
    /// <summary>
    /// Модель запроса на завершение рабочего элемента.
    /// </summary>
    public sealed record CompleteWorkItemModel(Guid WorkItemId, DateTime CompletedDate);
}
