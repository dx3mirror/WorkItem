using Warehouse.ContractProcessing.Domain.Entity;
using Warehouse.ContractProcessing.Domain.Enums;
using Warehouse.ContractProcessing.Domain.Exceptions;
using Warehouse.ContractProcessing.Domain.ValueObjects;

namespace Warehouse.ContractProcessing.Domain.Aggregates
{
    /// <summary>
    /// Агрегат-корень, представляющий контракт на выгрузку товаров на склад.
    /// </summary>
    public class UnloadingContract 
    {
        /// <summary>Уникальный идентификатор контракта.</summary>
        public ContractId Id { get; }

        /// <summary>Идентификатор склада, на который планируется выгрузка.</summary>
        public WarehouseId Warehouse { get; }

        /// <summary>Идентификатор менеджера, ответственного за контракт.</summary>
        public ManagerId Manager { get; }

        /// <summary>Дата и время создания контракта.</summary>
        public ContractDate CreatedAt { get; }

        /// <summary>Запланированная дата и время выгрузки.</summary>
        public ScheduledDate ScheduledFor { get; private set; }

        /// <summary>Текущий статус контракта.</summary>
        public ContractStatus Status { get; private set; }

        private readonly List<UnloadingLine> _lines = [];

        /// <summary>
        /// Коллекция товарных строк, входящих в договор разгрузки.
        /// </summary>
        public virtual ICollection<UnloadingLine> Lines => _lines;


        /// <summary>
        /// Приватный конструктор, используемый фабричным методом Create.
        /// </summary>
        private UnloadingContract(
            ContractId id,
            WarehouseId warehouse,
            ManagerId manager,
            ContractDate createdAt,
            ScheduledDate scheduledFor)
        {
            Id = id;
            Warehouse = warehouse;
            Manager = manager;
            CreatedAt = createdAt;
            ScheduledFor = scheduledFor;
            Status = ContractStatus.Pending;
        }

        /// <summary>
        /// Фабричный метод для создания нового контракта.
        /// </summary>
        /// <param name="contractGuid">GUID контракта.</param>
        /// <param name="warehouseGuid">GUID склада.</param>
        /// <param name="managerGuid">GUID менеджера.</param>
        /// <param name="scheduledFor">Запланированная дата выгрузки.</param>
        /// <returns>Экземпляр UnloadingContract.</returns>
        public static UnloadingContract Create(
            Guid contractGuid,
            Guid warehouseGuid,
            Guid managerGuid,
            DateTime scheduledFor)
        {
            return new UnloadingContract(
                ContractId.Of(contractGuid),
                WarehouseId.Of(warehouseGuid),
                ManagerId.Of(managerGuid),
                ContractDate.Now(),
                ScheduledDate.Of(scheduledFor));
        }

        /// <summary>
        /// Добавляет товарную позицию в контракт или увеличивает количество существующей.
        /// </summary>
        /// <param name="productGuid">GUID товара.</param>
        /// <param name="quantity">Количество единиц товара.</param>
        /// <exception cref="InvalidOperationException">
        /// Если контракт не в статусе Pending.
        /// </exception>
        public void AddLine(Guid productGuid, int quantity)
        {
            if (Status != ContractStatus.Pending)
                throw new UnloadingContractException("Нельзя добавлять линии в не-Pending контракт.");

            var existing = _lines.FirstOrDefault(x => x.Product == ProductId.Of(productGuid));
            if (existing is not null)
                existing.IncreaseQuantity(quantity);
            else
                _lines.Add(UnloadingLine.Create(
                    Guid.NewGuid(),
                    ProductId.Of(productGuid),
                    Quantity.Of(quantity)));
        }

        /// <summary>
        /// Переносит контракт на новую запланированную дату.
        /// </summary>
        /// <param name="newDate">Новая дата выгрузки.</param>
        /// <exception cref="InvalidOperationException">
        /// Если контракт не в статусе Pending.
        /// </exception>
        public void Reschedule(DateTime newDate)
        {
            if (Status != ContractStatus.Pending)
                throw new UnloadingContractException("Можно перенести только Pending контракт.");
            ScheduledFor = ScheduledDate.Of(newDate);
        }

        /// <summary>
        /// Подтверждает начало выгрузки и переводит контракт в статус InProgress.
        /// </summary>
        /// <exception cref="UnloadingContractException">
        /// Если контракт не в статусе Pending или отсутствуют позиции.
        /// </exception>
        public void Start()
        {
            if (Status != ContractStatus.Pending)
                throw new UnloadingContractException("Контракт уже запущен или завершён.");
            if (_lines.Count == 0)
                throw new UnloadingContractException("Нельзя начать выгрузку без линий.");
            Status = ContractStatus.InProgress;
        }

        /// <summary>
        /// Завершает контракт, переводя его в статус Completed.
        /// </summary>
        /// <exception cref="UnloadingContractException">
        /// Если контракт не в статусе InProgress.
        /// </exception>
        public void Complete()
        {
            if (Status != ContractStatus.InProgress)
                throw new UnloadingContractException("Контракт еще не запущен или уже завершён.");

            Status = ContractStatus.Completed;
        }

        /// <summary>
        /// Отменяет контракт и переводит его в статус Cancelled.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Если контракт уже завершён.
        /// </exception>
        public void Cancel()
        {
            if (Status == ContractStatus.Completed)
                throw new UnloadingContractException("Нельзя отменить уже завершённый контракт.");
            Status = ContractStatus.Cancelled;
        }
    }
}
