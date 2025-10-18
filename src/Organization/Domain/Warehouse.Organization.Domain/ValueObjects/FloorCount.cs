using Warehouse.Organization.Domain.Exceptions;

namespace Warehouse.Organization.Domain.ValueObjects
{
    /// <summary>
    /// Объект-значение, представляющий количество этажей в здании.
    /// </summary>
    public sealed class FloorCount : IEquatable<FloorCount>, IComparable<FloorCount>
    {
        /// <summary>
        /// Целочисленное значение количества этажей.
        /// </summary>
        public int Value { get; }

        /// <summary>
        /// Приватный конструктор. Используется только фабричным методом <see cref="Of"/>.
        /// </summary>
        /// <param name="value">Количество этажей.</param>
        private FloorCount(int value)
        {
            Value = value;
        }

        /// <summary>
        /// Фабричный метод для создания экземпляра <see cref="FloorCount"/>.
        /// </summary>
        /// <param name="value">Количество этажей.</param>
        /// <returns>Экземпляр <see cref="FloorCount"/>.</returns>
        /// <exception cref="WarehouseInfrastructureException">
        /// Выбрасывается, если значение не является положительным числом.
        /// </exception>
        public static FloorCount Of(int value)
        {
            if (value <= 0)
                throw new WarehouseInfrastructureException("Total floors must be positive.");
            return new FloorCount(value);
        }

        /// <inheritdoc/>
        public override string ToString() => Value.ToString();

        /// <inheritdoc/>
        public override bool Equals(object? obj) => Equals(obj as FloorCount);

        /// <inheritdoc/>
        public bool Equals(FloorCount? other) =>
            other is not null && Value == other.Value;

        /// <inheritdoc/>
        public override int GetHashCode() => Value.GetHashCode();

        /// <inheritdoc/>
        public int CompareTo(FloorCount? other)
        {
            if (other is null) return 1;
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Оператор сравнения на равенство.
        /// </summary>
        public static bool operator ==(FloorCount? left, FloorCount? right) => Equals(left, right);

        /// <summary>
        /// Оператор сравнения на неравенство.
        /// </summary>
        public static bool operator !=(FloorCount? left, FloorCount? right) => !Equals(left, right);

        /// <summary>
        /// Оператор "меньше".
        /// </summary>
        public static bool operator <(FloorCount left, FloorCount right) => left.Value < right.Value;

        /// <summary>
        /// Оператор "больше".
        /// </summary>
        public static bool operator >(FloorCount left, FloorCount right) => left.Value > right.Value;

        /// <summary>
        /// Оператор "меньше или равно".
        /// </summary>
        public static bool operator <=(FloorCount left, FloorCount right) => left.Value <= right.Value;

        /// <summary>
        /// Оператор "больше или равно".
        /// </summary>
        public static bool operator >=(FloorCount left, FloorCount right) => left.Value >= right.Value;
    }
}
