namespace Utilities.MassTransitSettings.Configurations
{
    /// <summary>
    /// Настройки подключения к серверу RabbitMQ.
    /// </summary>
    public class RabbitMqConfiguration
    {
        /// <summary>
        /// Адрес хоста RabbitMQ (например, "rabbitmq://localhost:5672").
        /// </summary>
        public string Host { get; set; } = string.Empty;

        /// <summary>
        /// Имя пользователя для подключения к RabbitMQ.
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Пароль для подключения к RabbitMQ.
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}
