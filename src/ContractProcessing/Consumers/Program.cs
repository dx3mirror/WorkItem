using MassTransit;
using Microsoft.Extensions.Hosting;
using Warehouse.ContractProcessing.ConsumerServices.Consumers;
using Warehouse.ContractProcessing.Сontract.Consumers;
using Warehouse.ContractProcessing.Сontract.Events;
using Warehouse.ContractProcessing.Сontract.Topics;

var host = Host.CreateDefaultBuilder(args)

    .ConfigureServices((context, services) =>
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<UnloadingErrorEventConsumer>();
            x.AddConsumer<UnloadingCompletedEventConsumer>();

            x.AddRider(rider =>
            {
                rider.AddConsumer<UnloadingErrorEventConsumer>();
                rider.AddConsumer<UnloadingCompletedEventConsumer>();

                rider.UsingKafka((ctx, k) =>
                {
                    var kafkaHost = context.Configuration["Kafka:BootstrapServers"] ?? "localhost:9092";
                    k.Host(kafkaHost);

                    k.TopicEndpoint<UnloadingErrorEvent>(KafkaTopics.UnloadingError, KafkaConsumerGroups.ConsumersLog, e =>
                    {
                        e.ConfigureConsumer<UnloadingErrorEventConsumer>(ctx);
                    });

                    k.TopicEndpoint<UnloadingCompletedEvent>(KafkaTopics.UnloadingCompleted, KafkaConsumerGroups.ConsumersLog, e =>
                    {
                        e.ConfigureConsumer<UnloadingCompletedEventConsumer>(ctx);
                    });
                });
            });
        });
    })
    .Build();

await host.RunAsync();
