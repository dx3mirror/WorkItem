namespace Warehouse.ContractProcessing.Сontract.Events
{
    /// <summary>
    /// Событие, публикуемое при старте разгрузки по договору.
    /// </summary>
    public class UnloadingStartedEvent
    {
        /// <summary>
        /// Идентификатор договора разгрузки.
        /// </summary>
        public Guid ContractId { get; init; }

        /// <summary>
        /// Идентификатор склада, в который осуществляется разгрузка.
        /// </summary>
        public Guid WarehouseId { get; init; }

        /// <summary>
        /// Идентификатор менеджера, ответственного за разгрузку.
        /// </summary>
        public Guid ManagerId { get; init; }

        /// <summary>
        /// Планируемая дата и время разгрузки.
        /// </summary>
        public DateTime ScheduledFor { get; init; }

        /// <summary>
        /// Количество товарных позиций в договоре.
        /// </summary>
        public int LinesCount { get; init; }

        /// <summary>
        /// Момент времени, когда была запущена разгрузка (UTC).
        /// </summary>
        public DateTime Timestamp { get; init; } = DateTime.UtcNow;
    }
}
