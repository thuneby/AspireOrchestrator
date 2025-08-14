using AspireOrchestrator.Domain.DataAccess;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
builder.AddAzureBlobServiceClient("blobs");

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

builder.Services.AddScoped<ReceiptDetailRepository>();

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
