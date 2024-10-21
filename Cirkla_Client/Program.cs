using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Cirkla.ApiClient;
using Cirkla_Client.Constants;
using Cirkla_Client.Services;
using Blazored.LocalStorage;
using Cirkla_API.Providers;
using System.IdentityModel.Tokens.Jwt;

namespace Cirkla_Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            // TODO: Move services to separate class
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<IApiAuthStateProvider, ApiAuthStateProvider>();
            builder.Services.AddSingleton<JwtSecurityTokenHandler>();
            builder.Services.AddBlazoredLocalStorage();

            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri(ApiAddress.baseAdress)
            });

            builder.Services.AddScoped<IClient, Client>(sp =>
            {
                var httpClient = sp.GetRequiredService<HttpClient>();
                return new Client(httpClient);

            });

            await builder.Build().RunAsync();
        }
    }
}
