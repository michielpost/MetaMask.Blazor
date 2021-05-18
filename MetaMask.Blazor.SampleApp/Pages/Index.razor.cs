using MetaMask.Blazor.Exceptions;
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
        public MetaMaskService MetaMaskService { get; set; } = default!;

        public bool HasMetaMask { get; set; }
        public string? SelectedAddress { get; set; }
        public string? TransactionCount { get; set; }
        public string? SignedData { get; set; }
        public string? RpcResult { get; set; }

        protected override async Task OnInitializedAsync()
        {
            HasMetaMask = await MetaMaskService.HasMetaMask();
            bool isSiteConnected = await MetaMaskService.IsSiteConnected();
            if (isSiteConnected)
                await GetSelectedAddress();
        }

        public async Task ConnectMetaMask()
        {
            await MetaMaskService.ConnectMetaMask();
            await GetSelectedAddress();
        }

        public async Task GetSelectedAddress()
        {
            SelectedAddress = await MetaMaskService.GetSelectedAddress();
            Console.WriteLine($"Address: {SelectedAddress}");
        }

        public async Task GetTransactionCount()
        {
            var transactionCount = await MetaMaskService.GetTransactionCount();
            TransactionCount = $"Transaction count: {transactionCount}";
        }

        public async Task SignData(string label, string value)
        {
            try
            {
                var result = await MetaMaskService.SignTypedData("test label", "test value");
                SignedData = $"Signed: {result}";
            }
            catch(UserDeniedException)
            {
                SignedData = "User Denied";
            }
            catch(Exception ex)
            {
                SignedData = $"Exception: {ex}";
            }
        }

        public async Task GenericRpc()
        {
            var result = await MetaMaskService.GenericRpc("eth_requestAccounts");
            RpcResult = $"RPC result: {result}";
        }
    }
}
