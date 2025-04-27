namespace WorkItem.Domain.ValueObjects
{
    /// <summary>
    /// Объект-значение, представляющий заголовок рабочего элемента.
    /// </summary>
    public sealed class WorkItemTitle : IEquatable<WorkItemTitle>
    {
        /// <summary>
        /// Текст заголовка.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Создаёт новый заголовок рабочего элемента.
        /// </summary>
        /// <param name="value">Текст заголовка.</param>
        /// <exception cref="ArgumentException">Если <paramref name="value"/> пустой или состоит только из пробелов.</exception>
        public WorkItemTitle(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Title cannot be empty.", nameof(value));

            Value = value;
        }

        /// <inheritdoc/>
        public bool Equals(WorkItemTitle? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value == other.Value;
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            return Equals(obj as WorkItemTitle);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
