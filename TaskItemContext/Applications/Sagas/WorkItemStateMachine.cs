using MassTransit;
using WorkItem.Application.Handlers.Contexts.Commands.CompleteWorkItem;
using WorkItem.Application.Handlers.Contexts.Commands.CreateWorkItem;
using WorkItem.Application.Handlers.Contexts.Commands.SetWorkItemDescription;
using WorkItem.Application.Handlers.Contexts.Commands.SetWorkItemTitle;
using WorkItemContext.Infrastructures.Sagas;

namespace WorkItem.Application.Sagas
{
    /// <summary>
    /// Сага для управления жизненным циклом рабочего элемента.
    /// Осуществляет обработку команд создания, установки заголовка, описания и завершения рабочего элемента.
    /// </summary>
    public class WorkItemStateMachine : MassTransitStateMachine<WorkItemState>
    {
        /// <summary>
        /// Состояние после создания рабочего элемента.
        /// </summary>
        public State Created { get; private set; } = null!;

        /// <summary>
        /// Состояние после установки заголовка рабочего элемента.
        /// </summary>
        public State TitleSet { get; private set; } = null!;

        /// <summary>
        /// Состояние после установки описания рабочего элемента.
        /// </summary>
        public State DescriptionSet { get; private set; } = null!;

        /// <summary>
        /// Состояние после завершения рабочего элемента.
        /// </summary>
        public State Completed { get; private set; } = null!;

        /// <summary>
        /// Событие создания рабочего элемента.
        /// </summary>
        public Event<CreateWorkItemCommand> CreateWorkItem { get; private set; } = null!;

        /// <summary>
        /// Событие установки заголовка рабочего элемента.
        /// </summary>
        public Event<SetWorkItemTitleCommand> SetTitle { get; private set; } = null!;

        /// <summary>
        /// Событие установки описания рабочего элемента.
        /// </summary>
        public Event<SetWorkItemDescriptionCommand> SetDescription { get; private set; } = null!;

        /// <summary>
        /// Событие завершения рабочего элемента.
        /// </summary>
        public Event<CompleteWorkItemCommand> Complete { get; private set; } = null!;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="WorkItemStateMachine"/>.
        /// Определяет переходы состояний в зависимости от входящих событий.
        /// </summary>
        public WorkItemStateMachine()
        {
            InstanceState(x => x.CurrentState);

            Event(() => CreateWorkItem, x => x.CorrelateById(context => context.Message.CorrelationId));
            Event(() => SetTitle, x => x.CorrelateById(m => m.Message.CorrelationId));
            Event(() => SetDescription, x => x.CorrelateById(m => m.Message.CorrelationId));
            Event(() => Complete, x => x.CorrelateById(m => m.Message.CorrelationId));

            Initially(
                When(CreateWorkItem)
                    .Then(context =>
                    {
                        context.Saga.UserId = context.Message.UserId;
                    })
                    .TransitionTo(Created)
            );

            During(Created,
                When(SetTitle)
                    .Then(context =>
                    {
                        context.Saga.Title = context.Message.Title;
                    })
                    .TransitionTo(TitleSet)
            );

            During(TitleSet,
                When(SetDescription)
                    .Then(context =>
                    {
                        context.Saga.Description = context.Message.Description;
                    })
                    .TransitionTo(DescriptionSet)
            );

            During(DescriptionSet,
                When(Complete)
                    .Then(context =>
                    {
                        context.Saga.CompletedDate = context.Message.CompletedDate;
                    })
                    .TransitionTo(Completed)
                    .Finalize()
            );

            SetCompletedWhenFinalized();
        }
    }
}
