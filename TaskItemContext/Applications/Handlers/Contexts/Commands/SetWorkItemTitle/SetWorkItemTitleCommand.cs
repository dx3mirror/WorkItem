using WorkItem.Application.Handlers.Contexts.Commands.Abstracts;

namespace WorkItem.Application.Handlers.Contexts.Commands.SetWorkItemTitle
{
    /// <summary>
    /// Команда на установку заголовка рабочего элемента.
    /// </summary>
    public sealed record SetWorkItemTitleCommand(Guid WorkItemId, string Title): Command;
}
