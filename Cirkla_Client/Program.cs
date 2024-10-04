using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Cirkla.ApiClient.ClientService;
using Cirkla_Client.Constants;

namespace Cirkla_Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri(Constants.Constants.baseAdress)
            });

            builder.Services.AddScoped<IClient, Client>(sp =>
            {
                var httpClient = sp.GetRequiredService<HttpClient>();
                return new Client(Constants.Constants.baseAdress, httpClient);
            });

            // TODO: Register httpClient from Nswag
            //builder.Services.AddScoped<IClient, Client>(sp => new Client
            //{
            //    BaseUrl = Constants.Constants.baseAdress, HttpClient httpClient
            //});

            await builder.Build().RunAsync();
        }
    }
}
