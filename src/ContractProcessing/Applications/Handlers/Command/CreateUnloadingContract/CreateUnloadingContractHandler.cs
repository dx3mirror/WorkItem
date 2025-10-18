using System.Diagnostics.CodeAnalysis;
using Warehouse.ContractProcessing.Applications.AppServices.Abstract;

namespace Warehouse.ContractProcessing.Applications.Handlers.Command.CreateUnloadingContract
{
    /// <summary>
    /// Обработчик команды <see cref="CreateUnloadingContractCommand"/>.
    /// Создаёт новый договор разгрузки.
    /// </summary>
    /// <remarks>
    /// Инициализирует новый экземпляр обработчика команды создания договора разгрузки.
    /// </remarks>
    /// <param name="service">Сервис управления договорами разгрузки.</param>
    public sealed class CreateUnloadingContractHandler([NotNull] IUnloadingContractService service)
    {
        private readonly IUnloadingContractService _service = service ?? throw new ArgumentNullException(nameof(service));

        /// <summary>
        /// Обрабатывает команду на создание договора разгрузки.
        /// </summary>
        /// <param name="command">Команда на создание договора разгрузки.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        public async Task Handle(
            [NotNull] CreateUnloadingContractCommand command,
            [NotNull] CancellationToken cancellationToken)
        {
            await _service.CreateContractAsync(
                command.ContractGuid,
                command.WarehouseGuid,
                command.ManagerGuid,
                command.ScheduledFor,
                cancellationToken);
        }
    }
}
