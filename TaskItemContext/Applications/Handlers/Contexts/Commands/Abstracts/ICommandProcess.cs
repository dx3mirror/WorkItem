namespace WorkItem.Application.Handlers.Contexts.Commands.Abstracts
{
    /// <summary>
    /// Маркерный интерфейс для всех командных обработчиков Wolverine.
    /// </summary>
    public interface ICommandProcess<in TCommand>
    {
        /// <summary>
        /// Обрабатывает команду.
        /// </summary>
        /// <param name="command">Команда.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Задача обработки команды.</returns>
        Task Handle(TCommand command, CancellationToken cancellationToken);
    }
}
