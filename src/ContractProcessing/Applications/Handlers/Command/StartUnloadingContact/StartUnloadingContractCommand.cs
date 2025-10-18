namespace Warehouse.ContractProcessing.Applications.Handlers.Command.StartUnloadingContact
{
    /// <summary>
    /// Команда на перевод договора разгрузки в статус «В работе» (начало разгрузки).
    /// </summary>
    /// <remarks>
    /// Создаёт новый экземпляр команды на запуск договора разгрузки.
    /// </remarks>
    /// <param name="contractId">Идентификатор договора разгрузки.</param>
    public sealed class StartUnloadingContractCommand(Guid contractId)
    {
        /// <summary>
        /// Идентификатор договора разгрузки.
        /// </summary>
        public Guid ContractId { get; } = contractId;
    }
}
