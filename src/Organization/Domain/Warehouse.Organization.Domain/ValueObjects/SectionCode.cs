using Warehouse.Organization.Domain.Exceptions;

namespace Warehouse.Organization.Domain.ValueObjects
{
    /// <summary>
    /// Объект-значение, представляющий уникальный код секции склада.
    /// </summary>
    public sealed class SectionCode : IEquatable<SectionCode>
    {
        /// <summary>
        /// Строковое значение кода секции.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Приватный конструктор. Используется только через фабрику <see cref="Of"/>.
        /// </summary>
        /// <param name="value">Значение кода секции.</param>
        private SectionCode(string value)
        {
            Value = value;
        }

        /// <summary>
        /// Фабричный метод создания экземпляра <see cref="SectionCode"/>.
        /// </summary>
        /// <param name="value">Строка кода секции.</param>
        /// <returns>Экземпляр <see cref="SectionCode"/>.</returns>
        /// <exception cref="WarehouseInfrastructureException">
        /// Если строка пуста или содержит только пробелы.
        /// </exception>
        public static SectionCode Of(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new WarehouseInfrastructureException("Section code cannot be empty.");
            return new SectionCode(value.Trim());
        }

        /// <inheritdoc/>
        public override string ToString() => Value;

        /// <inheritdoc/>
        public override bool Equals(object? obj) => Equals(obj as SectionCode);

        /// <inheritdoc/>
        public bool Equals(SectionCode? other) =>
            other is not null && Value.Equals(other.Value, StringComparison.Ordinal);

        /// <inheritdoc/>
        public override int GetHashCode() => Value.GetHashCode(StringComparison.Ordinal);

        /// <summary>
        /// Оператор сравнения на равенство.
        /// </summary>
        public static bool operator ==(SectionCode? left, SectionCode? right) => Equals(left, right);

        /// <summary>
        /// Оператор сравнения на неравенство.
        /// </summary>
        public static bool operator !=(SectionCode? left, SectionCode? right) => !Equals(left, right);
    }
}
