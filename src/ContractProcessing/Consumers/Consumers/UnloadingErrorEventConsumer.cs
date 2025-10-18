using MassTransit;
using Warehouse.ContractProcessing.Сontract.Events;

namespace Warehouse.ContractProcessing.ConsumerServices.Consumers
{
    /// <summary>
    /// Консьюмер события <see cref="UnloadingErrorEvent"/>.
    /// Обрабатывает сообщение об ошибке в процессе разгрузки.
    /// </summary>
    public class UnloadingErrorEventConsumer : IConsumer<UnloadingErrorEvent>
    {
        /// <summary>
        /// Обрабатывает событие ошибки разгрузки.
        /// </summary>
        /// <param name="context">Контекст сообщения, содержащий данные о событии.</param>
        public Task Consume(ConsumeContext<UnloadingErrorEvent> context)
        {
            Console.WriteLine($"[ERROR] {context.Message.ContractId}: {context.Message.Error}");
            return Task.CompletedTask;
        }
    }
}
