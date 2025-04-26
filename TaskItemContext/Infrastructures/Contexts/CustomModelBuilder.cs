using Microsoft.EntityFrameworkCore;
using Utilities.DbContextSettings.Configurations;
using WorkItemContext.Infrastructures.Contexts.Configurations;

namespace WorkItemContext.Infrastructures.Contexts
{
    /// <summary>
    /// Сборщик моделей.
    /// </summary>
    public static class CustomModelBuilder
    {
        /// <summary>
        /// Создает модель.
        /// </summary>
        /// <param name="modelBuilder">Билдер.</param>
        public static void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureForecastModels(modelBuilder);

            ModelBuilderExtension.SetDefaultDateTimeKind(modelBuilder, DateTimeKind.Utc);
        }

        private static void ConfigureForecastModels(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");

            modelBuilder.ApplyConfiguration(new WorkItemConfiguration());
            modelBuilder.ApplyConfiguration(new WorkItemCommentConfiguration());
            modelBuilder.ApplyConfiguration(new WorkItemStateConfiguration());
        }
    }
}
