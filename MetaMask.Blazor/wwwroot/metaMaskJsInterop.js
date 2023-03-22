// This is a JavaScript module that is loaded on demand. It can export any number of
// functions, and may import other JavaScript modules if required.

export async function checkMetaMask() {
    // Modern dapp browsers...
    if (window.ethereum) {
        if (ethereum.selectedAddress === null || ethereum.selectedAddress === undefined) {
            try {
                // Request account access if needed
                await requestAccounts();
            } catch (error) {
                // User denied account access...
                throw "UserDenied"
            }
        }
        else {
            console.log("Selected:" + ethereum.selectedAddress);
        }
    }
    // Non-dapp browsers...
    else {
        throw "NoMetaMask"
    }
}

export async function requestAccounts() {
    var result = await ethereum.request({
        method: 'eth_requestAccounts',
    });
    return result;
}

export function hasMetaMask() {
    return (window.ethereum != undefined);
}

export function isSiteConnected() {
    return (window.ethereum != undefined && (ethereum.selectedAddress != undefined || ethereum.selectedAddress != null));
}

export async function getSelectedAddress() {
    await checkMetaMask();

    return ethereum.selectedAddress;
}

export async function listenToChangeEvents() {
    if (hasMetaMask()) {
        ethereum.on("connect", function (connectInfo) {
            DotNet.invokeMethodAsync('MetaMask.Blazor', 'OnConnect');
        });

        ethereum.on("disconnect", function (error) {
            DotNet.invokeMethodAsync('MetaMask.Blazor', 'OnDisconnect');
        });

        ethereum.on("accountsChanged", function (accounts) {
            DotNet.invokeMethodAsync('MetaMask.Blazor', 'OnAccountsChanged', accounts[0]);
        });

        ethereum.on("chainChanged", function (chainId) {
            DotNet.invokeMethodAsync('MetaMask.Blazor', 'OnChainChanged', chainId);
        });
    }
}

export async function getSelectedChain() {
    await checkMetaMask();

    var result = await ethereum.request({
        method: 'eth_chainId'
    });
    return result;
}

export async function getTransactionCount() {
    await checkMetaMask();

    var result = await ethereum.request({
        method: 'eth_getTransactionCount',
        params:
            [
                ethereum.selectedAddress,
                'latest'
            ]

    });
    return result;
}

export async function personalSign(message) {
    await checkMetaMask();

    // Convert message to hex
    const hex = message.split("")
        .map(c => c.charCodeAt(0).toString(16).padStart(2, "0"))
        .join("");
    const msg = `0x${hex}`;

    try {
        var result = await ethereum.request({
            method: 'personal_sign',
            params:
                [
                    msg,
                    ethereum.selectedAddress
                ]
        });

        return result;
    } catch (error) {
        // User denied account access...
        throw "UserDenied"
    }
}

export async function signTypedData(label, value) {
    await checkMetaMask();

    const msgParams = [
        {
            type: 'string', // Valid solidity type
            name: label,
            value: value
        }
    ]

    try {
        var result = await ethereum.request({
            method: 'eth_signTypedData',
            params:
                [
                    msgParams,
                    ethereum.selectedAddress
                ]
        });

        return result;
    } catch (error) {
        // User denied account access...
        throw "UserDenied"
    }
}

export async function signTypedDataV4(typedData) {
    await checkMetaMask();
    
    try {
        var result = await ethereum.request({
            method: 'eth_signTypedData_v4',
            params:
                [
                    ethereum.selectedAddress,
                    typedData
                ],
            from: ethereum.selectedAddress
        });

        return result;
    } catch (error) {
        // User denied account access...
        throw "UserDenied"
    }
}

export async function sendTransaction(to, value, data) {
    await checkMetaMask();

    const transactionParameters = {
        to: to,
        from: ethereum.selectedAddress, // must match user's active address.
        value: value,
        data: data
    };

    try {
        var result = await ethereum.request({
            method: 'eth_sendTransaction',
            params: [transactionParameters]
        });

        return result;
    } catch (error) {
        if (error.code == 4001) {
            throw "UserDenied"
        }
        throw error;
    }
}

export async function genericRpc(method, params) {
    await checkMetaMask();

    var result = await ethereum.request({
        method: method,
        params: params
    });

    return result;
}