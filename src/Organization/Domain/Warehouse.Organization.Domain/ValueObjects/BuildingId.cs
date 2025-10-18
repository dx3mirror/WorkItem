using Warehouse.Organization.Domain.Exceptions;

namespace Warehouse.Organization.Domain.ValueObjects
{
    /// <summary>
    /// Объект-значение, представляющий уникальный идентификатор здания.
    /// </summary>
    public sealed class BuildingId : IEquatable<BuildingId>
    {
        /// <summary>
        /// Значение GUID идентификатора.
        /// </summary>
        public Guid Value { get; }

        /// <summary>
        /// Приватный конструктор. Используется только фабрикой <see cref="Of"/>.
        /// </summary>
        /// <param name="value">GUID идентификатора здания.</param>
        private BuildingId(Guid value)
        {
            Value = value;
        }

        /// <summary>
        /// Фабричный метод для создания экземпляра <see cref="BuildingId"/>.
        /// </summary>
        /// <param name="value">GUID здания.</param>
        /// <returns>Новый экземпляр <see cref="BuildingId"/>.</returns>
        /// <exception cref="WarehouseInfrastructureException">
        /// Если <paramref name="value"/> является пустым GUID.
        /// </exception>
        public static BuildingId Of(Guid value)
        {
            if (value == Guid.Empty)
                throw new WarehouseInfrastructureException("BuildingId cannot be an empty GUID.");
            return new BuildingId(value);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj) => Equals(obj as BuildingId);

        /// <inheritdoc/>
        public bool Equals(BuildingId? other) =>
            other is not null && Value.Equals(other.Value);

        /// <inheritdoc/>
        public override int GetHashCode() => Value.GetHashCode();

        /// <inheritdoc/>
        public override string ToString() => Value.ToString();

        /// <summary>
        /// Оператор сравнения на равенство.
        /// </summary>
        public static bool operator ==(BuildingId? left, BuildingId? right) => Equals(left, right);

        /// <summary>
        /// Оператор сравнения на неравенство.
        /// </summary>
        public static bool operator !=(BuildingId? left, BuildingId? right) => !Equals(left, right);
    }
}
