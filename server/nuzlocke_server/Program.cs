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
        var riotApiKey = app.Configuration["RGAPI-6326c744-4ee9-4cc3-91bf-9676cbc3101d"];
        
        try
        {
            var response = await httpClient.GetAsync($"https://europe.api.riotgames.com/riot/account/v1/accounts/by-riot-id/lulufizz/euw?api_key=RGAPI-cb139c63-5909-4352-834c-f6e6cc84db03");
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching summoner info during startup: {ex.Message}");
        }

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
