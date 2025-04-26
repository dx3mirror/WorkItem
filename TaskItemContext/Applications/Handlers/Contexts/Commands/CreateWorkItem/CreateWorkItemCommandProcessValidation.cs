using FluentValidation;

namespace WorkItem.Application.Handlers.Contexts.Commands.CreateWorkItem
{
    /// <summary>
    /// Валидатор команды <see cref="CreateWorkItemCommand"/>.
    /// </summary>
    public sealed class CreateWorkItemCommandProcessValidation : AbstractValidator<CreateWorkItemCommand>
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CreateWorkItemCommandProcessValidation"/>.
        /// </summary>
        public CreateWorkItemCommandProcessValidation()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("UserId не может быть пустым.");
        }
    }
}
