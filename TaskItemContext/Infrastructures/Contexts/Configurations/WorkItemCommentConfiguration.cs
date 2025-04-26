using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TaskExample.Domain.Entities;

namespace WorkItemContext.Infrastructures.Contexts.Configurations
{
    /// <summary>
    /// Конфигурация сущности <see cref="WorkItemComment"/>
    /// </summary>
    public class WorkItemCommentConfiguration : IEntityTypeConfiguration<WorkItemComment>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<WorkItemComment> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Author)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Content)
                .IsRequired()
                .HasMaxLength(2000);

            builder.Property(c => c.CreatedAt)
                .IsRequired();
        }
    }
}
