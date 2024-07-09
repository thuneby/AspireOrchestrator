using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


var builder = DistributedApplication.CreateBuilder(args);

builder.Build().Run();
