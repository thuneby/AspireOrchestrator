using AspireOrchestrator.Administration.Services;
using AspireOrchestrator.Domain.DataAccess;
using AspireOrchestrator.Storage.Helpers;
using AspireOrchestrator.Storage.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddAzureBlobServiceClient("blobs");
builder.Services.AddScoped<IStorageHelper, BlobStorageHelper>();

// Add service defaults & Aspire components.
builder.AddServiceDefaults();
builder.AddAzureServiceBusClient(connectionName: "servicebus");

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
builder.Services.AddScoped<DepositRepository>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<EventPublisherService>();
builder.Services.AddHttpClient<OrchestratorApiService>(
    static client => client.BaseAddress = new("https+http://orchestratorapi"));

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    //In development, create the blob container and queue if they don't exist.
    //var blobService = app.Services.GetRequiredService<BlobServiceClient>();
    //var docsContainer = blobService.GetBlobContainerClient("fileuploads");

    //await docsContainer.CreateIfNotExistsAsync();
}
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Admin}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
