using AspireOrchestrator.Domain.DataAccess;
using AspireOrchestrator.Validation.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;


var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.

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


// ToDo: Create database and migration project for ValidationContext
builder.Services.AddDbContext<ValidationContext>(options =>
    options.UseInMemoryDatabase(Guid.NewGuid().ToString())
        .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning)));

builder.Services.AddScoped<ReceiptDetailRepository>();
builder.Services.AddScoped<ValidationErrorRepository>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

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
