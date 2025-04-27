namespace WorkItem.Domain.Utilities
{
    /// <summary>
    /// Вспомогательные методы для генерации идентификаторов.
    /// </summary>
    public static class IdGenerator
    {
        public static Guid NewGuid() => Guid.NewGuid();
    }
}
