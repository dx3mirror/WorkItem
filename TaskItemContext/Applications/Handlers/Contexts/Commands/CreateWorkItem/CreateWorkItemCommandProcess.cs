using System.Diagnostics.CodeAnalysis;
using WorkItem.Application.AppServices.Contexts.WorkItems.Abstracts;
using WorkItem.Application.AppServices.Contexts.WorkItems.Models;
using WorkItem.Application.Handlers.Contexts.Commands.Abstracts;

namespace WorkItem.Application.Handlers.Contexts.Commands.CreateWorkItem
{
    /// <summary>
    /// Обработчик команды <see cref="CreateWorkItemCommand"/>.
    /// </summary>
    /// <remarks>
    /// Конструктор класса <see cref="CreateWorkItemCommandProcess"/>.
    /// </remarks>
    /// <param name="workItemService"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public sealed class CreateWorkItemCommandProcess([NotNull] IWorkItemService workItemService) : ICommandProcess<CreateWorkItemCommand>
    {
        private readonly IWorkItemService _workItemService = workItemService ?? throw new ArgumentNullException(nameof(workItemService));

        /// <inheritdoc/>
        public async Task Handle(CreateWorkItemCommand command, CancellationToken cancellationToken)
        {
            var request = new CreateWorkItemModel(command.UserId);
            await _workItemService.CreateAsync(request, cancellationToken);
        }
    }
}
