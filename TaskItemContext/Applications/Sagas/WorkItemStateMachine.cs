using MassTransit;
using WorkItem.Application.AppServices.Contexts.WorkItems.Messages;
using WorkItem.Application.Handlers.Contexts.Commands.CompleteWorkItem;
using WorkItem.Application.Handlers.Contexts.Commands.CreateWorkItem;
using WorkItem.Application.Handlers.Contexts.Commands.SetWorkItemDescription;
using WorkItem.Application.Handlers.Contexts.Commands.SetWorkItemTitle;
using WorkItemContext.Infrastructures.Sagas;

namespace WorkItem.Application.Sagas
{
    /// <summary>
    /// Сага для управления жизненным циклом рабочего элемента.
    /// Отслеживает команды создания, установки заголовка, описания и завершения.
    /// Реализует автоматическое завершение рабочего элемента по таймауту при отсутствии активности.
    /// </summary>
    public sealed class WorkItemStateMachine : MassTransitStateMachine<WorkItemState>
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
        /// Планировщик таймаута бездействия рабочего элемента.
        /// </summary>
        public Schedule<WorkItemState, WorkItemTimeoutExpired> TimeoutExpired { get; private set; } = null!;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="WorkItemStateMachine"/>.
        /// Определяет переходы между состояниями рабочего элемента и обработку таймаутов.
        /// </summary>
        public WorkItemStateMachine()
        {
            InstanceState(x => x.CurrentState);

            Event(() => CreateWorkItem, x => x.CorrelateById(context => context.Message.CorrelationId));
            Event(() => SetTitle, x => x.CorrelateById(context => context.Message.CorrelationId));
            Event(() => SetDescription, x => x.CorrelateById(context => context.Message.CorrelationId));
            Event(() => Complete, x => x.CorrelateById(context => context.Message.CorrelationId));

            Schedule(() => TimeoutExpired, saga => saga.TimeoutTokenId, cfg =>
            {
                cfg.Delay = TimeSpan.FromMinutes(30);
                cfg.Received = e => e.CorrelateById(context => context.Message.CorrelationId);
            });

            Initially(
                When(CreateWorkItem)
                    .Then(context => context.Saga.UserId = context.Message.UserId)
                    .Schedule(TimeoutExpired, context => new WorkItemTimeoutExpired(context.Saga.CorrelationId))
                    .TransitionTo(Created)
            );

            During(Created,
                When(SetTitle)
                    .Then(context => context.Saga.Title = context.Message.Title)
                    .ThenAsync(async context => await RescheduleTimeout(context))
                    .TransitionTo(TitleSet)
            );

            During(TitleSet,
                When(SetDescription)
                    .Then(context => context.Saga.Description = context.Message.Description)
                    .ThenAsync(async context => await RescheduleTimeout(context))
                    .TransitionTo(DescriptionSet)
            );

            During(DescriptionSet,
                When(Complete)
                    .Then(context => context.Saga.CompletedDate = context.Message.CompletedDate)
                    .ThenAsync(async context => await CancelTimeout(context))
                    .TransitionTo(Completed)
                    .Finalize()
            );

            DuringAny(
                When(TimeoutExpired.Received)
                    .ThenAsync(async context => await HandleTimeout(context))
                    .Finalize()
            );

            SetCompletedWhenFinalized();
        }

        /// <summary>
        /// Перепланирует таймер завершения рабочего элемента при активности пользователя.
        /// </summary>
        /// <param name="context">Контекст выполнения поведения саги.</param>
        private static async Task RescheduleTimeout(BehaviorContext<WorkItemState> context)
        {
            var scheduledMessage = await context.SchedulePublish(
                TimeSpan.FromMinutes(30),
                new WorkItemTimeoutExpired(context.Saga.CorrelationId));

            context.Saga.TimeoutTokenId = scheduledMessage.TokenId;
        }

        /// <summary>
        /// Отменяет активный таймер завершения рабочего элемента.
        /// </summary>
        /// <param name="context">Контекст выполнения поведения саги.</param>
        private static async Task CancelTimeout(BehaviorContext<WorkItemState> context)
        {
            if (context.Saga.TimeoutTokenId.HasValue)
            {
                var scheduler = context.GetPayload<IMessageScheduler>();
                var destinationAddress = new Uri($"queue:work-item-timeout-expired");

                await scheduler.CancelScheduledSend(destinationAddress, context.Saga.TimeoutTokenId.Value);
            }
        }

        /// <summary>
        /// Обрабатывает событие истечения времени ожидания активности пользователя.
        /// </summary>
        /// <param name="context">Контекст выполнения поведения саги.</param>
        private static Task HandleTimeout(BehaviorContext<WorkItemState> context)
        {
            // Здесь можно будет отправить уведомление или зафиксировать таймаут в логи.
            return Task.CompletedTask;
        }
    }
}




