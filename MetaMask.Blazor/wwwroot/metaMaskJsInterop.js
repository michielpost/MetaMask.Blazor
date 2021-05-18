// This is a JavaScript module that is loaded on demand. It can export any number of
// functions, and may import other JavaScript modules if required.

export async function loadMetaMask() {
    // Modern dapp browsers...
    if (window.ethereum) {
        try {
            // Request account access if needed
            await ethereum.enable();
        } catch (error) {
            // User denied account access...
        }
    }
    // Non-dapp browsers...
    else {
        console.log('Non-Ethereum browser detected. You should consider trying MetaMask!');
    }
}

export function getAddress() {
    //const accounts = await ethereum.request({ method: 'eth_accounts' });
    //console.log(accounts[0]);

    return ethereum.selectedAddress;
}

export async function getTransactionCount() {
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
    const msgParams = [
        {
            type: 'string', // Valid solidity type
            name: label,
            value: value
        }
    ]

    var result = await ethereum.request({
        method: 'eth_signTypedData',
        params:
            [
                msgParams,
                ethereum.selectedAddress
            ]
    });

    return result;
}

export async function genericRpc(method, params) {
    var result = await ethereum.request({
        method: method,
        params: params
    });

    return result;
}