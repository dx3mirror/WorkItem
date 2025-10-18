using MassTransit;
using MassTransit.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Utilities.MassTransitSettings.Configurations;

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
    /// <param name="rabbitMqConfiguration">Строка подключения к БД.</param>
    /// <returns>Обновлённая коллекция сервисов.</returns>
    public static IServiceCollection AddMassTransitSaga<TStateMachine, TState, TSagaDbContext>(
        this IServiceCollection services,
        string connectionString,
        RabbitMqConfiguration rabbitMqConfiguration)
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
            services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();

                x.AddSagaStateMachine<TStateMachine, TState>();

                x.SetSagaRepositoryProvider(
                    new EntityFrameworkSagaRepositoryRegistrationProvider(cfg =>
                    {
                        cfg.ExistingDbContext<TSagaDbContext>();
                    }));

                x.AddEntityFrameworkOutbox<TSagaDbContext>(o =>
                {
                    o.QueryDelay = TimeSpan.FromSeconds(30);
                    o.UsePostgres().UseBusOutbox();
                });

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri(rabbitMqConfiguration.Host), h =>
                    {
                        h.Username(rabbitMqConfiguration.Username);
                        h.Password(rabbitMqConfiguration.Password);
                    });

                    cfg.UseMessageRetry(r => r.Exponential(10, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(60), TimeSpan.FromSeconds(5)));

                    cfg.ConfigureEndpoints(context);
                });
            });

        });

        return services;
    }
}
