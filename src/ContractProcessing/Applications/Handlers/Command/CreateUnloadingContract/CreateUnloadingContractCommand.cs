namespace Warehouse.ContractProcessing.Applications.Handlers.Command.CreateUnloadingContract
{
    /// <summary>
    /// Команда на создание нового договора разгрузки.
    /// </summary>
    /// <remarks>
    /// Создаёт новый экземпляр команды на создание договора разгрузки.
    /// </remarks>
    /// <param name="contractGuid">Идентификатор договора разгрузки.</param>
    /// <param name="warehouseGuid">Идентификатор склада.</param>
    /// <param name="managerGuid">Идентификатор менеджера.</param>
    /// <param name="scheduledFor">Планируемая дата разгрузки.</param>
    public sealed class CreateUnloadingContractCommand(
        Guid contractGuid,
        Guid warehouseGuid,
        Guid managerGuid,
        DateTime scheduledFor)
    {
        /// <summary>
        /// Идентификатор договора разгрузки.
        /// </summary>
        public Guid ContractGuid { get; } = contractGuid;

        /// <summary>
        /// Идентификатор склада.
        /// </summary>
        public Guid WarehouseGuid { get; } = warehouseGuid;

        /// <summary>
        /// Идентификатор менеджера.
        /// </summary>
        public Guid ManagerGuid { get; } = managerGuid;

        /// <summary>
        /// Планируемая дата разгрузки.
        /// </summary>
        public DateTime ScheduledFor { get; } = scheduledFor;
    }
}
