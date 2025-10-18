using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Warehouse.ContractProcessing.Infrastructures.Common.Contexts
{
    /// <summary>
    /// Фабрика для создания контекста базы данных <see cref="ContractProcessingDbContext"/> в режиме проектирования.
    /// </summary>
    public class ContractProcessingDbContextFactory
    : IDesignTimeDbContextFactory<ContractProcessingDbContext>
    {
        /// <summary>
        /// Создаёт экземпляр контекста базы данных <see cref="ContractProcessingDbContext"/> в режиме проектирования.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public ContractProcessingDbContext CreateDbContext(string[] args)
        {
            var connectionString = Environment.GetEnvironmentVariable("CONTRACT_DB_CONNECTION")
                ?? "Host=localhost;Port=5432;Database=contractdb;Username=postgres;Password=postgres";

            var options = new DbContextOptionsBuilder<ContractProcessingDbContext>()
                .UseNpgsql(connectionString)
                .Options;

            return new ContractProcessingDbContext(options);
        }
    }
}
