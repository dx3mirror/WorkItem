using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using Utilities.DbContextSettings.Abstracts;
using WorkItem.Application.AppServices.Contexts.WorkItems.Abstracts;
using WorkItem.Application.AppServices.Contexts.WorkItems.Models;
using WorkItem.Domain.Aggregates;
using WorkItem.Domain.Exceptions;

namespace WorkItem.Application.AppServices.Contexts.WorkItems
{
    /// <inheritdoc/>
    /// <summary>
    /// Конструктор класса <see cref="WorkItemService"/>.
    /// </summary>
    /// <param name="workItemRepository"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public class WorkItemService([NotNull] IRepository<WorkItemEffect> workItemRepository) : IWorkItemService
    {
        private readonly IRepository<WorkItemEffect> _workItemRepository = workItemRepository
            ?? throw new ArgumentNullException(nameof(workItemRepository));

        /// <inheritdoc/>
        public async Task CreateAsync(CreateWorkItemModel request, CancellationToken cancellationToken)
        {
            var workItem = new WorkItemEffect(request.UserId);
            await _workItemRepository.AddAsync(workItem, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task SetTitleAsync(SetWorkItemTitleModel request, CancellationToken cancellationToken)
        {
            var workItem = await _workItemRepository.Where(t => t.Id == request.WorkItemId).SingleOrDefaultAsync(cancellationToken)
                ?? throw new EntityNotFoundException(nameof(WorkItemEffect), request.WorkItemId);

            workItem.SetTitle(request.Title);
            await _workItemRepository.UpdateAsync(workItem, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task SetDescriptionAsync(SetWorkItemDescriptionModel request, CancellationToken cancellationToken)
        {
            var workItem = await _workItemRepository.Where(t => t.Id == request.WorkItemId).SingleOrDefaultAsync(cancellationToken)
                ?? throw new EntityNotFoundException(nameof(WorkItemEffect), request.WorkItemId);

            workItem.SetDescription(request.Description);
            await _workItemRepository.UpdateAsync(workItem, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task CompleteAsync(CompleteWorkItemModel request, CancellationToken cancellationToken)
        {
            var workItem = await _workItemRepository.Where(t => t.Id == request.WorkItemId).SingleOrDefaultAsync(cancellationToken)
                ?? throw new EntityNotFoundException(nameof(WorkItemEffect), request.WorkItemId);

            workItem.Complete(request.CompletedDate);
            await _workItemRepository.UpdateAsync(workItem, cancellationToken);
        }
    }
}
