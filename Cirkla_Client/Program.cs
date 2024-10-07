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
                BaseAddress = new Uri(Constants.GetConstant.baseAdress)
            });

            builder.Services.AddScoped<IClient, Client>(sp =>
            {
                var httpClient = sp.GetRequiredService<HttpClient>();
                return new Client(GetConstant.baseAdress, httpClient);

            });

            await builder.Build().RunAsync();
        }
    }
}
