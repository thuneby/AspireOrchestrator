using AspireOrchestrator.Domain.DataAccess;
using AspireOrchestrator.Validation.DataAccess;
using AspireOrchestrator.Validation.DatabaseMigrations;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddHostedService<ValidationDbInitializer>();

builder.Services.AddDbContextPool<ValidationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("validationdb"), sqlOptions =>
    {
        sqlOptions.MigrationsAssembly("AspireOrchestrator.Validation.DatabaseMigrations");
        // Workaround for https://github.com/dotnet/aspire/issues/1023
        //sqlOptions.ExecutionStrategy(c => new RetryingSqlServerRetryingExecutionStrategy(c));
    }));
builder.EnrichSqlServerDbContext<DomainContext>(settings =>
    // Disable Aspire default retries as we're using a custom execution strategy
    settings.DisableRetry = true);

var host = builder.Build();
host.Run();
