using WorkItem.Application.Handlers.Contexts.Commands.Abstracts;

namespace WorkItem.Application.Handlers.Contexts.Commands.CreateWorkItem
{
    /// <summary>
    /// Запрос на создание рабочего элемента.
    /// </summary>
    public sealed record CreateWorkItemCommand (Guid UserId) : Command;
}
