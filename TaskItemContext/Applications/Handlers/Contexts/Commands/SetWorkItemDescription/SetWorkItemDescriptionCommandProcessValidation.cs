using FluentValidation;

namespace WorkItem.Application.Handlers.Contexts.Commands.SetWorkItemDescription
{
    /// <summary>
    /// Валидация команды <see cref="SetWorkItemDescriptionCommand"/>.
    /// </summary>
    public sealed class SetWorkItemDescriptionCommandProcessValidation : AbstractValidator<SetWorkItemDescriptionCommand>
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="SetWorkItemDescriptionCommandProcessValidation"/>.
        /// </summary>
        public SetWorkItemDescriptionCommandProcessValidation()
        {
            RuleFor(x => x.WorkItemId)
                .NotEmpty()
                .WithMessage("WorkItemId не может быть пустым.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description не может быть пустым.")
                .MaximumLength(2000)
                .WithMessage("Description не должен превышать 2000 символов.");

            RuleFor(x => x.CorrelationId)
                .NotEmpty()
                .WithMessage("CorrelationId не может быть пустым.");
        }
    }
}
