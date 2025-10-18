using Microsoft.Extensions.Hosting;
using Wolverine;
using Wolverine.FluentValidation;

namespace Utilities.WolverineSettings
{
    /// <summary>
    /// Расширения для конфигурации Wolverine.
    /// </summary>
    public static class WolverineConfigurationExtensions
    {
        /// <summary>
        /// Автоматически настраивает Wolverine для микросервиса.
        /// </summary>
        /// <typeparam name="TMarker">Тип-маркер для поиска сборки обработчиков команд.</typeparam>
        /// <param name="builder">Билдер приложения.</param>
        /// <returns>Обновлённый хост билдера.</returns>
        public static IHostApplicationBuilder AddWolverineWithDefaults<TMarker>(this IHostApplicationBuilder builder)
        {
            builder.Services.AddWolverine(opts =>
            {
                opts.Policies.AutoApplyTransactions();

                var assembly = typeof(TMarker).Assembly;
                opts.Discovery.IncludeAssembly(assembly);

                opts.UseFluentValidation();
            });

            return builder;
        }
    }
}
