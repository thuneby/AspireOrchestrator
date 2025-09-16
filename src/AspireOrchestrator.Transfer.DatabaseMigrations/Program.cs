using AspireOrchestrator.Transfer.DataAccess;
using AspireOrchestrator.Transfer.DatabaseMigrations;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddDbContextPool<TransferContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("transferdb"), sqlOptions =>
    {
        sqlOptions.MigrationsAssembly("AspireOrchestrator.Transfer.DatabaseMigrations");
        // Workaround for https://github.com/dotnet/aspire/issues/1023
        //sqlOptions.ExecutionStrategy(c => new RetryingSqlServerRetryingExecutionStrategy(c));
    }));
builder.EnrichSqlServerDbContext<TransferContext>(settings =>
    // Disable Aspire default retries as we're using a custom execution strategy
    settings.DisableRetry = true);

builder.Services.AddHostedService<TransferDbInitializer>();

var host = builder.Build();
host.Run();
