using MedicalClientBlazor.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;

namespace MedicalClientBlazor
{
    public class Program
    {
        private static readonly string baseUri = "https://localhost:44323/api/";
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped(service => new PatientService(baseUri));
            builder.Services.AddScoped(cService => new ConsultationService(baseUri));

            // add blazorise and font aweseme
            builder.Services.AddBlazorise(opt =>
            {
                opt.ChangeTextOnKeyPress = true;
            }).AddBootstrapProviders()
              .AddFontAwesomeIcons();

            var host = builder.Build();

            host.Services
                .UseBootstrapProviders()
                .UseFontAwesomeIcons();

            await host.RunAsync();
        }
    }
}
