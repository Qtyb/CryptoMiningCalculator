# CryptoMiningCalculator
This calculator serves purpose of showing mining data in your currency.
Currently only mining on eth.2miners.com is supported.

## First steps
 - Build application using .net 5 (soon .exe and .dll relase will be published)
 - Run in terminal: `.\2MinersStats.exe <WALLET_ID> <CURRENCY_ISO> *<ETHERSCAN_API_KEY>`
 - ETHERSCAN_API_KEY is optional and is only needed to show wallet value field

## Showed values
 - Unpaid value
 - Last 24h gain
 - Last hour gain
 - Total paid out
 - Next payout dat
 - Next payout value
 - Wallet value

## Used APIs
 - CoinGecko API
 - EtherScan API
 - 2Miners API
