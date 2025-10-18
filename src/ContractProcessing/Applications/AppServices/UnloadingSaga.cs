using MassTransit;
using System.Diagnostics.CodeAnalysis;
using Warehouse.ContractProcessing.Infrastructures.Sagas;
using Warehouse.ContractProcessing.Сontract.Events;

namespace Warehouse.ContractProcessing.Applications.AppServices
{
    /// <summary>
    /// Саговая машина состояния для отслеживания статуса контракта на выгрузку.
    /// </summary>
    public class UnloadingSaga : MassTransitStateMachine<UnloadingSagaState>
    {
        /// <summary>Состояние: контракт в процессе.</summary>
        [NotNull]
        [SuppressMessage("CodeQuality", "S1144:Unused private types or members", Justification = "Used by MassTransit via reflection")]
        public State? Started { get; private set; }

        /// <summary>Состояние: контракт завершен.</summary>
        [NotNull]
        [SuppressMessage("CodeQuality", "S1144:Unused private types or members", Justification = "Used by MassTransit via reflection")]
        public State? Completed { get; private set; }

        /// <summary>Состояние: контракт отменен.</summary>
        [NotNull]
        [SuppressMessage("CodeQuality", "S1144:Unused private types or members", Justification = "Used by MassTransit via reflection")]
        public State? Cancelled { get; private set; }

        /// <summary>Состояние: ошибка.</summary>
        [NotNull]
        [SuppressMessage("CodeQuality", "S1144:Unused private types or members", Justification = "Used by MassTransit via reflection")]
        public State? Failed { get; private set; }

        /// <summary>Событие старта.</summary>
        [NotNull]
        [SuppressMessage("CodeQuality", "S1144:Unused private types or members", Justification = "Used by MassTransit via reflection")]
        public Event<UnloadingStartedEvent>? StartEvent { get; private set; }

        /// <summary>Событие завершения.</summary>
        [NotNull]
        [SuppressMessage("CodeQuality", "S1144:Unused private types or members", Justification = "Used by MassTransit via reflection")]
        public Event<UnloadingCompletedEvent>? CompleteEvent { get; private set; }

        /// <summary>Событие отмены.</summary>
        [NotNull]
        [SuppressMessage("CodeQuality", "S1144:Unused private types or members", Justification = "Used by MassTransit via reflection")]
        public Event<UnloadingCancelledEvent>? CancelEvent { get; private set; }

        /// <summary>Событие ошибки.</summary>
        [NotNull]
        [SuppressMessage("CodeQuality", "S1144:Unused private types or members", Justification = "Used by MassTransit via reflection")]
        public Event<UnloadingErrorEvent>? ErrorEvent { get; private set; }

        /// <summary>
        /// Конструктор саги, описывающий поведение при различных событиях.
        /// </summary>
        public UnloadingSaga()
        {
            InstanceState(x => x.CurrentState);

            Event(() => StartEvent, x => x.CorrelateById(m => m.Message.ContractId));
            Event(() => CompleteEvent, x => x.CorrelateById(m => m.Message.ContractId));
            Event(() => CancelEvent, x => x.CorrelateById(m => m.Message.ContractId));
            Event(() => ErrorEvent, x => x.CorrelateById(m => m.Message.ContractId));

            Initially(
                When(ErrorEvent!)
                    .Then(ctx =>
                    {
                        ctx.Saga.CorrelationId = ctx.Message.ContractId;
                        ctx.Saga.Error = ctx.Message.Error;
                        ctx.Saga.FailedAt = ctx.Message.Timestamp;
                    })
                    .TransitionTo(Failed!), 
                When(StartEvent!)
                    .Then(ctx =>
                    {
                        ctx.Saga.CorrelationId = ctx.Message.ContractId;
                        ctx.Saga.CreatedAt = ctx.Message.Timestamp;
                        ctx.Saga.WarehouseId = ctx.Message.WarehouseId;
                        ctx.Saga.ManagerId = ctx.Message.ManagerId;
                        ctx.Saga.ScheduledFor = ctx.Message.ScheduledFor;
                        ctx.Saga.LinesCount = ctx.Message.LinesCount;
                        ctx.Saga.StartedAt = ctx.Message.Timestamp;
                    })
                    .TransitionTo(Started!)
            );
            During(Started!,
                When(CompleteEvent!)
                    .Then(ctx => ctx.Saga.CompletedAt = ctx.Message.Timestamp)
                    .TransitionTo(Completed!),

                When(CancelEvent!)
                    .Then(ctx => ctx.Saga.CancelledAt = ctx.Message.Timestamp)
                    .TransitionTo(Cancelled!)
            );

            During(Completed!,
                When(CancelEvent!)
                    .Then(ctx => ctx.Saga.CancelledAt = ctx.Message.Timestamp)
                    .TransitionTo(Cancelled!)
            );
        }
    }
}
