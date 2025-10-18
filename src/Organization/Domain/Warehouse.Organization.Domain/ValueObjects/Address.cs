using Warehouse.Organization.Domain.Exceptions;

namespace Warehouse.Organization.Domain.ValueObjects
{
    /// <summary>
    /// Объект-значение, представляющий полный почтовый адрес.
    /// </summary>
    public sealed class Address : IEquatable<Address>
    {
        /// <summary>
        /// Страна.
        /// </summary>
        public string Country { get; }

        /// <summary>
        /// Регион или штат.
        /// </summary>
        public string Region { get; }

        /// <summary>
        /// Город.
        /// </summary>
        public string City { get; }

        /// <summary>
        /// Улица.
        /// </summary>
        public string Street { get; }

        /// <summary>
        /// Номер здания.
        /// </summary>
        public string BuildingNumber { get; }

        private Address(string country, string region, string city, string street, string buildingNumber)
        {
            Country = country;
            Region = region;
            City = city;
            Street = street;
            BuildingNumber = buildingNumber;
        }

        /// <summary>
        /// Создаёт экземпляр <see cref="Address"/>, валидируя каждое поле.
        /// </summary>
        /// <param name="country">Страна.</param>
        /// <param name="region">Регион или штат.</param>
        /// <param name="city">Город.</param>
        /// <param name="street">Улица.</param>
        /// <param name="buildingNumber">Номер здания.</param>
        /// <returns>Новый экземпляр <see cref="Address"/>.</returns>
        public static Address Of(string country, string region, string city, string street, string buildingNumber)
        {
            if (string.IsNullOrWhiteSpace(country))
                throw new WarehouseInfrastructureException("Country is required.");
            if (string.IsNullOrWhiteSpace(region))
                throw new WarehouseInfrastructureException("Region is required.");
            if (string.IsNullOrWhiteSpace(city))
                throw new WarehouseInfrastructureException("City is required.");
            if (string.IsNullOrWhiteSpace(street))
                throw new WarehouseInfrastructureException("Street is required.");
            if (string.IsNullOrWhiteSpace(buildingNumber))
                throw new WarehouseInfrastructureException("Building number is required.");

            return new Address(
                country.Trim(),
                region.Trim(),
                city.Trim(),
                street.Trim(),
                buildingNumber.Trim());
        }

        /// <summary>
        /// Проверяет, равен ли текущий объект другому объекту <see cref="Address"/>.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj) => Equals(obj as Address);

        /// <summary>
        /// Проверяет, равен ли текущий объект другому объекту <see cref="Address"/> по значению.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Address? other)
        {
            if (other is null) return false;
            return Country == other.Country &&
                   Region == other.Region &&
                   City == other.City &&
                   Street == other.Street &&
                   BuildingNumber == other.BuildingNumber;
        }

        /// <summary>
        /// Получает хэш-код для адреса.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() =>
            HashCode.Combine(Country, Region, City, Street, BuildingNumber);

        /// <summary>
        /// Возвращает строковое представление адреса в формате:
        /// </summary>
        /// <returns></returns>
        public override string ToString() =>
            $"{Street}, {BuildingNumber}, {City}, {Region}, {Country}";

        /// <summary>
        /// Переопределяет оператор равенства для сравнения двух адресов.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Address? left, Address? right) => Equals(left, right);

        /// <summary>
        /// Переопределяет оператор неравенства для сравнения двух адресов.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Address? left, Address? right) => !Equals(left, right);
    }
}
