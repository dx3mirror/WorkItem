namespace Warehouse.ContractProcessing.Applications.Handlers.Command.AddUnloadingLine
{
    /// <summary>
    /// Команда на добавление позиции (товарной строки) в договор разгрузки.
    /// </summary>
    /// <remarks>
    /// Создаёт новый экземпляр команды на добавление товарной строки в договор разгрузки.
    /// </remarks>
    /// <param name="contractId">Идентификатор договора разгрузки.</param>
    /// <param name="productId">Идентификатор товара.</param>
    /// <param name="quantity">Количество товара.</param>
    public sealed class AddUnloadingLineCommand(Guid contractId, Guid productId, int quantity)
    {
        /// <summary>
        /// Идентификатор договора разгрузки.
        /// </summary>
        public Guid ContractId { get; } = contractId;

        /// <summary>
        /// Идентификатор товара.
        /// </summary>
        public Guid ProductId { get; } = productId;

        /// <summary>
        /// Количество товара.
        /// </summary>
        public int Quantity { get; } = quantity;
    }
}
