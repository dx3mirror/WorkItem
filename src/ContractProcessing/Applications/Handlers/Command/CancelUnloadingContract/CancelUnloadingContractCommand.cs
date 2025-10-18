namespace Warehouse.ContractProcessing.Applications.Handlers.Command.CancelUnloadingContract
{
    /// <summary>
    /// Команда на отмену договора разгрузки.
    /// </summary>
    /// <remarks>
    /// Создаёт новый экземпляр команды на отмену договора разгрузки.
    /// </remarks>
    /// <param name="contractId">Идентификатор договора разгрузки.</param>
    public sealed class CancelUnloadingContractCommand(Guid contractId)
    {
        /// <summary>
        /// Идентификатор договора разгрузки.
        /// </summary>
        public Guid ContractId { get; } = contractId;
    }
}
