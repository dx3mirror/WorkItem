using System.Diagnostics.CodeAnalysis;
using WorkItem.Application.AppServices.Contexts.WorkItems.Abstracts;
using WorkItem.Application.AppServices.Contexts.WorkItems.Models;
using WorkItem.Application.Handlers.Contexts.Commands.Abstracts;

namespace WorkItem.Application.Handlers.Contexts.Commands.CompleteWorkItem
{
    /// <summary>
    /// Обработчик команды <see cref="CompleteWorkItemCommand"/>.
    /// </summary>
    public sealed class CompleteWorkItemCommandProcess([NotNull]IWorkItemService workItemService)
        : ICommandProcess<CompleteWorkItemCommand>
    {
        private readonly IWorkItemService _workItemService = workItemService ?? throw new ArgumentNullException(nameof(workItemService));

        /// <inheritdoc/>
        public async Task Handle(CompleteWorkItemCommand command, CancellationToken cancellationToken)
        {
            var request = new CompleteWorkItemModel(command.WorkItemId, command.CompletedDate);
            await _workItemService.CompleteAsync(request, cancellationToken);
        }
    }
}
