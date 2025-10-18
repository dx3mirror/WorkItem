using MassTransit;
using Warehouse.ContractProcessing.Сontract.Events;

namespace Warehouse.ContractProcessing.ConsumerServices.Consumers
{
    /// <summary>
    /// Консьюмер события <see cref="UnloadingCompletedEvent"/>.
    /// Обрабатывает сообщение о завершении договора разгрузки.
    /// </summary>
    public class UnloadingCompletedEventConsumer : IConsumer<UnloadingCompletedEvent>
    {
        /// <summary>
        /// Обрабатывает событие завершения договора разгрузки.
        /// </summary>
        /// <param name="context">Контекст сообщения, содержащий данные о событии.</param>
        public Task Consume(ConsumeContext<UnloadingCompletedEvent> context)
        {
            Console.WriteLine($"[COMPLETED] Contract {context.Message.ContractId} completed at {context.Message.Timestamp}");
            return Task.CompletedTask;
        }
    }
}
