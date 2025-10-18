using Warehouse.Organization.Domain.Exceptions;

namespace Warehouse.Organization.Domain.ValueObjects
{
    /// <summary>
    /// Объект-значение, представляющий наименование складского помещения.
    /// </summary>
    public sealed class StorageFacilityName : IEquatable<StorageFacilityName>
    {
        /// <summary>
        /// Строковое значение наименования склада.
        /// </summary>
        public string Value { get; }

        private const byte MaxLength = 100;

        /// <summary>
        /// Приватный конструктор. Используется только через <see cref="Of"/>.
        /// </summary>
        /// <param name="value">Название складского помещения.</param>
        private StorageFacilityName(string value)
        {
            Value = value;
        }

        /// <summary>
        /// Фабричный метод для создания экземпляра <see cref="StorageFacilityName"/>.
        /// </summary>
        /// <param name="value">Строка наименования склада.</param>
        /// <returns>Экземпляр <see cref="StorageFacilityName"/>.</returns>
        /// <exception cref="WarehouseInfrastructureException">
        /// Выбрасывается, если строка пустая, состоит из пробелов или превышает допустимую длину.
        /// </exception>
        public static StorageFacilityName Of(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new WarehouseInfrastructureException("Storage facility name cannot be empty.");

            value = value.Trim();

            if (value.Length > MaxLength)
                throw new WarehouseInfrastructureException($"Storage facility name cannot exceed {MaxLength} characters.");

            return new StorageFacilityName(value);
        }

        /// <inheritdoc/>
        public override string ToString() => Value;

        /// <inheritdoc/>
        public override bool Equals(object? obj) => Equals(obj as StorageFacilityName);

        /// <inheritdoc/>
        public bool Equals(StorageFacilityName? other) =>
            other is not null && Value.Equals(other.Value, StringComparison.Ordinal);

        /// <inheritdoc/>
        public override int GetHashCode() => Value.GetHashCode(StringComparison.Ordinal);

        /// <summary>
        /// Оператор сравнения на равенство.
        /// </summary>
        public static bool operator ==(StorageFacilityName? left, StorageFacilityName? right) => Equals(left, right);

        /// <summary>
        /// Оператор сравнения на неравенство.
        /// </summary>
        public static bool operator !=(StorageFacilityName? left, StorageFacilityName? right) => !Equals(left, right);
    }
}
