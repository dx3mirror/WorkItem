namespace Warehouse.ContractProcessing.Domain.Enums
{
    /// <summary>
    /// Возможные статусы жизненного цикла контракта на выгрузку товаров.
    /// </summary>
    public enum ContractStatus
    {
        /// <summary>
        /// Контракт создан и ожидает начала выгрузки. Можно вносить изменения.
        /// </summary>
        Pending = 10,

        /// <summary>
        /// Выгрузка товаров по контракту в процессе выполнения.
        /// </summary>
        InProgress = 20,

        /// <summary>
        /// Выгрузка завершена успешно.
        /// </summary>
        Completed = 30,

        /// <summary>
        /// Контракт отменён и больше не будет выполняться.
        /// </summary>
        Cancelled = 40
    }
}
