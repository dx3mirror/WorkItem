namespace WorkItem.Application.AppServices.Contexts.WorkItems.Messages
{
    /// <summary>
    /// Сообщение о таймауте рабочего элемента.
    /// </summary>
    public record WorkItemTimeoutExpired(Guid CorrelationId);
}
