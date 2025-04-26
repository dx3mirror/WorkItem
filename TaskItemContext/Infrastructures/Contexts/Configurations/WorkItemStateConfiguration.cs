using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TaskExample.Domain.Enums;
using WorkItemContext.Infrastructures.Sagas;

namespace WorkItemContext.Infrastructures.Contexts.Configurations
{
    /// <summary>
    /// Конфигурация сущности состояния саги <see cref="WorkItemState"/>.
    /// </summary>
    public class WorkItemStateConfiguration : IEntityTypeConfiguration<WorkItemState>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<WorkItemState> builder)
        {
            builder.HasKey(x => x.CorrelationId);

            builder.Property(x => x.CurrentState)
                .IsRequired()
                .HasMaxLength(64);

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.Property(x => x.Title)
                .HasMaxLength(250);

            builder.Property(x => x.Description)
                .HasMaxLength(2000);

            builder.Property(x => x.CompletedDate);

            builder.Property(x => x.CreatedAt)
                .IsRequired();
        }
    }
}
