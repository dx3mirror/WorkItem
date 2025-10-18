using MassTransit;
using Microsoft.EntityFrameworkCore;
using Utilities.DbContextSettings;
using Warehouse.ContractProcessing.Applications.AppServices;
using Warehouse.ContractProcessing.Applications.AppServices.Abstract;
using Warehouse.ContractProcessing.Applications.Handlers.Command.CreateUnloadingContract;
using Warehouse.ContractProcessing.Applications.Handlers.Command.StartUnloadingContact;
using Warehouse.ContractProcessing.Infrastructures.Common.Configurators;
using Warehouse.ContractProcessing.Infrastructures.Common.Contexts;
using Warehouse.ContractProcessing.Infrastructures.Sagas;
using Warehouse.ContractProcessing.Сontract.Events;
using Wolverine;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataAccess<ContractProcessingDbContext, ContractProcessingDbContextConfigurator>();
builder.Services.AddScoped<IUnloadingContractService, UnloadingContractService>();
var connectionString = builder.Configuration.GetConnectionString("ContractProcessingDb");
if (string.IsNullOrWhiteSpace(connectionString))
    throw new InvalidOperationException("Connection string 'ContractProcessingDb' is not configured.");

builder.Services.AddMassTransit(x =>
{
    x.AddSagaStateMachine<UnloadingSaga, UnloadingSagaState>()
     .EntityFrameworkRepository(r =>
     {
         r.ConcurrencyMode = ConcurrencyMode.Optimistic;
         r.AddDbContext<DbContext, ContractProcessingDbContext>((provider, cfg) =>
         {
             cfg.UseNpgsql(connectionString,
                 npgsql => npgsql.MigrationsAssembly(typeof(ContractProcessingDbContext).Assembly.GetName().Name));
         });
     });

    x.UsingInMemory((context, cfg) =>
    {
        cfg.ConfigureEndpoints(context);
    });

    x.AddRider(rider =>
    {
        rider.AddSagaStateMachine<UnloadingSaga, UnloadingSagaState>()
             .EntityFrameworkRepository(r =>
             {
                 r.ConcurrencyMode = ConcurrencyMode.Optimistic;
                 r.AddDbContext<DbContext, ContractProcessingDbContext>((provider, cfg) =>
                 {
                     cfg.UseNpgsql(connectionString,
                         npgsql => npgsql.MigrationsAssembly(typeof(ContractProcessingDbContext).Assembly.GetName().Name));
                 });
             });

        rider.AddProducer<UnloadingStartedEvent>("unloading-started");
        rider.AddProducer<UnloadingCompletedEvent>("unloading-completed");
        rider.AddProducer<UnloadingCancelledEvent>("unloading-cancelled");

        // 3.2 Настройка Kafka транспорта
        rider.UsingKafka((context, k) =>
        {
            var kafkaServers = builder.Configuration["Kafka:BootstrapServers"]
                                ?? throw new InvalidOperationException("Kafka:BootstrapServers not configured");

            k.Host(kafkaServers);

            k.TopicEndpoint<UnloadingStartedEvent>("unloading-started", "contract-processing", e =>
            {
                e.ConfigureSaga<UnloadingSagaState>(context);
            });

            k.TopicEndpoint<UnloadingCompletedEvent>("unloading-completed", "contract-processing", e =>
            {
                e.ConfigureSaga<UnloadingSagaState>(context);
            });

            k.TopicEndpoint<UnloadingCancelledEvent>("unloading-cancelled", "contract-processing", e =>
            {
                e.ConfigureSaga<UnloadingSagaState>(context);
            });
        });
    });
});

builder.Services.AddWolverine(opts =>
{
    opts.Discovery.IncludeAssembly(typeof(CreateUnloadingContractHandler).Assembly);
    opts.Discovery.IncludeAssembly(typeof(StartUnloadingContractCommandHandler).Assembly);
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
