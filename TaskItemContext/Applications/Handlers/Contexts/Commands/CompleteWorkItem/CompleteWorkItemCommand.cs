using WorkItem.Application.Handlers.Contexts.Commands.Abstracts;

namespace WorkItem.Application.Handlers.Contexts.Commands.CompleteWorkItem
{
    /// <summary>
    /// Команда на завершение рабочего элемента.
    /// </summary>
    public sealed record CompleteWorkItemCommand(Guid WorkItemId, DateTime CompletedDate) : Command;
}
