using AspireOrchestrator.Orchestrator.DataAccess;
using AspireOrchestrator.Orchestrator.Interfaces;
using AspireOrchestrator.Orchestrator.WebApi.Services;
using Azure.Messaging.ServiceBus;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddDbContextPool<OrchestratorContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("orchestratordb"), sqlOptions =>
    {
        // Workaround for https://github.com/dotnet/aspire/issues/1023
        //sqlOptions.ExecutionStrategy(c => new RetryingSqlServerRetryingExecutionStrategy(c));
    }));
builder.EnrichSqlServerDbContext<OrchestratorContext>(settings =>
    // Disable Aspire default retries as we're using a custom execution strategy
    settings.DisableRetry = true);

builder.Services.AddScoped<IFlowRepository, FlowRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<TenantRepository>();

builder.AddAzureServiceBusClient(connectionName: "servicebus");
builder.Services.AddScoped<EventPublisherService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline and seed database.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger(c => { c.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi2_0; });
    app.UseSwaggerUI();

    //using (var scope = app.Services.CreateScope())
    //{
    //    var context = scope.ServiceProvider.GetRequiredService<OrchestratorContext>();
    //    context.Database.EnsureCreated();
    //}
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
