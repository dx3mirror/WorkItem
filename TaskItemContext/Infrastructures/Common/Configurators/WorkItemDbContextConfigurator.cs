using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Utilities.DbContextSettings.Configurators;
using WorkItemContext.Infrastructures.Contexts;

namespace WorkItemContext.Infrastructures.Common.Configurators
{
    public class WorkItemDbContextConfigurator(IConfiguration configuration, ILoggerFactory loggerFactory)
        : BaseDbContextConfigurator<WorkItemDbContext>(configuration, loggerFactory)
    {
        /// <inheritdoc/>
        protected override string ConnectionStringName => "TaskItemDb";
    }
}
