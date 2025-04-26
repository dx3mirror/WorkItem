using System.Diagnostics.CodeAnalysis;
using TaskExample.Domain.ValueObjects;
using WorkItem.Application.AppServices.Contexts.WorkItems.Abstracts;
using WorkItem.Application.AppServices.Contexts.WorkItems.Models;
using WorkItem.Application.Handlers.Contexts.Commands.Abstracts;

namespace WorkItem.Application.Handlers.Contexts.Commands.SetWorkItemTitle
{
    /// <summary>
    /// Обработчик команды <see cref="SetWorkItemTitleCommand"/>.
    /// </summary>
    public sealed class SetWorkItemTitleCommandProcess([NotNull] IWorkItemService workItemService)
        : ICommandProcess<SetWorkItemTitleCommand>
    {
        private readonly IWorkItemService _workItemService = workItemService ?? throw new ArgumentNullException(nameof(workItemService));

        /// <inheritdoc/>
        public async Task Handle(SetWorkItemTitleCommand command, CancellationToken cancellationToken)
        {
            var request = new SetWorkItemTitleModel(command.WorkItemId, new WorkItemTitle(command.Title));
            await _workItemService.SetTitleAsync(request, cancellationToken);
        }
    }
}
