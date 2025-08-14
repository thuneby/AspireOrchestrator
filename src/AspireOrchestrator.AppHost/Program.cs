var builder = DistributedApplication.CreateBuilder(args);

var sqlserver = builder.AddSqlServer("sqlserver", port: 63015)
    .WithLifetime(ContainerLifetime.Persistent);

var orchestratordb = sqlserver.AddDatabase("orchestratordb");

var migrationservice = builder.AddProject<Projects.AspireOrchestrator_DatabaseMigrations>("migration")
    .WithReference(orchestratordb)
    .WaitFor(orchestratordb);

// Existing Azure Service Bus
//var serviceBus = builder.AddConnectionString("servicebus");

// Local emulator for Azure Service Bus
var serviceBus = builder.AddAzureServiceBus("servicebus")
    .RunAsEmulator();

var topic = serviceBus.AddServiceBusTopic("events");
topic.AddServiceBusSubscription("eventsubscription")
    .WithProperties(subscription => subscription.MaxDeliveryCount = 5)
    .WithProperties(subscription => subscription.DefaultMessageTimeToLive = TimeSpan.FromMinutes(10))
    .WithProperties(subscription => subscription.LockDuration = TimeSpan.FromMinutes(5));

var blobs = builder.AddAzureStorage("storage").RunAsEmulator(
    azurite =>
    {
        azurite.WithLifetime(ContainerLifetime.Persistent);
    })
    .AddBlobs("blobs");

var domaindb = sqlserver.AddDatabase("domaindb");

var domainmigrationservice = builder.AddProject<Projects.AspireOrchestrator_Domain_DatabaseMigrations>("domainmigration")
    .WithReference(domaindb)
    .WaitFor(domaindb);

var apiservice = builder.AddProject<Projects.AspireOrchestrator_Orchestrator_WebApi>("orchestratorapi")
    .WithReference(orchestratordb)
    .WithReference(serviceBus)
    .WaitForCompletion(migrationservice);


builder.AddProject<Projects.AspireOrchestrator_MessagingWorker>("messagingworker")
    .WithReference(serviceBus)
    .WithReference(apiservice)
    .WaitFor(serviceBus)
    .WaitFor(apiservice);


builder.AddProject<Projects.AspireOrchestrator_Administration>("aspireorchestrator-administration")
    .WithReference(domaindb)
    .WaitForCompletion(domainmigrationservice)
    .WithReference(serviceBus)
    .WithReference(blobs)
    .WaitFor(blobs)
    .WithReference(apiservice);

builder.AddProject<Projects.AspireOrchestrator_Parsing_WebApi>("aspireorchestrator-parsing-webapi")
    .WithReference(domaindb)
    .WaitForCompletion(domainmigrationservice)
    .WithReference(blobs)
    .WaitFor(blobs);


builder.Build().Run();
