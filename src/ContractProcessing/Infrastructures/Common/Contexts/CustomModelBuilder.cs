using Microsoft.EntityFrameworkCore;
using Utilities.DbContextSettings.Configurations;
using Warehouse.ContractProcessing.Domain.ValueObjects;
using Warehouse.ContractProcessing.Infrastructures.Common.Contexts.Configurations;

namespace Warehouse.ContractProcessing.Infrastructures.Common.Contexts
{
    /// <summary>
    /// Сборщик моделей.
    /// </summary>
    public static class CustomModelBuilder
    {
        /// <summary>
        /// Конфигурирует модель EF Core для микросервиса обработки контрактов выгрузки и инфраструктуры.
        /// </summary>
        /// <param name="modelBuilder">Конфигуратор модели EF Core.</param>
        public static void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ContractDate>();
            modelBuilder.Ignore<ScheduledDate>();

            // Применяем конфигурации доменных агрегатов
            modelBuilder.ApplyConfiguration(new UnloadingContractConfiguration());
            modelBuilder.ApplyConfiguration(new UnloadingLineConfiguration());
            modelBuilder.ApplyConfiguration(new UnloadingSagaStateConfiguration());

            // Настройка типов DateTime по умолчанию в UTC
            ModelBuilderExtension.SetDefaultDateTimeKind(modelBuilder, DateTimeKind.Utc);
        }
    }
}
