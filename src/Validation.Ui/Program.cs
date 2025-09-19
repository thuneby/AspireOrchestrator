using AspireOrchestrator.Domain.DataAccess;
using AspireOrchestrator.Validation.DataAccess;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DomainContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("Validation.Ui")));

builder.Services.AddDbContext<ValidationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ValidationConnection"),
        b => b.MigrationsAssembly("Validation.Ui")));


builder.Services.AddScoped<ReceiptDetailRepository>();
builder.Services.AddScoped<ValidationErrorRepository>();


builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
