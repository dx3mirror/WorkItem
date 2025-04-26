namespace TaskExample.Domain.Exceptions
{
    /// <summary>
    /// Исключение, выбрасываемое при нарушении бизнес-валидации в домене.
    /// </summary>
    public sealed class DomainValidationException(string propertyName, string message)
        : Exception($"Validation failed for '{propertyName}': {message}")
    {
        /// <summary>
        /// Имя свойства, вызвавшего ошибку валидации.
        /// </summary>
        public string PropertyName { get; } = propertyName;
    }
}
