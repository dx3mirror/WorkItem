using Warehouse.Organization.Domain.Entities;
using Warehouse.Organization.Domain.Exceptions;
using Warehouse.Organization.Domain.ValueObjects;

namespace Warehouse.Organization.Domain.Aggregates
{
    /// <summary>
    /// Складское помещение, расположенное в здании.
    /// Является агрегатом, содержащим секции хранения.
    /// </summary>
    public class StorageFacility
    {
        /// <summary>
        /// Уникальный идентификатор складского помещения.
        /// </summary>
        public StorageFacilityId Id { get; }

        /// <summary>
        /// Название или обозначение складского помещения.
        /// </summary>
        public StorageFacilityName Name { get; }

        /// <summary>
        /// Идентификатор здания, в котором находится склад.
        /// </summary>
        public BuildingId Building { get; }

        /// <summary>
        /// Номер этажа, на котором расположен склад.
        /// </summary>
        public FloorNumber Floor { get; }

        private readonly List<StorageSection> _sections = [];

        /// <summary>
        /// Коллекция секций, входящих в состав склада.
        /// </summary>
        public IReadOnlyCollection<StorageSection> Sections => _sections.AsReadOnly();

        /// <summary>
        /// Приватный конструктор для создания экземпляра <see cref="StorageFacility"/>.
        /// Используется фабричным методом <see cref="Create"/>.
        /// </summary>
        /// <param name="id">Идентификатор склада.</param>
        /// <param name="name">Название склада.</param>
        /// <param name="building">Идентификатор здания.</param>
        /// <param name="floor">Номер этажа.</param>
        private StorageFacility(StorageFacilityId id, StorageFacilityName name, BuildingId building, FloorNumber floor)
        {
            Id = id;
            Name = name;
            Building = building;
            Floor = floor;
        }

        /// <summary>
        /// Фабричный метод для создания нового экземпляра <see cref="StorageFacility"/>.
        /// </summary>
        /// <param name="id">GUID склада.</param>
        /// <param name="name">Название склада.</param>
        /// <param name="buildingId">GUID здания.</param>
        /// <param name="floor">Номер этажа.</param>
        /// <returns>Новый экземпляр <see cref="StorageFacility"/>.</returns>
        public static StorageFacility Create(Guid id, string name, Guid buildingId, int floor)
        {
            return new StorageFacility(
                StorageFacilityId.Of(id),
                StorageFacilityName.Of(name),
                BuildingId.Of(buildingId),
                FloorNumber.Of(floor));
        }

        /// <summary>
        /// Добавляет новую секцию на склад.
        /// </summary>
        /// <param name="sectionCode">Код секции.</param>
        /// <param name="area">Площадь секции в квадратных метрах.</param>
        /// <exception cref="WarehouseInfrastructureException">
        /// Если секция с таким кодом уже существует.
        /// </exception>
        public void AddSection(string sectionCode, double area)
        {
            var code = SectionCode.Of(sectionCode);
            if (_sections.Any(s => s.Code == code))
                throw new WarehouseInfrastructureException($"Section '{sectionCode}' already exists.");
            _sections.Add(StorageSection.Create(
                SectionId.Of(Guid.NewGuid()), code, Area.Of(area)));
        }
    }
}
