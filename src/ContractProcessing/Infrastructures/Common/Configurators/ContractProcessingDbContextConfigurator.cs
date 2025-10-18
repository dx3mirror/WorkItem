using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Utilities.DbContextSettings.Configurators;
using Warehouse.ContractProcessing.Infrastructures.Common.Contexts;

namespace Warehouse.ContractProcessing.Infrastructures.Common.Configurators
{
    /// <summary>
    /// Конфигуратор DbContext для обработки контрактов разгрузки.
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="loggerFactory"></param>
    public class ContractProcessingDbContextConfigurator(
        IConfiguration configuration,
        ILoggerFactory loggerFactory)
                : BaseDbContextConfigurator<ContractProcessingDbContext>(configuration, loggerFactory)
    {

        /// <inheritdoc />
        protected override string ConnectionStringName => "ContractProcessingDb";
    }
}
