using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

namespace MetaMask.Blazor
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMetaMaskBlazor(this IServiceCollection services)
        {
            services.AddScoped<IMetaMaskService>(sp => new MetaMaskService(sp.GetRequiredService<IJSRuntime>()));
        }
    }
}
