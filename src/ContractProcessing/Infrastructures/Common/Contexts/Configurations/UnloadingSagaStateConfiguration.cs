using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Warehouse.ContractProcessing.Infrastructures.Sagas;

namespace Warehouse.ContractProcessing.Infrastructures.Common.Contexts.Configurations
{
    /// <summary>
    /// Конфигурация состояния саги <see cref="UnloadingSagaState"/>.
    /// </summary>
    public class UnloadingSagaStateConfiguration : IEntityTypeConfiguration<UnloadingSagaState>
    {
        /// <summary>
        /// Конфигурирует модель <see cref="UnloadingSagaState"/> для базы данных.
        /// </summary>
        /// <param name="builder">Конфигуратор типа сущности.</param>
        public void Configure(EntityTypeBuilder<UnloadingSagaState> builder)
        {
            // Установка первичного ключа
            builder.HasKey(x => x.CorrelationId);

            // Настройка таблицы
            builder.ToTable("UnloadingSagaStates");

            // Свойства
            builder.Property(x => x.CurrentState).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.StartedAt);
            builder.Property(x => x.CompletedAt);
            builder.Property(x => x.CancelledAt);
            builder.Property(x => x.WarehouseId).IsRequired();
            builder.Property(x => x.ManagerId).IsRequired();
            builder.Property(x => x.ScheduledFor).IsRequired();
            builder.Property(x => x.LinesCount).IsRequired();
        }
    }
}
