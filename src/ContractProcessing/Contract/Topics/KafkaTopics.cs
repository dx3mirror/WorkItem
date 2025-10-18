namespace Warehouse.ContractProcessing.Сontract.Topics
{
    /// <summary>
    /// Константы с именами Kafka-топиков, используемых для событий разгрузки.
    /// </summary>
    public static class KafkaTopics
    {
        /// <summary>
        /// Топик событий запуска разгрузки.
        /// </summary>
        public const string UnloadingStarted = "unloading-started";

        /// <summary>
        /// Топик событий завершения разгрузки.
        /// </summary>
        public const string UnloadingCompleted = "unloading-completed";

        /// <summary>
        /// Топик событий отмены разгрузки.
        /// </summary>
        public const string UnloadingCancelled = "unloading-cancelled";

        /// <summary>
        /// Топик событий ошибок разгрузки.
        /// </summary>
        public const string UnloadingError = "unloading-error";
    }
}
