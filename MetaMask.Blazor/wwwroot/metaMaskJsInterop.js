// This is a JavaScript module that is loaded on demand. It can export any number of
// functions, and may import other JavaScript modules if required.

export async function checkMetaMask() {
    // Modern dapp browsers...
    if (window.ethereum) {
        if (ethereum.selectedAddress === null) {
            try {
                // Request account access if needed
                await requestAccounts();
            } catch (error) {
                // User denied account access...
                throw "UserDenied"
            }
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
    return (window.ethereum != undefined && ethereum.selectedAddress != undefined);
}

export async function getSelectedAddress() {
    await checkMetaMask();

    return ethereum.selectedAddress;
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
        console.log(error);
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