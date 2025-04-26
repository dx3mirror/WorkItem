using FluentValidation;

namespace WorkItem.Application.Handlers.Contexts.Commands.SetWorkItemTitle
{
    /// <summary>
    /// Валидация команды <see cref="SetWorkItemTitleCommand"/>.
    /// </summary>
    public sealed class SetWorkItemTitleCommandProcessValidation : AbstractValidator<SetWorkItemTitleCommand>
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="SetWorkItemTitleCommandProcessValidation"/>.
        /// </summary>
        public SetWorkItemTitleCommandProcessValidation()
        {
            RuleFor(x => x.WorkItemId)
                .NotEmpty()
                .WithMessage("WorkItemId не может быть пустым.");

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title не может быть пустым.")
                .MaximumLength(250)
                .WithMessage("Title не должен превышать 250 символов.");

            RuleFor(x => x.CorrelationId)
                .NotEmpty()
                .WithMessage("CorrelationId не может быть пустым.");
        }
    }
}
