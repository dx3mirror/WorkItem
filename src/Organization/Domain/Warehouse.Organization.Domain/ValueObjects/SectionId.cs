using Warehouse.Organization.Domain.Exceptions;

namespace Warehouse.Organization.Domain.ValueObjects
{
    /// <summary>
    /// Объект-значение, представляющий уникальный идентификатор секции склада.
    /// </summary>
    public sealed class SectionId : IEquatable<SectionId>
    {
        /// <summary>
        /// Значение GUID идентификатора.
        /// </summary>
        public Guid Value { get; }

        /// <summary>
        /// Приватный конструктор. Используется только фабрикой <see cref="Of"/>.
        /// </summary>
        /// <param name="value">GUID секции склада.</param>
        private SectionId(Guid value)
        {
            Value = value;
        }

        /// <summary>
        /// Фабричный метод для создания экземпляра <see cref="SectionId"/>.
        /// </summary>
        /// <param name="value">GUID секции склада.</param>
        /// <returns>Экземпляр <see cref="SectionId"/>.</returns>
        /// <exception cref="WarehouseInfrastructureException">
        /// Если значение GUID является пустым.
        /// </exception>
        public static SectionId Of(Guid value)
        {
            if (value == Guid.Empty)
                throw new WarehouseInfrastructureException("SectionId cannot be an empty GUID.");
            return new SectionId(value);
        }

        /// <inheritdoc/>
        public override string ToString() => Value.ToString();

        /// <inheritdoc/>
        public override bool Equals(object? obj) => Equals(obj as SectionId);

        /// <inheritdoc/>
        public bool Equals(SectionId? other) =>
            other is not null && Value.Equals(other.Value);

        /// <inheritdoc/>
        public override int GetHashCode() => Value.GetHashCode();

        /// <summary>
        /// Оператор сравнения на равенство.
        /// </summary>
        public static bool operator ==(SectionId? left, SectionId? right) => Equals(left, right);

        /// <summary>
        /// Оператор сравнения на неравенство.
        /// </summary>
        public static bool operator !=(SectionId? left, SectionId? right) => !Equals(left, right);
    }
}
