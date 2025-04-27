namespace WorkItem.Domain.Enums
{
    /// <summary>
    /// Статус рабочего элемента.
    /// </summary>
    public enum WorkItemStatus
    {
        /// <summary>
        /// Новый рабочий элемент.
        /// </summary>
        New = 1,

        /// <summary>
        /// Заголовок установлен.
        /// </summary>
        SetTitle = 2,

        /// <summary>
        /// Описание установлено.
        /// </summary>
        SetDescription = 3,

        /// <summary>
        /// Данные о завершении установлены.
        /// </summary>
        SetCompletedData = 4
    }
}
