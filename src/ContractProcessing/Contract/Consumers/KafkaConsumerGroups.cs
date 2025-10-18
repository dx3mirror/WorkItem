namespace Warehouse.ContractProcessing.Сontract.Consumers
{
    /// <summary>
    /// Константы с именами групп Kafka-консьюмеров, используемых в системе.
    /// </summary>
    public static class KafkaConsumerGroups
    {
        /// <summary>
        /// Группа обработки договоров.
        /// </summary>
        public const string ContractProcessing = "contract-processing";

        /// <summary>
        /// Группа логирования событий потребителей.
        /// </summary>
        public const string ConsumersLog = "consumers-log";
    }
}
