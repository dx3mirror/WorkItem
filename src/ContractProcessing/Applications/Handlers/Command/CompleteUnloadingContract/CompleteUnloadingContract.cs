namespace Warehouse.ContractProcessing.Applications.Handlers.Command.CompleteUnloadingContract
{
    /// <summary>
    /// Команда на завершение договора разгрузки.
    /// </summary>
    public sealed class CompleteUnloadingContractCommand
    {
        /// <summary>
        /// Идентификатор договора разгрузки.
        /// </summary>
        public Guid ContractId { get; }

        /// <summary>
        /// Создаёт новый экземпляр команды на завершение договора разгрузки.
        /// </summary>
        /// <param name="contractId">Идентификатор договора разгрузки.</param>
        public CompleteUnloadingContractCommand(Guid contractId)
        {
            ContractId = contractId;
        }
    }
}
