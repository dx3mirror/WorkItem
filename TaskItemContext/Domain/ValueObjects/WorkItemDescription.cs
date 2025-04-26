namespace TaskExample.Domain.ValueObjects
{
    /// <summary>
    /// Объект-значение, представляющий описание рабочего элемента.
    /// </summary>
    public sealed class WorkItemDescription : IEquatable<WorkItemDescription>
    {
        /// <summary>
        /// Текст описания.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Создаёт новое описание рабочего элемента.
        /// </summary>
        /// <param name="value">Текст описания.</param>
        /// <exception cref="ArgumentException">Если <paramref name="value"/> пустой или состоит только из пробелов.</exception>
        public WorkItemDescription(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Description cannot be empty.", nameof(value));

            Value = value;
        }

        /// <inheritdoc/>
        public bool Equals(WorkItemDescription? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value == other.Value;
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            return Equals(obj as WorkItemDescription);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
