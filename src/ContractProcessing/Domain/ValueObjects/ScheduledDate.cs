using Warehouse.ContractProcessing.Domain.Exceptions;

namespace Warehouse.ContractProcessing.Domain.ValueObjects
{
    /// <summary>
    /// Объект-значение, представляющий запланированную дату и время выгрузки (в UTC).
    /// </summary>
    public sealed class ScheduledDate : IEquatable<ScheduledDate>, IComparable<ScheduledDate>
    {
        /// <summary>
        /// Значение даты и времени в формате UTC.
        /// </summary>
        public DateTime Value { get; }

        /// <summary>
        /// Конструктор для EF Core. Не использовать напрямую.
        /// </summary>
        public ScheduledDate(DateTime value)
        {
            Value = value;
        }

        /// <summary>
        /// Фабрика для создания <see cref="ScheduledDate"/>.
        /// </summary>
        /// <param name="dt">Дата и время выгрузки в UTC.</param>
        /// <returns>Экземпляр <see cref="ScheduledDate"/>.</returns>
        /// <exception cref="UnloadingContractException">
        /// Если дата находится в прошлом.
        /// </exception>
        public static ScheduledDate Of(DateTime dt)
        {
            if (dt.Kind != DateTimeKind.Utc)
                throw new UnloadingContractException("Scheduled date must be in UTC.");
            if (dt < DateTime.UtcNow)
                throw new UnloadingContractException("Scheduled date cannot be in the past.");
            return new ScheduledDate(dt);
        }

        /// <inheritdoc/>
        public override string ToString() => Value.ToString("u");

        /// <inheritdoc/>
        public override bool Equals(object? obj) => Equals(obj as ScheduledDate);

        /// <inheritdoc/>
        public bool Equals(ScheduledDate? other) =>
            other is not null && Value.Equals(other.Value);

        /// <inheritdoc/>
        public override int GetHashCode() => Value.GetHashCode();

        /// <inheritdoc/>
        public int CompareTo(ScheduledDate? other)
        {
            if (other is null) return 1;
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Оператор равенства.
        /// </summary>
        public static bool operator ==(ScheduledDate? left, ScheduledDate? right) => Equals(left, right);

        /// <summary>
        /// Оператор неравенства.
        /// </summary>
        public static bool operator !=(ScheduledDate? left, ScheduledDate? right) => !Equals(left, right);

        /// <summary>
        /// Оператор "меньше".
        /// </summary>
        public static bool operator <(ScheduledDate left, ScheduledDate right) => left.Value < right.Value;

        /// <summary>
        /// Оператор "больше".
        /// </summary>
        public static bool operator >(ScheduledDate left, ScheduledDate right) => left.Value > right.Value;

        /// <summary>
        /// Оператор "меньше или равно".
        /// </summary>
        public static bool operator <=(ScheduledDate left, ScheduledDate right) => left.Value <= right.Value;

        /// <summary>
        /// Оператор "больше или равно".
        /// </summary>
        public static bool operator >=(ScheduledDate left, ScheduledDate right) => left.Value >= right.Value;
    }
}
