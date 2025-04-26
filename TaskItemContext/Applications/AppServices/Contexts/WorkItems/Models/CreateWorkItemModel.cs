namespace WorkItem.Application.AppServices.Contexts.WorkItems.Models
{
    /// <summary>
    /// Модель запроса на создание рабочего элемента.
    /// </summary>
    public sealed record CreateWorkItemModel(Guid UserId);
}
