using Warehouse.ContractProcessing.Domain.Exceptions;

namespace Warehouse.ContractProcessing.Domain.ValueObjects
{
    /// <summary>
    /// Объект-значение, представляющий уникальный идентификатор склада.
    /// </summary>
    public readonly struct WarehouseId : IEquatable<WarehouseId>
    {
        /// <summary>
        /// Значение идентификатора склада в формате GUID.
        /// </summary>
        public Guid Value { get; }

        /// <summary>
        /// Приватный конструктор. Используется только через <see cref="Of"/>.
        /// </summary>
        /// <param name="value">Значение идентификатора склада.</param>
        private WarehouseId(Guid value)
        {
            Value = value;
        }

        /// <summary>
        /// Фабричный метод для создания экземпляра <see cref="WarehouseId"/>.
        /// </summary>
        /// <param name="id">GUID склада.</param>
        /// <returns>Экземпляр <see cref="WarehouseId"/>.</returns>
        /// <exception cref="UnloadingContractException">
        /// Если передан пустой GUID.
        /// </exception>
        public static WarehouseId Of(Guid id)
        {
            if (id == Guid.Empty)
                throw new UnloadingContractException("WarehouseId cannot be an empty GUID.");
            return new WarehouseId(id);
        }

        /// <inheritdoc/>
        public bool Equals(WarehouseId other) => Value.Equals(other.Value);

        /// <inheritdoc/>
        public override bool Equals(object? obj) =>
            obj is WarehouseId other && Equals(other);

        /// <inheritdoc/>
        public override int GetHashCode() => Value.GetHashCode();

        /// <inheritdoc/>
        public override string ToString() => Value.ToString();

        /// <summary>
        /// Оператор сравнения на равенство.
        /// </summary>
        public static bool operator ==(WarehouseId left, WarehouseId right) => left.Equals(right);

        /// <summary>
        /// Оператор сравнения на неравенство.
        /// </summary>
        public static bool operator !=(WarehouseId left, WarehouseId right) => !left.Equals(right);
    }
}
