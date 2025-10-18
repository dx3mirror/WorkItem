using Warehouse.Organization.Domain.Exceptions;

namespace Warehouse.Organization.Domain.ValueObjects
{
    /// <summary>
    /// Объект-значение, представляющий номер этажа.
    /// Этаж может быть нулевым (например, цокольный или первый по европейской системе).
    /// </summary>
    public sealed class FloorNumber : IEquatable<FloorNumber>, IComparable<FloorNumber>
    {
        /// <summary>
        /// Целочисленное значение номера этажа.
        /// </summary>
        public int Value { get; }

        /// <summary>
        /// Приватный конструктор. Используется только фабрикой <see cref="Of"/>.
        /// </summary>
        /// <param name="value">Номер этажа.</param>
        private FloorNumber(int value)
        {
            Value = value;
        }

        /// <summary>
        /// Фабричный метод для создания экземпляра <see cref="FloorNumber"/>.
        /// </summary>
        /// <param name="value">Номер этажа.</param>
        /// <returns>Экземпляр <see cref="FloorNumber"/>.</returns>
        /// <exception cref="WarehouseInfrastructureException">
        /// Выбрасывается, если значение отрицательное.
        /// </exception>
        public static FloorNumber Of(int value)
        {
            if (value < 0)
                throw new WarehouseInfrastructureException("Floor number must be non-negative.");
            return new FloorNumber(value);
        }

        /// <inheritdoc/>
        public override string ToString() => Value.ToString();

        /// <inheritdoc/>
        public override bool Equals(object? obj) => Equals(obj as FloorNumber);

        /// <inheritdoc/>
        public bool Equals(FloorNumber? other) =>
            other is not null && Value == other.Value;

        /// <inheritdoc/>
        public override int GetHashCode() => Value.GetHashCode();

        /// <inheritdoc/>
        public int CompareTo(FloorNumber? other)
        {
            if (other is null) return 1;
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Оператор сравнения на равенство.
        /// </summary>
        public static bool operator ==(FloorNumber? left, FloorNumber? right) => Equals(left, right);

        /// <summary>
        /// Оператор сравнения на неравенство.
        /// </summary>
        public static bool operator !=(FloorNumber? left, FloorNumber? right) => !Equals(left, right);

        /// <summary>
        /// Оператор "меньше".
        /// </summary>
        public static bool operator <(FloorNumber left, FloorNumber right) => left.Value < right.Value;

        /// <summary>
        /// Оператор "больше".
        /// </summary>
        public static bool operator >(FloorNumber left, FloorNumber right) => left.Value > right.Value;

        /// <summary>
        /// Оператор "меньше или равно".
        /// </summary>
        public static bool operator <=(FloorNumber left, FloorNumber right) => left.Value <= right.Value;

        /// <summary>
        /// Оператор "больше или равно".
        /// </summary>
        public static bool operator >=(FloorNumber left, FloorNumber right) => left.Value >= right.Value;
    }
}
