using MassTransit;
using System.Diagnostics.CodeAnalysis;
using Warehouse.ContractProcessing.Applications.AppServices.Abstract;
using Warehouse.ContractProcessing.Сontract.Events;

namespace Warehouse.ContractProcessing.Applications.Handlers.Command.CompleteUnloadingContract
{
    /// <summary>
    /// Обработчик команды <see cref="CompleteUnloadingContractCommand"/>.
    /// Завершает договор разгрузки и публикует событие об этом.
    /// </summary>
    /// <remarks>
    /// Создаёт новый экземпляр обработчика команды завершения договора разгрузки.
    /// </remarks>
    /// <param name="service">Сервис управления договорами разгрузки.</param>
    /// <param name="publish">Публикатор события о завершении договора.</param>
    public sealed class CompleteUnloadingContractCommandHandler(
        [NotNull] IUnloadingContractService service,
        [NotNull] ITopicProducer<UnloadingCompletedEvent> publish)
    {
        private readonly IUnloadingContractService _service = service ?? throw new ArgumentNullException(nameof(service));
        private readonly ITopicProducer<UnloadingCompletedEvent> _publish = publish ?? throw new ArgumentNullException(nameof(publish));

        /// <summary>
        /// Обрабатывает команду на завершение договора разгрузки.
        /// </summary>
        /// <param name="command">Команда на завершение договора.</param>
        /// <param name="ct">Токен отмены операции.</param>
        public async Task Handle(
            [NotNull] CompleteUnloadingContractCommand command,
            [NotNull] CancellationToken ct)
        {
            await _service.CompleteContractAsync(command.ContractId, ct);

            await _publish.Produce(new UnloadingCompletedEvent
            {
                ContractId = command.ContractId,
                Timestamp = DateTime.UtcNow
            }, ct);
        }
    }
}
