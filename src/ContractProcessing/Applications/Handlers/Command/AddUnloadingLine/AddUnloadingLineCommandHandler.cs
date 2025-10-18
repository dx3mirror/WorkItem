using System.Diagnostics.CodeAnalysis;
using Warehouse.ContractProcessing.Applications.AppServices.Abstract;

namespace Warehouse.ContractProcessing.Applications.Handlers.Command.AddUnloadingLine
{
    /// <summary>
    /// Обработчик команды <see cref="AddUnloadingLineCommand"/>.
    /// Добавляет новую товарную строку в договор разгрузки.
    /// </summary>
    /// <remarks>
    /// Инициализирует новый экземпляр обработчика команды.
    /// </remarks>
    /// <param name="service">Сервис управления договорами разгрузки.</param>
    public sealed class AddUnloadingLineCommandHandler([NotNull] IUnloadingContractService service)
    {
        private readonly IUnloadingContractService _service = service ?? throw new ArgumentNullException(nameof(service));

        /// <summary>
        /// Обрабатывает команду на добавление товарной строки в договор разгрузки.
        /// </summary>
        /// <param name="command">Команда на добавление товарной строки.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        public Task Handle([NotNull]AddUnloadingLineCommand command, CancellationToken cancellationToken)
        {
            return _service.AddLineToContractAsync(command.ContractId, command.ProductId, command.Quantity, cancellationToken);
        }
    }
}
