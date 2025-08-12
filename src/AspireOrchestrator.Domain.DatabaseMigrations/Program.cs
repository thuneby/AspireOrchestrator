using AspireOrchestrator.Domain.DataAccess;
using AspireOrchestrator.Domain.DatabaseMigrations;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<DomainDbInitializer>();

builder.AddServiceDefaults();

builder.Services.AddDbContextPool<DomainContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("domaindb"), sqlOptions =>
    {
        sqlOptions.MigrationsAssembly("AspireOrchestrator.Domain.DatabaseMigrations");
        // Workaround for https://github.com/dotnet/aspire/issues/1023
        //sqlOptions.ExecutionStrategy(c => new RetryingSqlServerRetryingExecutionStrategy(c));
    }));
builder.EnrichSqlServerDbContext<DomainContext>(settings =>
    // Disable Aspire default retries as we're using a custom execution strategy
    settings.DisableRetry = true);

var host = builder.Build();
host.Run();
