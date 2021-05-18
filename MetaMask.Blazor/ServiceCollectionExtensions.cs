using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaMask.Blazor
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMetaMaskBlazor(this IServiceCollection services)
        {
            services.AddScoped<MetaMaskService>();
        }
    }
}
