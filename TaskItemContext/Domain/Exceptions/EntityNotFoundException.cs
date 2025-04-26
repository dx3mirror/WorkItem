namespace WorkItem.Domain.Exceptions
{
    /// <summary>
    /// Исключение, возникающее при отсутствии сущности в хранилище.
    /// </summary>
    /// <remarks>
    /// Конструктор исключения.
    /// </remarks>
    /// <param name="entityName"></param>
    /// <param name="id"></param>
    public sealed class EntityNotFoundException(string entityName, Guid id)
        : Exception($"Entity '{entityName}' with ID '{id}' was not found.")
    {
    }
}
