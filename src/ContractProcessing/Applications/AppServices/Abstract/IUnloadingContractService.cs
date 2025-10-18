using System.Diagnostics.CodeAnalysis;
using Warehouse.ContractProcessing.Domain.Aggregates;

namespace Warehouse.ContractProcessing.Applications.AppServices.Abstract
{
    /// <summary>
    /// Сервис управления договорами разгрузки.
    /// Предоставляет операции по созданию, запуску, завершению, отмене договоров,
    /// добавлению позиций в договор, а также получению актуального состояния договора.
    /// </summary>
    public interface IUnloadingContractService
    {
        /// <summary>
        /// Создаёт новый договор разгрузки.
        /// </summary>
        /// <param name="contractGuid">Идентификатор договора разгрузки.</param>
        /// <param name="warehouseGuid">Идентификатор склада, в который планируется разгрузка.</param>
        /// <param name="managerGuid">Идентификатор менеджера, ответственного за разгрузку.</param>
        /// <param name="scheduledFor">Планируемая дата и время выполнения разгрузки.</param>
        /// <param name="cancellationToken">Токен для отмены асинхронной операции.</param>
        /// <returns>Асинхронная задача без возвращаемого результата.</returns>
        Task CreateContractAsync(
            [NotNull] Guid contractGuid,
            [NotNull] Guid warehouseGuid,
            [NotNull] Guid managerGuid,
            [NotNull] DateTime scheduledFor,
            [NotNull] CancellationToken cancellationToken);

        /// <summary>
        /// Переводит договор в статус «В работе» (начало разгрузки).
        /// </summary>
        /// <param name="contractId">Идентификатор договора.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        Task StartContractAsync(
            [NotNull] Guid contractId,
            [NotNull] CancellationToken cancellationToken);

        /// <summary>
        /// Завершает договор (разгрузка выполнена).
        /// </summary>
        /// <param name="contractId">Идентификатор договора.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        Task CompleteContractAsync(
            [NotNull] Guid contractId,
            [NotNull] CancellationToken cancellationToken);

        /// <summary>
        /// Отменяет договор разгрузки.
        /// </summary>
        /// <param name="contractId">Идентификатор договора.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        Task CancelContractAsync(
            [NotNull] Guid contractId,
            [NotNull] CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет строку (позицию товара) в договор разгрузки.
        /// </summary>
        /// <param name="contractId">Идентификатор договора.</param>
        /// <param name="productId">Идентификатор товара.</param>
        /// <param name="quantity">Количество товара.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        Task AddLineToContractAsync(
            [NotNull] Guid contractId,
            [NotNull] Guid productId,
            [NotNull] int quantity,
            [NotNull] CancellationToken cancellationToken);

        /// <summary>
        /// Получает полную копию данных договора (снимок состояния).
        /// </summary>
        /// <param name="contractId">Идентификатор договора.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Снимок состояния договора разгрузки.</returns>
        [return: NotNull]
        Task<UnloadingContract> GetContractSnapshotAsync(
            [NotNull] Guid contractId,
            [NotNull] CancellationToken cancellationToken);
    }
}
