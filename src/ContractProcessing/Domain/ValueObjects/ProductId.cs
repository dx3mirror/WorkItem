using Warehouse.ContractProcessing.Domain.Exceptions;

namespace Warehouse.ContractProcessing.Domain.ValueObjects
{
    /// <summary>
    /// Объект-значение, представляющий уникальный идентификатор товара.
    /// </summary>
    public readonly struct ProductId : IEquatable<ProductId>
    {
        /// <summary>
        /// Значение идентификатора товара в формате GUID.
        /// </summary>
        public Guid Value { get; }

        /// <summary>
        /// Приватный конструктор. Используется только через <see cref="Of"/>.
        /// </summary>
        /// <param name="value">GUID товара.</param>
        private ProductId(Guid value)
        {
            Value = value;
        }

        /// <summary>
        /// Фабричный метод для создания экземпляра <see cref="ProductId"/>.
        /// </summary>
        /// <param name="id">GUID товара.</param>
        /// <returns>Экземпляр <see cref="ProductId"/>.</returns>
        /// <exception cref="UnloadingContractException">
        /// Если GUID пустой.
        /// </exception>
        public static ProductId Of(Guid id)
        {
            if (id == Guid.Empty)
                throw new UnloadingContractException("ProductId cannot be an empty GUID.");
            return new ProductId(id);
        }

        /// <inheritdoc/>
        public bool Equals(ProductId other) => Value.Equals(other.Value);

        /// <inheritdoc/>
        public override bool Equals(object? obj) =>
            obj is ProductId other && Equals(other);

        /// <inheritdoc/>
        public override int GetHashCode() => Value.GetHashCode();

        /// <inheritdoc/>
        public override string ToString() => Value.ToString();

        /// <summary>
        /// Оператор сравнения на равенство.
        /// </summary>
        public static bool operator ==(ProductId left, ProductId right) => left.Equals(right);

        /// <summary>
        /// Оператор сравнения на неравенство.
        /// </summary>
        public static bool operator !=(ProductId left, ProductId right) => !left.Equals(right);
    }
}
