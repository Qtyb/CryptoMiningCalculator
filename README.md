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

## Example output

```
WalletId: [0x1234567890ABCDEF1234566789ABCDED1234345]                                                                  
Currency: [USD]                                                                                                       
UnPaidValue: [15,5798809541]                                                                                            
Last24hGain: [10,1668809541]                                                                                            
PerHourGain: [0,8588350539]                                                                                             
TotalPaidOut: [43,8588350539]                                                                                                     
NextPayoutDateTime: [17.04.2021 20:35:29]                                                                               
NextPayoutValue: [130,865]                                                                                              
WalletValue: [500,324]  
```

## Used APIs
 - CoinGecko API
 - EtherScan API
 - 2Miners API
