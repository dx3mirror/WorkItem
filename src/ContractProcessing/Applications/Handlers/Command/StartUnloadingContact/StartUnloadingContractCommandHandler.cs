using MassTransit;
using System.Diagnostics.CodeAnalysis;
using Warehouse.ContractProcessing.Applications.AppServices.Abstract;
using Warehouse.ContractProcessing.Сontract.Events;

namespace Warehouse.ContractProcessing.Applications.Handlers.Command.StartUnloadingContact
{
    /// <summary>
    /// Обработчик команды <see cref="StartUnloadingContractCommand"/>.
    /// Переводит договор разгрузки в статус «В работе» и публикует событие об этом.
    /// </summary>
    /// <remarks>
    /// Создаёт новый экземпляр обработчика команды запуска договора разгрузки.
    /// </remarks>
    /// <param name="service">Сервис управления договорами разгрузки.</param>
    /// <param name="producer">Публикатор события о начале разгрузки.</param>
    public sealed class StartUnloadingContractCommandHandler(
        [NotNull] IUnloadingContractService service,
        [NotNull] ITopicProducer<UnloadingStartedEvent> producer)
    {
        private readonly IUnloadingContractService _service = service ?? throw new ArgumentNullException(nameof(service));
        private readonly ITopicProducer<UnloadingStartedEvent> _producer = producer ?? throw new ArgumentNullException(nameof(producer));

        /// <summary>
        /// Обрабатывает команду на запуск договора разгрузки.
        /// </summary>
        /// <param name="command">Команда на запуск договора разгрузки.</param>
        /// <param name="ct">Токен отмены операции.</param>
        public async Task Handle(
            [NotNull] StartUnloadingContractCommand command,
            [NotNull] CancellationToken ct)
        {
            await _service.StartContractAsync(command.ContractId, ct);

            var contract = await _service.GetContractSnapshotAsync(command.ContractId, ct);

            await _producer.Produce(new UnloadingStartedEvent
            {
                ContractId = contract.Id.Value,
                WarehouseId = contract.Warehouse.Value,
                ManagerId = contract.Manager.Value,
                ScheduledFor = contract.ScheduledFor.Value,
                LinesCount = contract.Lines.Count
            }, ct);
        }
    }
}
