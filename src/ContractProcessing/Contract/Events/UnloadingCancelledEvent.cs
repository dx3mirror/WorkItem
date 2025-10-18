namespace Warehouse.ContractProcessing.Сontract.Events
{
    /// <summary>
    /// Событие, публикуемое при отмене договора разгрузки.
    /// </summary>
    public class UnloadingCancelledEvent
    {
        /// <summary>
        /// Идентификатор отменённого договора разгрузки.
        /// </summary>
        public Guid ContractId { get; init; }

        /// <summary>
        /// Момент времени, когда договор был отменён (UTC).
        /// </summary>
        public DateTime Timestamp { get; init; } = DateTime.UtcNow;
    }
}
