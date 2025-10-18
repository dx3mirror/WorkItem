using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Utilities.DbContextSettings.Configurations;

namespace Utilities.DbContextSettings.Configurators
{
    /// <summary>
    /// Базовый класс конфигуратора DbContext.
    /// </summary>
    public abstract class BaseDbContextConfigurator<TDbContext>(
        IConfiguration configuration,
        ILoggerFactory loggerFactory)
        : IDbContextOptionsConfigurator<TDbContext>
        where TDbContext : DbContext
    {
        /// <summary>
        /// Имя строки подключения.
        /// </summary>
        protected abstract string ConnectionStringName { get; }

        /// <inheritdoc/>
        public void Configure(DbContextOptionsBuilder<TDbContext> options)
        {
            var connectionString = configuration.GetConnectionString(ConnectionStringName)
                ?? throw new InvalidOperationException($"Connection string '{ConnectionStringName}' not found.");

            options
                .UseLoggerFactory(loggerFactory)
                .UseNpgsql(connectionString, npgsqlOptions =>
                {
                    npgsqlOptions.CommandTimeout(60);
                    npgsqlOptions.EnableRetryOnFailure();
                })
                .EnableSensitiveDataLogging(); // Для дебага, потом можно отключить на проде
        }
    }
}
