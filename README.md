# CryptoMiningCalculator
This calculator serves purpose of showing mining data in your currency.
Currently only mining on eth.2miners.com is supported.
Currently only PLN currency is working. Contribution is welcome.
## Road map
 - Port console app to desktop (windows) and mobile (android)
 - add support for to other FIAT currencies (USD, EUR...)
 - add support for other mining currencies
## First steps
 - Build application using .net 5 (soon .exe and .dll relase will be published)
 - Run in terminal: `.\2MinersStats.exe <WALLET_ID> <CURRENCY_ISO> *<ETHERSCAN_API_KEY>`
 - ETHERSCAN_API_KEY is optional and is only needed to show wallet value field

## Example output

```
Wallet Id:      [0x1234567890ABCDEF1234566789ABCDED1234345]                                                            
Currency:       [PLN]                                                                                                   
Conversion rate:[11986,93] PLN/ETH                                                                                      
Unpaid value:   [214,48539893496] PLN                                                                                   
Last 24h:       [20,59932344026] PLN                                                                                    
Last hour:      [1,18428471014] PLN                                                                                     
Paid out:       [599,8778806069] PLN                                                                                    
Next Payout:    [17.05.2021 02:38:33] (based on last hour)                                                              
Next Payout:    [22.05.2021 06:03:56] (based on last 24 hours)                                                          
Payout Value:   [599,3465] PLN                                                                                          
Wallet value:   [599,8778806069] PLN                                                                                    
Unpaid value:   [0,017893272] ETH                                                                                       
Last 24h:       [0,001718482] ETH                                                                                       
Last hour:      [0,000098798] ETH                                                                                       
Paid out:       [0,05004433] ETH                                                                                        
Wallet value:   [0,05004433] ETH
```

## Used APIs
 - CoinGecko API
 - EtherScan API
 - 2Miners API
