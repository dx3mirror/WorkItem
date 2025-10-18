namespace Warehouse.ContractProcessing.Сontract.Request
{
    /// <summary>
    /// Запрос на добавление товарной позиции в договор разгрузки.
    /// </summary>
    public class AddUnloadingLineRequest
    {
        /// <summary>
        /// Идентификатор договора разгрузки.
        /// </summary>
        public Guid ContractId { get; set; }

        /// <summary>
        /// Идентификатор товара.
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Количество товара.
        /// </summary>
        public int Quantity { get; set; }
    }
}
