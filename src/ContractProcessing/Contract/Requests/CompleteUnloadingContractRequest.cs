namespace Warehouse.ContractProcessing.Сontract.Request
{
    /// <summary>
    /// Запрос на завершение договора разгрузки.
    /// </summary>
    public class CompleteUnloadingContractRequest
    {
        /// <summary>
        /// Идентификатор договора разгрузки.
        /// </summary>
        public Guid ContractId { get; set; }
    }
}
