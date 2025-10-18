using Warehouse.Organization.Domain.ValueObjects;

namespace Warehouse.Organization.Domain.Entities
{
    /// <summary>
    /// Секция склада, представляющая собой определённую зону хранения.
    /// </summary>
    public class StorageSection
    {
        /// <summary>
        /// Уникальный идентификатор секции.
        /// </summary>
        public SectionId Id { get; }

        /// <summary>
        /// Уникальный код секции.
        /// </summary>
        public SectionCode Code { get; }

        /// <summary>
        /// Площадь секции в квадратных метрах.
        /// </summary>
        public Area Area { get; private set; }

        /// <summary>
        /// Приватный конструктор для создания экземпляра <see cref="StorageSection"/>.
        /// Используется фабричным методом <see cref="Create"/>.
        /// </summary>
        /// <param name="id">Идентификатор секции.</param>
        /// <param name="code">Код секции.</param>
        /// <param name="area">Площадь секции.</param>
        private StorageSection(SectionId id, SectionCode code, Area area)
        {
            Id = id;
            Code = code;
            Area = area;
        }

        /// <summary>
        /// Фабричный метод для создания новой секции склада.
        /// </summary>
        /// <param name="id">Идентификатор секции.</param>
        /// <param name="code">Код секции.</param>
        /// <param name="area">Площадь секции.</param>
        /// <returns>Экземпляр <see cref="StorageSection"/>.</returns>
        public static StorageSection Create(SectionId id, SectionCode code, Area area)
        {
            return new StorageSection(id, code, area);
        }

        /// <summary>
        /// Изменяет площадь секции.
        /// </summary>
        /// <param name="newArea">Новая площадь секции.</param>
        public void Resize(Area newArea)
        {
            Area = newArea;
        }
    }
}
