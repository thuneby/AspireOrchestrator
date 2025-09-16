using AspireOrchestrator.BackendSimulator;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddSingleton<BackendReplyQueueManager>();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
