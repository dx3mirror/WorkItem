using Warehouse.ContractProcessing.Domain.Exceptions;

namespace Warehouse.ContractProcessing.Domain.ValueObjects
{
    /// <summary>
    /// Объект-значение, представляющий уникальный идентификатор контракта (обёртка над <see cref="Guid"/>).
    /// </summary>
    public readonly struct ContractId : IEquatable<ContractId>
    {
        /// <summary>
        /// Значение идентификатора контракта в формате GUID.
        /// </summary>
        public Guid Value { get; }

        /// <summary>
        /// Конструктор, используемый фабрикой <see cref="Of"/>.
        /// </summary>
        /// <param name="value">Значение GUID идентификатора.</param>
        private ContractId(Guid value)
        {
            Value = value;
        }

        /// <summary>
        /// Фабричный метод для создания нового идентификатора контракта.
        /// </summary>
        /// <param name="id">Значение GUID.</param>
        /// <returns>Экземпляр <see cref="ContractId"/>.</returns>
        /// <exception cref="UnloadingContractException">
        /// Если переданный GUID является пустым.
        /// </exception>
        public static ContractId Of(Guid id)
        {
            if (id == Guid.Empty)
                throw new UnloadingContractException("ContractId cannot be an empty GUID.");
            return new ContractId(id);
        }

        /// <inheritdoc/>
        public override string ToString() => Value.ToString();

        /// <inheritdoc/>
        public bool Equals(ContractId other) => Value.Equals(other.Value);

        /// <inheritdoc/>
        public override bool Equals(object? obj) =>
            obj is ContractId other && Equals(other);

        /// <inheritdoc/>
        public override int GetHashCode() => Value.GetHashCode();

        /// <summary>
        /// Оператор сравнения на равенство.
        /// </summary>
        public static bool operator ==(ContractId left, ContractId right) => left.Equals(right);

        /// <summary>
        /// Оператор сравнения на неравенство.
        /// </summary>
        public static bool operator !=(ContractId left, ContractId right) => !left.Equals(right);
    }
}
