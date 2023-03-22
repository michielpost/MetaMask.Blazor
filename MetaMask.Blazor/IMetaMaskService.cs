using MetaMask.Blazor.Enums;
using MetaMask.Blazor.Extensions;
using Microsoft.JSInterop;
using System;
using System.Numerics;
using System.Threading.Tasks;

namespace MetaMask.Blazor
{
    public interface IMetaMaskService
    {
        public static event Func<string, Task>? AccountChangedEvent;
        public static event Func<(long, Chain), Task>? ChainChangedEvent;

        /// <summary>
        /// https://docs.metamask.io/guide/ethereum-provider.html#connect
        /// </summary>
        public static event Action? OnConnectEvent;

        /// <summary>
        /// https://docs.metamask.io/guide/ethereum-provider.html#disconnect
        /// </summary>
        public static event Action? OnDisconnectEvent;

        ValueTask ConnectMetaMask();
        ValueTask DisposeAsync();
        ValueTask<dynamic> GenericRpc(string method, params dynamic[]? args);
        Task<BigInteger> GetBalance(string address, string block = "latest");
        ValueTask<string> GetSelectedAddress();
        ValueTask<(long chainId, Chain chain)> GetSelectedChain();
        ValueTask<long> GetTransactionCount();
        ValueTask<bool> HasMetaMask();
        ValueTask<bool> IsSiteConnected();
        ValueTask ListenToEvents();
        ValueTask<IJSObjectReference> LoadScripts(IJSRuntime jsRuntime);
        Task<string> RequestAccounts();
        ValueTask<string> SendTransaction(string to, BigInteger weiValue, string? data = null);
        ValueTask<string> PersonalSign(string message);
        ValueTask<string> SignTypedData(string label, string value);
        ValueTask<string> SignTypedDataV4(string typedData);

        [JSInvokable()]
        static void OnConnect()
        {
            if (OnConnectEvent != null)
            {
                OnConnectEvent.Invoke();
            }
        }

        [JSInvokable()]
        static void OnDisconnect()
        {
            if (OnDisconnectEvent != null)
            {
                OnDisconnectEvent.Invoke();
            }
        }

        [JSInvokable()]
        static async Task OnAccountsChanged(string selectedAccount)
        {
            if (AccountChangedEvent != null)
            {
                await AccountChangedEvent.Invoke(selectedAccount);
            }
        }

        [JSInvokable()]
        static async Task OnChainChanged(string chainhex)
        {
            if (ChainChangedEvent != null)
            {
                await ChainChangedEvent.Invoke(ChainHexToChainResponse(chainhex));
            }
        }

        protected static (long chainId, Chain chain) ChainHexToChainResponse(string chainHex)
        {
            long chainId = chainHex.HexToInt();
            return (chainId, (Chain)chainId);
        }
    }
}