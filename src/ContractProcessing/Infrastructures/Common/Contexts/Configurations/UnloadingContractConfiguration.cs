using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.ContractProcessing.Domain.Aggregates;
using Warehouse.ContractProcessing.Domain.ValueObjects;

namespace Warehouse.ContractProcessing.Infrastructures.Common.Contexts.Configurations
{
    /// <summary>
    /// Конфигурация агрегата <see cref="UnloadingContract"/>.
    /// </summary>
    public class UnloadingContractConfiguration : IEntityTypeConfiguration<UnloadingContract>
    {
        /// <summary>
        /// Конфигурирует модель агрегата <see cref="UnloadingContract"/> в контексте базы данных.
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<UnloadingContract> builder)
        {
            builder.ToTable("UnloadingContracts");

            // Первичный ключ
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .HasConversion(id => id.Value, value => ContractId.Of(value))
                .ValueGeneratedNever();

            // Value Object: WarehouseId
            builder.Property(c => c.Warehouse)
                .HasConversion(w => w.Value, v => WarehouseId.Of(v))
                .IsRequired();

            // Value Object: ManagerId
            builder.Property(c => c.Manager)
                .HasConversion(m => m.Value, v => ManagerId.Of(v))
                .IsRequired();

            builder.Property(c => c.CreatedAt)
                .HasConversion(
                    cd => cd.Value,
                    dt => new ContractDate(dt))
                .HasColumnName("CreatedAt")
                .IsRequired();

            builder.Property(c => c.ScheduledFor)
                .HasConversion(
                    sd => sd.Value,
                    dt => new ScheduledDate(dt))
                .HasColumnName("ScheduledFor")
                .IsRequired();

            // Статус
            builder.Property(c => c.Status)
                .HasConversion<int>()
                .IsRequired();

            // Навигация на линии контракта
            builder.HasMany(c => c.Lines)
                   .WithOne()
                   .HasForeignKey("ContractId");

            builder.Navigation(c => c.Lines)
                   .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
