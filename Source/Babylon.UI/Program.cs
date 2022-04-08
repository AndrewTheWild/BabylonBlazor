using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Babylon.Blazor;
using Babylon.UI.Shared.Helpers.App;
using Microsoft.JSInterop;

namespace Babylon.UI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddTransient(sp => new InstanceCreator(sp.GetService<IJSRuntime>()));

            builder.Services.AddSingleton<AppState>();

            await builder.Build().RunAsync();
        }
    }
}
