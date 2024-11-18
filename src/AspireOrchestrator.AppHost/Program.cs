using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


var builder = DistributedApplication.CreateBuilder(args);

var sqlserver = builder.AddSqlServer("sqlserver")
    .WithLifetime(ContainerLifetime.Persistent);

var orchestratordb = sqlserver.AddDatabase("orchestratordb");

var migrationservice = builder.AddProject<Projects.AspireOrchestrator_DatabaseMigrations>("migration")
    .WithReference(orchestratordb)
    .WaitFor(orchestratordb);

builder.AddProject<Projects.AspireOrchestrator_Orchestrator_WebApi>("orchestrator")
    .WithReference(orchestratordb)
    .WaitForCompletion(migrationservice);


builder.Build().Run();
