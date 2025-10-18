using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.ContractProcessing.Domain.Aggregates;
using Warehouse.ContractProcessing.Domain.Entity;
using Warehouse.ContractProcessing.Domain.ValueObjects;

namespace Warehouse.ContractProcessing.Infrastructures.Common.Contexts.Configurations
{
    /// <summary>
    /// Конфигурация сущности <see cref="UnloadingLine"/>.
    /// </summary>
    public class UnloadingLineConfiguration : IEntityTypeConfiguration<UnloadingLine>
    {
        /// <summary>
        /// Конфигурирует модель сущности <see cref="UnloadingLine"/> в контексте базы данных.
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<UnloadingLine> builder)
        {
            builder.ToTable("UnloadingLines");

            // Первичный ключ
            builder.HasKey(l => l.Id);
            builder.Property(l => l.Id)
                   .ValueGeneratedNever();

            // Value Object: ProductId
            builder.Property(l => l.Product)
                   .HasConversion(p => p.Value, v => ProductId.Of(v))
                   .IsRequired();

            //Quantity 
            builder.Property(l => l.Quantity)
                .HasConversion(
                    q => q.Value,          // в БД храним целое
                    v => Quantity.Of(v))   // при чтении оборачиваем в struct
                .HasColumnName("Quantity")
                .IsRequired();

            // Внешний ключ на UnloadingContract
            builder.Property<ContractId>("ContractId")
                .HasColumnName("ContractId")
                .HasConversion(
                    id => id.Value,            // в БД Guid
                    value => ContractId.Of(value)) // из БД обратно VO
                .IsRequired();
        }
    }
}
