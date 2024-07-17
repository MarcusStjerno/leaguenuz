using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add configuration
        builder.Configuration.AddJsonFile("appsettings.json");
        builder.Configuration.AddEnvironmentVariables();

        // Add services to the container.
        builder.Services.AddHttpClient();
        builder.Services.AddControllers();

        var app = builder.Build();

        // Make API call during startup
        var httpClientFactory = app.Services.GetRequiredService<IHttpClientFactory>();
        var httpClient = httpClientFactory.CreateClient();


        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.Run();
    }
}
