namespace Warehouse.Organization.Domain.Exceptions
{
    /// <summary>
    /// Исключение, возникающее при ошибках инфраструктуры склада.
    /// </summary>
    public class WarehouseInfrastructureException : Exception
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="WarehouseInfrastructureException"/>.
        /// </summary>
        public WarehouseInfrastructureException() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public WarehouseInfrastructureException(string message)
            : base(message) { }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="WarehouseInfrastructureException"/>.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public WarehouseInfrastructureException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
