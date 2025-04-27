namespace WorkItem.Domain.Utilities
{
    /// <summary>
    /// Вспомогательные методы для получения времени.
    /// </summary>
    public static class SystemClock
    {
        public static DateTime UtcNow() => DateTime.UtcNow;
    }
}
