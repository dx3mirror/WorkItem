using MassTransit;

namespace Warehouse.ContractProcessing.Infrastructures.Sagas
{
    /// <summary>
    /// Состояние саги для отслеживания жизненного цикла договора разгрузки.
    /// </summary>
    public class UnloadingSagaState : SagaStateMachineInstance
    {
        /// <summary>
        /// Идентификатор корреляции саги (используется для сопоставления событий).
        /// </summary>
        public Guid CorrelationId { get; set; }

        /// <summary>
        /// Текущее состояние договора разгрузки.
        /// </summary>
        public string CurrentState { get; set; } = default!;

        /// <summary>
        /// Дата и время создания договора.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Дата и время начала разгрузки (если началась).
        /// </summary>
        public DateTime? StartedAt { get; set; }

        /// <summary>
        /// Дата и время завершения разгрузки (если завершена).
        /// </summary>
        public DateTime? CompletedAt { get; set; }

        /// <summary>
        /// Дата и время отмены договора (если отменён).
        /// </summary>
        public DateTime? CancelledAt { get; set; }

        /// <summary>
        /// Сообщение об ошибке (если произошла ошибка).
        /// </summary>
        public string? Error { get; set; }

        /// <summary>
        /// Дата и время возникновения ошибки (если произошла).
        /// </summary>
        public DateTime? FailedAt { get; set; }

        /// <summary>
        /// Идентификатор склада.
        /// </summary>
        public Guid WarehouseId { get; set; }

        /// <summary>
        /// Идентификатор менеджера.
        /// </summary>
        public Guid ManagerId { get; set; }

        /// <summary>
        /// Запланированная дата и время разгрузки.
        /// </summary>
        public DateTime ScheduledFor { get; set; }

        /// <summary>
        /// Количество товарных позиций в договоре.
        /// </summary>
        public int LinesCount { get; set; }
    }
}
