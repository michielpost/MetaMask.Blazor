# MetaMask.Blazor
Use MetaMask with Blazor WebAssembly

This library provides an easy helper to use MetaMask with Blazor WebAssembly.

**[Live Demo](https://600bkveode5ts2d3lsagn6pf4q6psed9c343e16016ma9dp2b9g0lbo.siasky.net/)**

Real world implementation, login with MetaMask: [SkyDocs](https://skydocs.hns.siasky.net)

## How to use
See included Blazor sample app.

Install: [MetaMask.Blazor on NuGet](https://www.nuget.org/packages/MetaMask.Blazor/)

Register in `Program.cs`:
```cs
builder.Services.AddMetaMaskBlazor();
```

Inject the `MetaMaskService` in your Razor page
```cs
@using MetaMask.Blazor
@inject MetaMaskService MetaMaskService
```

or class when using a .razor.cs file:
```cs
[Inject]
public MetaMaskService MetaMaskService { get; set; } = default!;
```

Use the `MetaMaskService`:

Check if the user has MetaMask installed:
```cs
HasMetaMask = await MetaMaskService.HasMetaMask();
```

Check if the user has previously connected to your site.
```cs
bool isSiteConnected = await MetaMaskService.IsSiteConnected();
```

Initialize a new connection with MetaMask
```cs
await MetaMaskService.ConnectMetaMask();
```
This can throw exceptions if the user decides to not allow the connection.

Once there is a connection, you can use other method calls like:
- `GetSelectedAddress`
- `GetTransactionCount`
- `SignTypedData`
- or use the generic RPC method: `GenericRpc`

## Reference
- https://docs.metamask.io
- https://metamask.io

## Acknowledgements
Development of MetaMask.Blazor has been made possible with a grant from [The Graph](https://thegraph.com/blog/wave-one-funding).