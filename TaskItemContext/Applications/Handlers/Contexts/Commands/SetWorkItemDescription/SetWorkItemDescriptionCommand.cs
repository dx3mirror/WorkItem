using WorkItem.Application.Handlers.Contexts.Commands.Abstracts;

namespace WorkItem.Application.Handlers.Contexts.Commands.SetWorkItemDescription
{
    /// <summary>
    /// Команда на установку описания рабочего элемента.
    /// </summary>
    public sealed record SetWorkItemDescriptionCommand(Guid WorkItemId, string Description, Guid CorrelationId) : Command;
}
