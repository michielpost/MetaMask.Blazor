using MetaMask.Blazor.Exceptions;
using Microsoft.JSInterop;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace MetaMask.Blazor
{
    // This class provides JavaScript functionality for MetaMask wrapped
    // in a .NET class for easy consumption. The associated JavaScript module is
    // loaded on demand when first needed.
    //
    // This class can be registered as scoped DI service and then injected into Blazor
    // components for use.

    public class MetaMaskService : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;

        public MetaMaskService(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => LoadScripts(jsRuntime).AsTask());
        }

        public ValueTask<IJSObjectReference> LoadScripts(IJSRuntime jsRuntime)
        {
            //await jsRuntime.InvokeAsync<IJSObjectReference>("import", "https://cdn.ethers.io/lib/ethers-5.1.0.umd.min.js");
            return jsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/MetaMask.Blazor/metaMaskJsInterop.js");
        }

        public async ValueTask ConnectMetaMask()
        {
            var module = await moduleTask.Value;
            try
            {
                await module.InvokeVoidAsync("checkMetaMask");
            }
            catch (Exception ex)
            {
                HandleExceptions(ex);
                throw;
            }
        }

        public async ValueTask<bool> HasMetaMask()
        {
            var module = await moduleTask.Value;
            try
            {
                return await module.InvokeAsync<bool>("hasMetaMask");
            }
            catch (Exception ex)
            {
                HandleExceptions(ex);
                throw;
            }
        }

        public async ValueTask<bool> IsSiteConnected()
        {
            var module = await moduleTask.Value;
            try
            {
                return await module.InvokeAsync<bool>("isSiteConnected");
            }
            catch (Exception ex)
            {
                HandleExceptions(ex);
                throw;
            }
        }

        public async ValueTask<string> GetSelectedAddress()
        {
            var module = await moduleTask.Value;
            try
            {
                return await module.InvokeAsync<string>("getSelectedAddress", null);
            }
            catch (Exception ex)
            {
                HandleExceptions(ex);
                throw;
            }
        }

        public async ValueTask<int> GetTransactionCount()
        {
            var module = await moduleTask.Value;
            try
            {
                var result = await module.InvokeAsync<JsonElement>("getTransactionCount");
                var resultString = result.GetString()?.Replace("0x", string.Empty);
                if (resultString != null)
                {
                    int intValue = int.Parse(resultString, System.Globalization.NumberStyles.HexNumber);
                    return intValue;
                }
                return 0;
            }
            catch (Exception ex)
            {
                HandleExceptions(ex);
                throw;
            }
        }

        public async ValueTask<string> SignTypedData(string label, string value)
        {
            var module = await moduleTask.Value;
            try
            {
                return await module.InvokeAsync<string>("signTypedData", label, value);
            }
            catch (Exception ex)
            {
                HandleExceptions(ex);
                throw;
            }
        }

        public async ValueTask<dynamic> GenericRpc(string method, params dynamic?[]? args)
        {
            var module = await moduleTask.Value;
            try
            {
                return await module.InvokeAsync<dynamic>("genericRpc", method, args);
            }
            catch (Exception ex)
            {
                HandleExceptions(ex);
                throw;
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await module.DisposeAsync();
            }
        }

        private void HandleExceptions(Exception ex)
        {
            switch (ex.Message)
            {
                case "NoMetaMask":
                    throw new NoMetaMaskException();
                case "UserDenied":
                    throw new UserDeniedException();
            }
        }
    }
}
