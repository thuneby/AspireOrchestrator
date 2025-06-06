using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


var builder = DistributedApplication.CreateBuilder(args);

var sqlserver = builder.AddSqlServer("sqlserver", port: 63015)
    .WithLifetime(ContainerLifetime.Persistent);

var orchestratordb = sqlserver.AddDatabase("orchestratordb");

var migrationservice = builder.AddProject<Projects.AspireOrchestrator_DatabaseMigrations>("migration")
    .WithReference(orchestratordb)
    .WaitFor(orchestratordb);

var serviceBus = builder.AddConnectionString("servicebus");

//var serviceBus = builder.AddAzureServiceBus("servicebus")
//    .RunAsEmulator();

//var topic = serviceBus.AddServiceBusTopic("events");
//topic.AddServiceBusSubscription("eventsubscription")
//    .WithProperties(subscription => subscription.MaxDeliveryCount = 10);

var apiservice = builder.AddProject<Projects.AspireOrchestrator_Orchestrator_WebApi>("orchestratorapi")
    .WithReference(orchestratordb)
    .WithReference(serviceBus)
    .WaitForCompletion(migrationservice);


builder.AddProject<Projects.AspireOrchestrator_MessagingWorker>("messagingworker")
    .WithReference(serviceBus)
    .WithReference(apiservice)
    .WaitFor(apiservice);


builder.Build().Run();
