namespace Warehouse.ContractProcessing.Сontract.Events
{
    /// <summary>
    /// Событие, публикуемое при завершении договора разгрузки.
    /// </summary>
    public class UnloadingCompletedEvent
    {
        /// <summary>
        /// Идентификатор завершённого договора разгрузки.
        /// </summary>
        public Guid ContractId { get; init; }

        /// <summary>
        /// Момент времени, когда договор был завершён (UTC).
        /// </summary>
        public DateTime Timestamp { get; init; } = DateTime.UtcNow;
    }
}
