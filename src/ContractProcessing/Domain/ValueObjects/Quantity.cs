using Warehouse.ContractProcessing.Domain.Exceptions;

namespace Warehouse.ContractProcessing.Domain.ValueObjects
{
    /// <summary>
    /// Объект-значение, представляющий количество единиц товара.
    /// </summary>
    public readonly struct Quantity : IEquatable<Quantity>, IComparable<Quantity>
    {
        /// <summary>
        /// Значение количества товара.
        /// </summary>
        public int Value { get; }

        /// <summary>
        /// Приватный конструктор. Используется только фабрикой <see cref="Of"/>.
        /// </summary>
        /// <param name="value">Количество товара.</param>
        private Quantity(int value)
        {
            Value = value;
        }

        /// <summary>
        /// Фабричный метод для создания количества.
        /// </summary>
        /// <param name="value">Значение количества.</param>
        /// <returns>Экземпляр <see cref="Quantity"/>.</returns>
        /// <exception cref="UnloadingContractException">
        /// Если значение меньше 0.
        /// </exception>
        public static Quantity Of(int value)
        {
            if (value < 0)
                throw new UnloadingContractException("Quantity cannot be negative.");
            return new Quantity(value);
        }

        /// <summary>
        /// Возвращает новое значение, увеличенное на указанное число.
        /// </summary>
        /// <param name="other">Добавляемое значение.</param>
        /// <returns>Новое значение <see cref="Quantity"/>.</returns>
        public Quantity Add(int other) => Of(Value + other);

        /// <summary>
        /// Возвращает новое значение, уменьшенное на указанное число.
        /// </summary>
        /// <param name="other">Вычитаемое значение.</param>
        /// <returns>Новое значение <see cref="Quantity"/>.</returns>
        /// <exception cref="UnloadingContractException">
        /// Если результат меньше 0.
        /// </exception>
        public Quantity Subtract(int other)
        {
            if (Value - other < 0)
                throw new UnloadingContractException("Resulting quantity cannot be negative.");
            return Of(Value - other);
        }

        /// <inheritdoc/>
        public override string ToString() => Value.ToString();

        /// <inheritdoc/>
        public bool Equals(Quantity other) => Value == other.Value;

        /// <inheritdoc/>
        public override bool Equals(object? obj) =>
            obj is Quantity other && Equals(other);

        /// <inheritdoc/>
        public override int GetHashCode() => Value.GetHashCode();

        /// <inheritdoc/>
        public int CompareTo(Quantity other) => Value.CompareTo(other.Value);

        /// <summary>
        /// Оператор равенства.
        /// </summary>
        public static bool operator ==(Quantity left, Quantity right) => left.Equals(right);

        /// <summary>
        /// Оператор неравенства.
        /// </summary>
        public static bool operator !=(Quantity left, Quantity right) => !left.Equals(right);

        /// <summary>
        /// Оператор "меньше".
        /// </summary>
        public static bool operator <(Quantity left, Quantity right) => left.Value < right.Value;

        /// <summary>
        /// Оператор "больше".
        /// </summary>
        public static bool operator >(Quantity left, Quantity right) => left.Value > right.Value;

        /// <summary>
        /// Оператор "меньше или равно".
        /// </summary>
        public static bool operator <=(Quantity left, Quantity right) => left.Value <= right.Value;

        /// <summary>
        /// Оператор "больше или равно".
        /// </summary>
        public static bool operator >=(Quantity left, Quantity right) => left.Value >= right.Value;
    }
}
