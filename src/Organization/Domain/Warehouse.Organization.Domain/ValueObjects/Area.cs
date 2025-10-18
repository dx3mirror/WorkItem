using Warehouse.Organization.Domain.Exceptions;

namespace Warehouse.Organization.Domain.ValueObjects
{
    /// <summary>
    /// Объект-значение, представляющий площадь в квадратных метрах.
    /// Используется для определения физической площади объектов инфраструктуры.
    /// </summary>
    public sealed class Area : IEquatable<Area>, IComparable<Area>
    {
        /// <summary>
        /// Значение площади в квадратных метрах.
        /// </summary>
        public double Value { get; }

        /// <summary>
        /// Приватный конструктор. Используется только внутри фабрики <see cref="Of(double)"/>.
        /// </summary>
        /// <param name="value">Площадь в квадратных метрах.</param>
        private Area(double value)
        {
            Value = value;
        }

        /// <summary>
        /// Фабричный метод создания экземпляра <see cref="Area"/>.
        /// </summary>
        /// <param name="value">Значение площади в квадратных метрах.</param>
        /// <returns>Новый экземпляр <see cref="Area"/>.</returns>
        /// <exception cref="WarehouseInfrastructureException">
        /// Выбрасывается, если площадь не является положительным числом.
        /// </exception>
        public static Area Of(double value)
        {
            if (value <= 0)
                throw new WarehouseInfrastructureException("Area must be greater than zero.");
            return new Area(value);
        }

        /// <summary>
        /// Возвращает новый экземпляр <see cref="Area"/>, являющийся суммой текущей и переданной площади.
        /// </summary>
        /// <param name="other">Другая площадь.</param>
        /// <returns>Суммарная площадь.</returns>
        public Area Add(Area other) => Of(Value + other.Value);

        /// <summary>
        /// Возвращает новую площадь, представляющую разницу между текущей и переданной площадью.
        /// </summary>
        /// <param name="other">Площадь для вычитания.</param>
        /// <returns>Результат вычитания.</returns>
        /// <exception cref="WarehouseInfrastructureException">
        /// Если результат вычитания меньше или равен нулю.
        /// </exception>
        public Area Subtract(Area other)
        {
            var result = Value - other.Value;
            if (result <= 0)
                throw new WarehouseInfrastructureException("Resulting area must be greater than zero.");
            return Of(result);
        }

        /// <summary>
        /// Возвращает строковое представление площади в формате "X m²".
        /// </summary>
        /// <returns>Строка, представляющая площадь.</returns>
        public override string ToString() => $"{Value:0.##} m²";

        /// <summary>
        /// Проверяет эквивалентность текущей площади и переданной.
        /// </summary>
        /// <param name="obj">Объект для сравнения.</param>
        /// <returns><c>true</c>, если площади равны.</returns>
        public override bool Equals(object? obj) => Equals(obj as Area);

        /// <summary>
        /// Проверяет эквивалентность с другим объектом <see cref="Area"/>.
        /// </summary>
        /// <param name="other">Площадь для сравнения.</param>
        /// <returns><c>true</c>, если значения совпадают.</returns>
        public bool Equals(Area? other) =>
            other is not null && Value.Equals(other.Value);

        /// <summary>
        /// Возвращает хэш-код текущего объекта.
        /// </summary>
        /// <returns>Целое число – хэш-код.</returns>
        public override int GetHashCode() => Value.GetHashCode();

        /// <summary>
        /// Сравнивает текущую площадь с другой по значению.
        /// </summary>
        /// <param name="other">Площадь для сравнения.</param>
        /// <returns>1, если больше; -1, если меньше; 0, если равны.</returns>
        public int CompareTo(Area? other)
        {
            if (other is null) return 1;
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Оператор сравнения на равенство.
        /// </summary>
        public static bool operator ==(Area? left, Area? right) => Equals(left, right);

        /// <summary>
        /// Оператор сравнения на неравенство.
        /// </summary>
        public static bool operator !=(Area? left, Area? right) => !Equals(left, right);

        /// <summary>
        /// Оператор "больше".
        /// </summary>
        public static bool operator >(Area left, Area right) => left.Value > right.Value;

        /// <summary>
        /// Оператор "меньше".
        /// </summary>
        public static bool operator <(Area left, Area right) => left.Value < right.Value;

        /// <summary>
        /// Оператор "больше или равно".
        /// </summary>
        public static bool operator >=(Area left, Area right) => left.Value >= right.Value;

        /// <summary>
        /// Оператор "меньше или равно".
        /// </summary>
        public static bool operator <=(Area left, Area right) => left.Value <= right.Value;
    }
}
