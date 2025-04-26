using FluentValidation;
using WorkItem.Application.Handlers.Contexts.Commands.CreateWorkItem;

namespace WorkItem.Application.Handlers.Contexts.Commands.CompleteWorkItem
{
    /// <summary>
    /// Валидация команды <see cref="CompleteWorkItemCommand"/>.
    /// </summary>
    public sealed class CompleteWorkItemCommandProcessValidation : AbstractValidator<CompleteWorkItemCommand>
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CompleteWorkItemCommandProcessValidation"/>.
        /// </summary>
        public CompleteWorkItemCommandProcessValidation()
        {
            RuleFor(x => x.WorkItemId)
                .NotEmpty()
                .WithMessage("WorkItemId не может быть пустым.");

            RuleFor(x => x.CompletedDate)
                .NotEmpty()
                .WithMessage("CompletedDate не может быть пустым.")
                .Must(date => date != default)
                .WithMessage("CompletedDate должен быть валидной датой.");

            RuleFor(x => x.CorrelationId)
                .NotEmpty()
                .WithMessage("CorrelationId не может быть пустым.");
        }
    }
}
