namespace Warehouse.ContractProcessing.Сontract.Request
{
    /// <summary>
    /// Запрос на отмену договора разгрузки.
    /// </summary>
    public class CancelUnloadingContractRequest
    {
        /// <summary>
        /// Идентификатор договора разгрузки.
        /// </summary>
        public Guid ContractId { get; set; }
    }
}
