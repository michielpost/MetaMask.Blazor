using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetaMask.Blazor.SampleApp.Pages
{
    public partial class Index
    {
        [Inject]
        public MetaMaskJsInterop MetaMaskJsInterop { get; set; } = default!;

        public string? SelectedAddress { get; set; }
        public string? TransactionCount { get; set; }
        public string? SignedData { get; set; }
        public string? RpcResult { get; set; }

        protected override async Task OnInitializedAsync()
        {
            bool isSiteConnected = await MetaMaskJsInterop.IsSiteConnected();
            if (isSiteConnected)
                await GetSelectedAddress();
        }

        public async Task CheckMetaMask()
        {
            await MetaMaskJsInterop.CheckMetaMask();
            await GetSelectedAddress();
        }

        public async Task GetSelectedAddress()
        {
            SelectedAddress = await MetaMaskJsInterop.GetSelectedAddress();
            Console.WriteLine($"Address: {SelectedAddress}");
        }

        public async Task GetTransactionCount()
        {
            var transactionCount = await MetaMaskJsInterop.GetTransactionCount();
            TransactionCount = $"Transaction count: {transactionCount}";
        }

        public async Task SignData(string label, string value)
        {
            var result = await MetaMaskJsInterop.SignTypedData("test label", "test value");
            SignedData = $"Signed: {result}";
        }

        public async Task GenericRpc()
        {
            var result = await MetaMaskJsInterop.GenericRpc("eth_requestAccounts");
            RpcResult = $"RPC result: {result}";
        }
    }
}
