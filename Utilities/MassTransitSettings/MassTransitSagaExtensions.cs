using MassTransit;
using MassTransit.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Utilities.MassTransitSettings;

/// <summary>
/// Расширения для настройки MassTransit с поддержкой саг и Entity Framework.
/// </summary>
public static class MassTransitSagaExtensions
{
    /// <summary>
    /// Регистрирует сагу в MassTransit с использованием Entity Framework репозитория.
    /// </summary>
    /// <typeparam name="TStateMachine">Тип стейт-машины саги.</typeparam>
    /// <typeparam name="TState">Тип состояния саги.</typeparam>
    /// <typeparam name="TSagaDbContext">Тип DbContext для хранения состояния саги.</typeparam>
    /// <param name="services">Коллекция сервисов.</param>
    /// <param name="connectionString">Строка подключения к БД.</param>
    /// <returns>Обновлённая коллекция сервисов.</returns>
    public static IServiceCollection AddMassTransitSaga<TStateMachine, TState, TSagaDbContext>(
        this IServiceCollection services,
        string connectionString)
        where TStateMachine : MassTransitStateMachine<TState>
        where TState : class, SagaStateMachineInstance
        where TSagaDbContext : DbContext
    {
        services.AddDbContext<TSagaDbContext>(options =>
        {
            options.UseNpgsql(connectionString, npgsql =>
            {
                npgsql.MigrationsAssembly(typeof(TSagaDbContext).Assembly.GetName().Name);
            });
        });

        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();

            x.AddSagaStateMachine<TStateMachine, TState>();

            x.SetSagaRepositoryProvider(
                new EntityFrameworkSagaRepositoryRegistrationProvider(cfg =>
                {
                    cfg.ExistingDbContext<TSagaDbContext>();
                }));

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}
