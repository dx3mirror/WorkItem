using Warehouse.ContractProcessing.Domain.Exceptions;

namespace Warehouse.ContractProcessing.Domain.ValueObjects
{
    /// <summary>
    /// Объект-значение, представляющий уникальный идентификатор менеджера.
    /// </summary>
    public readonly struct ManagerId : IEquatable<ManagerId>
    {
        /// <summary>
        /// Значение идентификатора менеджера в формате GUID.
        /// </summary>
        public Guid Value { get; }

        /// <summary>
        /// Приватный конструктор. Используется только через метод <see cref="Of"/>.
        /// </summary>
        /// <param name="value">GUID идентификатора.</param>
        private ManagerId(Guid value)
        {
            Value = value;
        }

        /// <summary>
        /// Фабричный метод для создания экземпляра <see cref="ManagerId"/>.
        /// </summary>
        /// <param name="id">GUID менеджера.</param>
        /// <returns>Экземпляр <see cref="ManagerId"/>.</returns>
        /// <exception cref="UnloadingContractException">
        /// Если переданный GUID является пустым.
        /// </exception>
        public static ManagerId Of(Guid id)
        {
            if (id == Guid.Empty)
                throw new UnloadingContractException("ManagerId cannot be an empty GUID.");
            return new ManagerId(id);
        }

        /// <inheritdoc/>
        public bool Equals(ManagerId other) => Value.Equals(other.Value);

        /// <inheritdoc/>
        public override bool Equals(object? obj) =>
            obj is ManagerId other && Equals(other);

        /// <inheritdoc/>
        public override int GetHashCode() => Value.GetHashCode();

        /// <inheritdoc/>
        public override string ToString() => Value.ToString();

        /// <summary>
        /// Оператор сравнения на равенство.
        /// </summary>
        public static bool operator ==(ManagerId left, ManagerId right) => left.Equals(right);

        /// <summary>
        /// Оператор сравнения на неравенство.
        /// </summary>
        public static bool operator !=(ManagerId left, ManagerId right) => !left.Equals(right);
    }
}
