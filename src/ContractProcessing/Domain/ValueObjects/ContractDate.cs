namespace Warehouse.ContractProcessing.Domain.ValueObjects
{
    /// <summary>
    /// Объект-значение, представляющий дату и время создания контракта (в UTC).
    /// </summary>
    public sealed class ContractDate : IEquatable<ContractDate>, IComparable<ContractDate>
    {
        /// <summary>
        /// Значение даты и времени создания контракта в формате UTC.
        /// </summary>
        public DateTime Value { get; }

        /// <summary>
        /// Приватный конструктор. Используется только методом <see cref="Now"/>.
        /// </summary>
        /// <param name="value">Дата и время создания контракта.</param>
        public ContractDate(DateTime value)
        {
            Value = value;
        }

        /// <summary>
        /// Возвращает текущую дату и время в формате UTC.
        /// Используется при создании нового контракта.
        /// </summary>
        /// <returns>Экземпляр <see cref="ContractDate"/> с текущим значением времени.</returns>
        public static ContractDate Now() => new(DateTime.UtcNow);

        /// <inheritdoc/>
        public override string ToString() => Value.ToString("u");

        /// <inheritdoc/>
        public override bool Equals(object? obj) => Equals(obj as ContractDate);

        /// <inheritdoc/>
        public bool Equals(ContractDate? other) =>
            other is not null && Value.Equals(other.Value);

        /// <inheritdoc/>
        public override int GetHashCode() => Value.GetHashCode();

        /// <inheritdoc/>
        public int CompareTo(ContractDate? other)
        {
            if (other is null) return 1;
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Оператор сравнения на равенство.
        /// </summary>
        public static bool operator ==(ContractDate? left, ContractDate? right) => Equals(left, right);

        /// <summary>
        /// Оператор сравнения на неравенство.
        /// </summary>
        public static bool operator !=(ContractDate? left, ContractDate? right) => !Equals(left, right);

        /// <summary>
        /// Оператор "меньше".
        /// </summary>
        public static bool operator <(ContractDate left, ContractDate right) => left.Value < right.Value;

        /// <summary>
        /// Оператор "больше".
        /// </summary>
        public static bool operator >(ContractDate left, ContractDate right) => left.Value > right.Value;

        /// <summary>
        /// Оператор "меньше или равно".
        /// </summary>
        public static bool operator <=(ContractDate left, ContractDate right) => left.Value <= right.Value;

        /// <summary>
        /// Оператор "больше или равно".
        /// </summary>
        public static bool operator >=(ContractDate left, ContractDate right) => left.Value >= right.Value;
    }
}
