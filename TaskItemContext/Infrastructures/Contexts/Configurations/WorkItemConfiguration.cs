using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkItem.Domain.Aggregates;

namespace WorkItemContext.Infrastructures.Contexts.Configurations
{
    /// <summary>
    /// Конфигурация сущности <see cref="WorkItemEffect"/>
    /// </summary>
    public class WorkItemConfiguration : IEntityTypeConfiguration<WorkItemEffect>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<WorkItemEffect> builder)
        {
            builder.HasKey(w => w.Id);

            builder.Property(w => w.UserId)
                .IsRequired();

            builder.Property(w => w.CreationDate)
                .IsRequired();

            builder.OwnsOne(w => w.Title, title =>
            {
                title.Property(t => t.Value)
                    .HasColumnName("Title")
                    .HasMaxLength(250);
            });

            builder.OwnsOne(w => w.Description, description =>
            {
                description.Property(d => d.Value)
                    .HasColumnName("Description")
                    .HasMaxLength(2000);
            });

            builder.Property(w => w.CompletedDate);

            builder.Property(w => w.Status)
                .HasConversion<int>()
                .IsRequired();

            builder.Metadata.FindNavigation(nameof(WorkItemEffect.Comments))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
