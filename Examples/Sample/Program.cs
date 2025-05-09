
using Bitget.Net.Objects;
using BitMart.Net.Objects;
using CryptoClients.Net;
using CryptoClients.Net.Enums;
using CryptoClients.Net.Models;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis;
using DeepCoin.Net.Objects;
using Kucoin.Net.Objects;
using OKX.Net.Objects;

var symbol = new SharedSymbol(TradingMode.PerpetualLinear, "XRP", "USDT");
var restClient = new ExchangeRestClient(globalOptions =>
{
    globalOptions.Proxy = new CryptoExchange.Net.Objects.ApiProxy("socks5://192.168.2.100", 1080, "", "");
    globalOptions.RequestTimeout = TimeSpan.FromSeconds(10);
    globalOptions.ApiCredentials = new ExchangeCredentials();
    //globalOptions.ApiCredentials.Binance = new ApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!);
    //globalOptions.ApiCredentials.BingX = new ApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!);
    //globalOptions.ApiCredentials.Bitfinex = new ApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!);
    //globalOptions.ApiCredentials.Bitget = new BitgetApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!, apiCredential.Passphrase!);
    //globalOptions.ApiCredentials.BitMart = new BitMartApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!, apiCredential.Passphrase!);
    //globalOptions.ApiCredentials.BitMEX = new ApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!);
    //globalOptions.ApiCredentials.Bybit = new ApiCredentials("xzrw8f84fTyWI3y5U3", "JHO142DkjPnBTbPr3KLBqfvS7HF5H76arRmH");
    globalOptions.ApiCredentials.Bybit = new ApiCredentials("PglQBNdPTZK6MycUpT", "enMKQVH03ivit5bdB1kcsUUf4fPGNUZDxxun");
    //globalOptions.ApiCredentials.Coinbase = new ApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!);
    //globalOptions.ApiCredentials.CoinEx = new ApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!);
    //globalOptions.ApiCredentials.CryptoCom = new ApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!);
    //globalOptions.ApiCredentials.DeepCoin = new DeepCoinApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!, apiCredential.Passphrase!);
    //globalOptions.ApiCredentials.GateIo = new ApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!);
    //globalOptions.ApiCredentials.HTX = new ApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!);
    //globalOptions.ApiCredentials.HyperLiquid = new ApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!);
    //globalOptions.ApiCredentials.Kraken = new ApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!);
    //globalOptions.ApiCredentials.Kucoin = new KucoinApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!, apiCredential.Passphrase!);
    //globalOptions.ApiCredentials.Mexc = new ApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!);
    //globalOptions.ApiCredentials.OKX = new OKXApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!, apiCredential.Passphrase!);
    //globalOptions.ApiCredentials.WhiteBit = new ApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!);
    //globalOptions.ApiCredentials.XT = new ApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!);
}, binanceRestOptions: (options) =>
{
    //if (exGlobalOptions!.Testnet ?? false)
    {
        options.Environment = Binance.Net.BinanceEnvironment.Testnet;
    }
}, okxRestOptions: (options) =>
{
    //if (exGlobalOptions!.Testnet ?? false)
    {
        options.Environment = OKX.Net.OKXEnvironment.Demo;
    }
}, coinExRestOptions: (options) =>
{

}, gateIoRestOptions: (options) =>
{

}, bybitRestOptions: (options) =>
{
    //if (exGlobalOptions!.Testnet ?? false)
    {
        //options.Environment = Bybit.Net.BybitEnvironment.Testnet;
    }
});

//var socketClient = new ExchangeSocketClient();
var socketClient = new ExchangeSocketClient(globalOptions =>
{
    globalOptions.Proxy = new CryptoExchange.Net.Objects.ApiProxy("socks5://192.168.2.100", 1080, "", "");
    globalOptions.RequestTimeout = TimeSpan.FromSeconds(10);
    globalOptions.ApiCredentials = new ExchangeCredentials();
    //globalOptions.ApiCredentials.Binance = new ApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!);
    //globalOptions.ApiCredentials.BingX = new ApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!);
    //globalOptions.ApiCredentials.Bitfinex = new ApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!);
    //globalOptions.ApiCredentials.Bitget = new BitgetApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!, apiCredential.Passphrase!);
    //globalOptions.ApiCredentials.BitMart = new BitMartApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!, apiCredential.Passphrase!);
    //globalOptions.ApiCredentials.BitMEX = new ApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!);
    //globalOptions.ApiCredentials.Bybit = new ApiCredentials("xzrw8f84fTyWI3y5U3", "JHO142DkjPnBTbPr3KLBqfvS7HF5H76arRmH");
    globalOptions.ApiCredentials.Bybit = new ApiCredentials("PglQBNdPTZK6MycUpT", "enMKQVH03ivit5bdB1kcsUUf4fPGNUZDxxun");
    //globalOptions.ApiCredentials.Coinbase = new ApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!);
    //globalOptions.ApiCredentials.CoinEx = new ApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!);
    //globalOptions.ApiCredentials.CryptoCom = new ApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!);
    //globalOptions.ApiCredentials.DeepCoin = new DeepCoinApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!, apiCredential.Passphrase!);
    //globalOptions.ApiCredentials.GateIo = new ApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!);
    //globalOptions.ApiCredentials.HTX = new ApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!);
    //globalOptions.ApiCredentials.HyperLiquid = new ApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!);
    //globalOptions.ApiCredentials.Kraken = new ApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!);
    //globalOptions.ApiCredentials.Kucoin = new KucoinApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!, apiCredential.Passphrase!);
    //globalOptions.ApiCredentials.Mexc = new ApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!);
    //globalOptions.ApiCredentials.OKX = new OKXApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!, apiCredential.Passphrase!);
    //globalOptions.ApiCredentials.WhiteBit = new ApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!);
    //globalOptions.ApiCredentials.XT = new ApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!);
}, binanceSocketOptions: (options) =>
{
    //if (exGlobalOptions!.Testnet ?? false)
    {
        options.Environment = Binance.Net.BinanceEnvironment.Testnet;
    }
}, okxSocketOptions: (options) =>
{
    //if (exGlobalOptions!.Testnet ?? false)
    {
        options.Environment = OKX.Net.OKXEnvironment.Demo;
    }
}, coinExSocketOptions: (options) =>
{

}, gateIoSocketOptions: (options) =>
{

}, bybitSocketOptions: (options) =>
{
    //if (exGlobalOptions!.Testnet ?? false)
    {
        //options.Environment = Bybit.Net.BybitEnvironment.Testnet;
    }
});

var exchangeParameters = new ExchangeParameters();
exchangeParameters.AddValue(new ExchangeParameter(Exchange.Bybit, "UnifiedAccount", "true"));
var balancesClient = restClient.GetBalancesClient(TradingMode.PerpetualLinear, Exchange.Bybit);
var balances = balancesClient.GetBalancesAsync(new GetBalancesRequest(TradingMode.PerpetualLinear, exchangeParameters: exchangeParameters)).GetAwaiter().GetResult();

var feeClient = restClient.GetFeeClient(TradingMode.DeliveryLinear, Exchange.GateIo);
var fees = feeClient!.GetFeesAsync(new GetFeeRequest(symbol, exchangeParameters: exchangeParameters)).GetAwaiter().GetResult();

// Subscribe to trade updates for the specified exchange
//foreach (var subResult in await socketClient.SubscribeToTradeUpdatesAsync(new SubscribeTradeRequest(symbol), LogTrades, [Exchange.Binance, Exchange.HTX, Exchange.OKX]))
//    Console.WriteLine($"{subResult.Exchange} subscribe result: {subResult.Success} {subResult.Error}");
// 订阅余额更新
var client = socketClient.GetBalanceClient(TradingMode.PerpetualLinear, Exchange.Bybit);
await client!.SubscribeToBalanceUpdatesAsync(new SubscribeBalancesRequest(listenKey: null, tradingMode: TradingMode.PerpetualLinear, exchangeParameters: exchangeParameters), (update) =>
{
    foreach (var item in update.Data)
    {
        Console.WriteLine($"Asset: {item.Asset} Available: {item.Available} Total: {item.Total}");
    }
});

Console.ReadLine();

void LogTrades(ExchangeEvent<IEnumerable<SharedTrade>> update)
{
    foreach (var item in update.Data)
        Console.WriteLine($"{update.Exchange.PadRight(10)} | {item.Quantity} @ {item.Price}");    
}