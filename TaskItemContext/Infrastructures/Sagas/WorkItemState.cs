using MassTransit;

namespace WorkItemContext.Infrastructures.Sagas
{
    /// <summary>
    /// Состояние саги для рабочего элемента.
    /// </summary>
    public class WorkItemState : SagaStateMachineInstance
    {
        /// <summary>
        /// Идентификатор корреляции саги.
        /// </summary>
        public Guid CorrelationId { get; set; }

        /// <summary>
        /// Текущее состояние саги.
        /// </summary>
        public string CurrentState { get; set; } = string.Empty;

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Заголовок рабочего элемента.
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Описание рабочего элемента.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Дата завершения рабочего элемента.
        /// </summary>
        public DateTime? CompletedDate { get; set; }

        /// <summary>
        /// Дата создания состояния.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Идентификатор отложенного сообщения для таймаута.
        /// </summary>
        public Guid? TimeoutTokenId { get; set; }
    }
}
