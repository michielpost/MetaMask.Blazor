using MetaMask.Blazor;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddMetaMaskBlazor();

await builder.Build().RunAsync();
