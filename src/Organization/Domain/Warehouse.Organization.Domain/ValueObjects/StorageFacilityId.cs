using Warehouse.Organization.Domain.Exceptions;

namespace Warehouse.Organization.Domain.ValueObjects
{
    /// <summary>
    /// Объект-значение, представляющий уникальный идентификатор складского помещения.
    /// </summary>
    public sealed class StorageFacilityId : IEquatable<StorageFacilityId>
    {
        /// <summary>
        /// Значение идентификатора в формате GUID.
        /// </summary>
        public Guid Value { get; }

        /// <summary>
        /// Приватный конструктор. Используется через <see cref="Of"/>.
        /// </summary>
        /// <param name="value">GUID идентификатора.</param>
        private StorageFacilityId(Guid value)
        {
            Value = value;
        }

        /// <summary>
        /// Фабричный метод для создания экземпляра <see cref="StorageFacilityId"/>.
        /// </summary>
        /// <param name="value">GUID идентификатора складского помещения.</param>
        /// <returns>Экземпляр <see cref="StorageFacilityId"/>.</returns>
        /// <exception cref="WarehouseInfrastructureException">
        /// Если передан пустой GUID.
        /// </exception>
        public static StorageFacilityId Of(Guid value)
        {
            if (value == Guid.Empty)
                throw new WarehouseInfrastructureException("StorageFacilityId cannot be an empty GUID.");
            return new StorageFacilityId(value);
        }

        /// <inheritdoc/>
        public override string ToString() => Value.ToString();

        /// <inheritdoc/>
        public override bool Equals(object? obj) => Equals(obj as StorageFacilityId);

        /// <inheritdoc/>
        public bool Equals(StorageFacilityId? other) =>
            other is not null && Value.Equals(other.Value);

        /// <inheritdoc/>
        public override int GetHashCode() => Value.GetHashCode();

        /// <summary>
        /// Оператор сравнения на равенство.
        /// </summary>
        public static bool operator ==(StorageFacilityId? left, StorageFacilityId? right) => Equals(left, right);

        /// <summary>
        /// Оператор сравнения на неравенство.
        /// </summary>
        public static bool operator !=(StorageFacilityId? left, StorageFacilityId? right) => !Equals(left, right);
    }
}
