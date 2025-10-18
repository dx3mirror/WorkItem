using Warehouse.ContractProcessing.Domain.Aggregates;
using Warehouse.ContractProcessing.Domain.ValueObjects;

namespace Warehouse.ContractProcessing.Domain.Entity
{
    /// <summary>
    /// Строка контракта — товар и количество.
    /// </summary>
    /// <summary>
    /// Сущность строки контракта — представляет конкретный товар и его количество в рамках <see cref="UnloadingContract"/>.
    /// </summary>
    public class UnloadingLine
    {
        /// <summary>
        /// Уникальный идентификатор строки контракта.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Идентификатор товара в этой строке.
        /// </summary>
        public ProductId Product { get; }

        /// <summary>
        /// Количество единиц товара в этой строке контракта.
        /// </summary>
        public Quantity Quantity { get; private set; }

        /// <summary>
        /// Приватный конструктор для принудительного использования фабричного метода <see cref="Create"/>.
        /// </summary>
        private UnloadingLine(Guid id, ProductId product, Quantity quantity)
        {
            Id = id;
            Product = product;
            Quantity = quantity;
        }

        /// <summary>
        /// Фабричный метод для создания новой строки контракта.
        /// </summary>
        /// <param name="id">GUID новой строки.</param>
        /// <param name="product">Идентификатор товара.</param>
        /// <param name="qty">Начальное количество.</param>
        public static UnloadingLine Create(Guid id, ProductId product, Quantity qty)
            => new UnloadingLine(id, product, qty);

        /// <summary>
        /// Увеличивает текущее количество товара на заданное значение.
        /// </summary>
        /// <param name="more">Сколько единиц добавить.</param>
        public void IncreaseQuantity(int more)
        {
            Quantity = Quantity.Add(more);
        }

        /// <summary>
        /// Уменьшает текущее количество товара на заданное значение.
        /// </summary>
        /// <param name="less">Сколько единиц убрать.</param>
        public void DecreaseQuantity(int less)
        {
            Quantity = Quantity.Subtract(less);
        }
    }
}
