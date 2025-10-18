namespace Warehouse.ContractProcessing.Сontract.Request
{
    /// <summary>
    /// Запрос на создание нового договора разгрузки.
    /// </summary>
    public class CreateUnloadingContractRequest
    {
        /// <summary>
        /// Идентификатор договора разгрузки.
        /// </summary>
        public Guid ContractGuid { get; set; }

        /// <summary>
        /// Идентификатор склада.
        /// </summary>
        public Guid WarehouseGuid { get; set; }

        /// <summary>
        /// Идентификатор менеджера.
        /// </summary>
        public Guid ManagerGuid { get; set; }

        /// <summary>
        /// Планируемая дата и время разгрузки.
        /// </summary>
        public DateTime ScheduledFor { get; set; }
    }
}
