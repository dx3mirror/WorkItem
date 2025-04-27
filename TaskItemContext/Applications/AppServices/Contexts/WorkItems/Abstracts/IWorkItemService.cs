using WorkItem.Application.AppServices.Contexts.WorkItems.Models;

namespace WorkItem.Application.AppServices.Contexts.WorkItems.Abstracts
{
    /// <summary>
    /// Сервис для работы с рабочими элементами.
    /// </summary>
    public interface IWorkItemService
    {
        /// <summary>
        /// Создаёт новый рабочий элемент.
        /// </summary>
        /// <param name="request">Модель запроса на создание рабочего элемента.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Созданный рабочий элемент.</returns>
        Task CreateAsync(CreateWorkItemModel request, CancellationToken cancellationToken);

        /// <summary>
        /// Устанавливает заголовок рабочего элемента.
        /// </summary>
        Task SetTitleAsync(SetWorkItemTitleModel request, CancellationToken cancellationToken);

        /// <summary>
        /// Устанавливает описание рабочего элемента.
        /// </summary>
        Task SetDescriptionAsync(SetWorkItemDescriptionModel request, CancellationToken cancellationToken);

        /// <summary>
        /// Завершает рабочий элемент.
        /// </summary>
        Task CompleteAsync(CompleteWorkItemModel request, CancellationToken cancellationToken);
    }
}
