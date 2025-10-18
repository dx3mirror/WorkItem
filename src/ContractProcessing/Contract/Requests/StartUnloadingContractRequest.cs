namespace Warehouse.ContractProcessing.Сontract.Request
{
    /// <summary>
    /// Запрос на запуск договора разгрузки.
    /// </summary>
    public class StartUnloadingContractRequest
    {
        /// <summary>
        /// Идентификатор договора разгрузки.
        /// </summary>
        public Guid ContractId { get; set; }
    }
}
