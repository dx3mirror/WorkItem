using System.Diagnostics.CodeAnalysis;
using TaskExample.Domain.ValueObjects;
using WorkItem.Application.AppServices.Contexts.WorkItems.Abstracts;
using WorkItem.Application.AppServices.Contexts.WorkItems.Models;
using WorkItem.Application.Handlers.Contexts.Commands.Abstracts;

namespace WorkItem.Application.Handlers.Contexts.Commands.SetWorkItemDescription
{
    /// <summary>
    /// Обработчик команды <see cref="SetWorkItemDescriptionCommand"/>.
    /// </summary>
    public sealed class SetWorkItemDescriptionCommandProcess([NotNull]IWorkItemService workItemService)
        : ICommandProcess<SetWorkItemDescriptionCommand>
    {
        private readonly IWorkItemService _workItemService = workItemService ?? throw new ArgumentNullException(nameof(workItemService));

        /// <inheritdoc/>
        public async Task Handle(SetWorkItemDescriptionCommand command, CancellationToken cancellationToken)
        {
            var request = new SetWorkItemDescriptionModel(command.WorkItemId, new WorkItemDescription(command.Description));
            await _workItemService.SetDescriptionAsync(request, cancellationToken);
        }
    }
}
