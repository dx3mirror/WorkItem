namespace Warehouse.ContractProcessing.Сontract.Events
{
    /// <summary>
    /// Событие, публикуемое при возникновении ошибки в процессе разгрузки.
    /// </summary>
    public class UnloadingErrorEvent
    {
        /// <summary>
        /// Идентификатор договора разгрузки, в котором произошла ошибка.
        /// </summary>
        public Guid ContractId { get; set; }

        /// <summary>
        /// Краткое описание ошибки.
        /// </summary>
        public string Error { get; set; } = null!;

        /// <summary>
        /// Момент времени, когда произошла ошибка (UTC).
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Дополнительная информация о причине ошибки (опционально).
        /// </summary>
        public string? Reason { get; set; }
    }
}
