using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace WorkItemContext.Infrastructures.Contexts
{
    public class WorkItemDbContext(DbContextOptions<WorkItemDbContext> options) : DbContext(options)
    {
        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            CustomModelBuilder.OnModelCreating(modelBuilder);
        }
    }
}
