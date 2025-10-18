using MassTransit;
using System.Diagnostics.CodeAnalysis;
using Warehouse.ContractProcessing.Applications.AppServices.Abstract;
using Warehouse.ContractProcessing.Сontract.Events;

namespace Warehouse.ContractProcessing.Applications.Handlers.Command.CancelUnloadingContract
{
    /// <summary>
    /// Обработчик команды <see cref="CancelUnloadingContractCommand"/>.
    /// Отменяет договор разгрузки и публикует событие об этом.
    /// </summary>
    /// <remarks>
    /// Создаёт новый экземпляр обработчика команды отмены договора разгрузки.
    /// </remarks>
    /// <param name="service">Сервис управления договорами разгрузки.</param>
    /// <param name="publish">Публикатор события об отмене договора.</param>
    public sealed class CancelUnloadingContractCommandHandler(
        [NotNull] IUnloadingContractService service,
        [NotNull] ITopicProducer<UnloadingCancelledEvent> publish)
    {
        private readonly IUnloadingContractService _service = service ?? throw new ArgumentNullException(nameof(service));
        private readonly ITopicProducer<UnloadingCancelledEvent> _publish = publish ?? throw new ArgumentNullException(nameof(publish));

        /// <summary>
        /// Обрабатывает команду на отмену договора разгрузки.
        /// </summary>
        /// <param name="command">Команда на отмену договора.</param>
        /// <param name="ct">Токен отмены операции.</param>
        public async Task Handle(
            [NotNull] CancelUnloadingContractCommand command,
            [NotNull] CancellationToken ct)
        {
            await _service.CancelContractAsync(command.ContractId, ct);

            await _publish.Produce(new UnloadingCancelledEvent
            {
                ContractId = command.ContractId,
                Timestamp = DateTime.UtcNow
            }, ct);
        }
    }
}
