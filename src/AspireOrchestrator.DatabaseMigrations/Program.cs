using AspireOrchestrator.DatabaseMigrations;
using AspireOrchestrator.Orchestrator.DataAccess;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<OrchestratorDbInitializer>();

builder.AddServiceDefaults();

builder.Services.AddDbContextPool<OrchestratorContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("orchestratordb"), sqlOptions =>
    {
        sqlOptions.MigrationsAssembly("AspireOrchestrator.DatabaseMigrations");
        // Workaround for https://github.com/dotnet/aspire/issues/1023
        //sqlOptions.ExecutionStrategy(c => new RetryingSqlServerRetryingExecutionStrategy(c));
    }));
builder.EnrichSqlServerDbContext<OrchestratorContext>(settings =>
    // Disable Aspire default retries as we're using a custom execution strategy
    settings.DisableRetry = true);

var app = builder.Build();

app.Run();