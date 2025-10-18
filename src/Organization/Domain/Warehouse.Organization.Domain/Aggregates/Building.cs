using Warehouse.Organization.Domain.ValueObjects;

namespace Warehouse.Organization.Domain.Aggregates
{
    /// <summary>
    /// Здание, в котором расположены складские помещения.
    /// </summary>
    public class Building
    {
        /// <summary>
        /// Уникальный идентификатор здания.
        /// </summary>
        public BuildingId Id { get; }

        /// <summary>
        /// Адрес, по которому расположено здание.
        /// </summary>
        public Address Address { get; }

        /// <summary>
        /// Общее количество этажей в здании.
        /// </summary>
        public FloorCount TotalFloors { get; }

        /// <summary>
        /// Приватный конструктор для создания экземпляра здания.
        /// </summary>
        /// <param name="id">Идентификатор здания.</param>
        /// <param name="address">Адрес здания.</param>
        /// <param name="totalFloors">Количество этажей в здании.</param>
        private Building(BuildingId id, Address address, FloorCount totalFloors)
        {
            Id = id;
            Address = address;
            TotalFloors = totalFloors;
        }

        /// <summary>
        /// Фабричный метод для создания нового здания.
        /// </summary>
        /// <param name="id">GUID здания.</param>
        /// <param name="country">Страна, в которой расположено здание.</param>
        /// <param name="region">Регион или область.</param>
        /// <param name="city">Город.</param>
        /// <param name="street">Улица.</param>
        /// <param name="buildingNumber">Номер здания.</param>
        /// <param name="totalFloors">Количество этажей.</param>
        /// <returns>Новый экземпляр <see cref="Building"/>.</returns>
        public static Building Create(
            Guid id,
            string country,
            string region,
            string city,
            string street,
            string buildingNumber,
            int totalFloors)
        {
            return new Building(
                BuildingId.Of(id),
                Address.Of(country, region, city, street, buildingNumber),
                FloorCount.Of(totalFloors));
        }
    }
}
