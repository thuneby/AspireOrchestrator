using AspireOrchestrator.MessagingWorker;
using AspireOrchestrator.MessagingWorker.Services;

var builder = Host.CreateApplicationBuilder(args);

builder.AddAzureServiceBusClient(connectionName: "servicebus");

builder.AddServiceDefaults();

builder.Services.AddHostedService<Worker>();

builder.Services.AddHttpClient<EventService>(
    static client => client.BaseAddress = new ("https+http://orchestratorapi"));

var host = builder.Build();
host.Run();
