using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using CryptoClients.Net;
using CryptoClients.Net.Enums;
using CryptoClients.Net.Interfaces;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.CommonObjects;
using Bitget.Net.Objects;
using BitMart.Net.Objects;
using DeepCoin.Net.Objects;
using Kucoin.Net.Objects;
using OKX.Net.Objects;
using CryptoClients.Net.Models;
using System.Text.Json;
using System.Runtime.Versioning;
using System.Text;
using Bybit.Net.Enums;
using Kucoin.Net.Enums;
using CryptoExchange.Net.Interfaces;


namespace CryptoExchangeWrapper
{
    /// <summary>
    /// NativeSocketMethods
    /// </summary>
    public unsafe static class NativeSocketMethods
    {
        /// <summary>
        /// 创建CryptoClients.Net.Interfaces.IExchangeSocketClient实例
        /// </summary>
        /// <param name="globalOptions">CryptoClients.Net.Models.GlobalExchangeOptions实例的json字符串</param>
        /// <returns>CryptoClients.Net.Interfaces.IExchangeSocketClient实例的指针</returns>
        [UnmanagedCallersOnly(EntryPoint = "create_exchange_socket_client")]
        public unsafe static IntPtr CreateExchangeSocketClient(sbyte* globalOptions)
        {
            try
            {
                var globalOptionsString = new string(globalOptions);
                var exGlobalOptions = JsonSerializer.Deserialize<ExGlobalExchangeOptions>(globalOptionsString);
                var exGlobalOptionsString = JsonSerializer.Serialize(exGlobalOptions);
                var socketClient = new ExchangeSocketClient(globalOptions =>
                {
                    if (exGlobalOptions?.Proxy != null)
                    {
                        globalOptions.Proxy = new CryptoExchange.Net.Objects.ApiProxy(exGlobalOptions.Proxy.Host!, exGlobalOptions.Proxy.Port, exGlobalOptions.Proxy.Username, exGlobalOptions.Proxy.Password);
                    }
                    globalOptions.RequestTimeout = TimeSpan.FromSeconds(10);
                    globalOptions.ApiCredentials = new ExchangeCredentials();
                    foreach (var apiCredential in exGlobalOptions?.ApiCredentials ?? new List<ExApiCredentials>())
                    {
                        switch (apiCredential.Exchange)
                        {
                            case "Binance":
                                globalOptions.ApiCredentials.Binance = new ApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!);
                                break;
                            case "BingX":
                                globalOptions.ApiCredentials.BingX = new ApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!);
                                break;
                            case "Bitfinex":
                                globalOptions.ApiCredentials.Bitfinex = new ApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!);
                                break;
                            case "Bitget":
                                globalOptions.ApiCredentials.Bitget = new BitgetApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!, apiCredential.Passphrase!);
                                break;
                            case "BitMart":
                                globalOptions.ApiCredentials.BitMart = new BitMartApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!, apiCredential.Passphrase!);
                                break;
                            case "BitMEX":
                                globalOptions.ApiCredentials.BitMEX = new ApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!);
                                break;
                            case "Bybit":
                                globalOptions.ApiCredentials.Bybit = new ApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!);
                                break;
                            case "Coinbase":
                                globalOptions.ApiCredentials.Coinbase = new ApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!);
                                break;
                            case "CoinEx":
                                globalOptions.ApiCredentials.CoinEx = new ApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!);
                                break;
                            case "CryptoCom":
                                globalOptions.ApiCredentials.CryptoCom = new ApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!);
                                break;
                            case "DeepCoin":
                                globalOptions.ApiCredentials.DeepCoin = new DeepCoinApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!, apiCredential.Passphrase!);
                                break;
                            case "GateIo":
                                globalOptions.ApiCredentials.GateIo = new ApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!);
                                break;
                            case "HTX":
                                globalOptions.ApiCredentials.HTX = new ApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!);
                                break;
                            case "HyperLiquid":
                                globalOptions.ApiCredentials.HyperLiquid = new ApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!);
                                break;
                            case "Kraken":
                                globalOptions.ApiCredentials.Kraken = new ApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!);
                                break;
                            case "Kucoin":
                                globalOptions.ApiCredentials.Kucoin = new KucoinApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!, apiCredential.Passphrase!);
                                break;
                            case "Mexc":
                                globalOptions.ApiCredentials.Mexc = new ApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!);
                                break;
                            case "OKX":
                                globalOptions.ApiCredentials.OKX = new OKXApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!, apiCredential.Passphrase!);
                                break;
                            case "WhiteBit":
                                globalOptions.ApiCredentials.WhiteBit = new ApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!);
                                break;
                            case "XT":
                                globalOptions.ApiCredentials.XT = new ApiCredentials(apiCredential.ApiKey!, apiCredential.ApiSecret!);
                                break;
                            default:
                                break;
                        }
                    }
                }, binanceSocketOptions: (options) =>
                {
                    if (exGlobalOptions!.Testnet ?? false)
                    {
                        options.Environment = Binance.Net.BinanceEnvironment.Testnet;
                    }
                }, okxSocketOptions: (options) =>
                {
                    if (exGlobalOptions!.Testnet ?? false)
                    {
                        options.Environment = OKX.Net.OKXEnvironment.Demo;
                    }
                }, coinExSocketOptions: (options) =>
                {

                }, gateIoSocketOptions: (options) =>
                {

                }, bybitSocketOptions: (options) =>
                {
                    if (exGlobalOptions!.Testnet ?? false)
                    {
                        options.Environment = Bybit.Net.BybitEnvironment.Testnet;
                    }
                });
                return GCHandle.ToIntPtr(GCHandle.Alloc(socketClient));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return IntPtr.Zero;
            }
        }

        /// <summary>
        /// 获取行情客户端
        /// </summary>
        /// <param name="inst">实例</param>
        /// <param name="tradingMode">交易模式</param>
        /// <param name="exchange">交易所</param>
        /// <returns>行情客户端</returns>
        [UnmanagedCallersOnly(EntryPoint = "get_tickers_socket_client")]
        public unsafe static IntPtr GetTickersSocketClient(IntPtr inst, int tradingMode, sbyte* exchange)
        {
            try
            {
                var socketClient = GCHandle.FromIntPtr(inst).Target as IExchangeSocketClient;
                var tickersClient = socketClient?.GetTickersClient((TradingMode)tradingMode, new string(exchange));
                return GCHandle.ToIntPtr(GCHandle.Alloc(tickersClient));
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return IntPtr.Zero; }
        }

        /// <summary>
        /// 获取行情客户端
        /// </summary>
        /// <param name="inst">实例</param>
        /// <param name="tradingMode">交易模式</param>
        /// <param name="exchange">交易所</param>
        /// <returns>行情客户端</returns>
        [UnmanagedCallersOnly(EntryPoint = "get_ticker_socket_client")]
        public unsafe static IntPtr GetTickerSocketClient(IntPtr inst, int tradingMode, sbyte* exchange)
        {
            try
            {
                var socketClient = GCHandle.FromIntPtr(inst).Target as IExchangeSocketClient;
                var tickerClient = socketClient?.GetTickerClient((TradingMode)tradingMode, new string(exchange));
                return GCHandle.ToIntPtr(GCHandle.Alloc(tickerClient));
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return IntPtr.Zero; }
        }

        /// <summary>
        /// 获取订单簿客户端
        /// </summary>
        /// <param name="inst">实例</param>
        /// <param name="tradingMode">交易模式</param>
        /// <param name="exchange">交易所</param>
        /// <returns>订单簿客户端</returns>
        [UnmanagedCallersOnly(EntryPoint = "get_order_book_socket_client")]
        public unsafe static IntPtr GetOrderBookSocketClient(IntPtr inst, int tradingMode, sbyte* exchange)
        {
            try
            {
                var socketClient = GCHandle.FromIntPtr(inst).Target as IExchangeSocketClient;
                var orderBookClient = socketClient?.GetOrderBookClient((TradingMode)tradingMode, new string(exchange));
                return GCHandle.ToIntPtr(GCHandle.Alloc(orderBookClient));
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return IntPtr.Zero; }
        }

        /// <summary>
        /// 获取订单簿客户端
        /// </summary>
        /// <param name="inst">实例</param>
        /// <param name="tradingMode">交易模式</param>
        /// <param name="exchange">交易所</param>
        /// <returns>订单簿客户端</returns>
        [UnmanagedCallersOnly(EntryPoint = "get_book_ticker_socket_client")]
        public unsafe static IntPtr GetBookTickerSocketClient(IntPtr inst, int tradingMode, sbyte* exchange)
        {
            try
            {
                var socketClient = GCHandle.FromIntPtr(inst).Target as IExchangeSocketClient;
                var bookTickerClient = socketClient?.GetBookTickerClient((TradingMode)tradingMode, new string(exchange));
                return GCHandle.ToIntPtr(GCHandle.Alloc(bookTickerClient));
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return IntPtr.Zero; }
        }

        /// <summary>
        /// 获取交易客户端
        /// </summary>
        /// <param name="inst">实例</param>
        /// <param name="tradingMode">交易模式</param>
        /// <param name="exchange">交易所</param>
        /// <returns>交易客户端</returns>
        [UnmanagedCallersOnly(EntryPoint = "get_trade_socket_client")]
        public unsafe static IntPtr GetTradeSocketClient(IntPtr inst, int tradingMode, sbyte* exchange)
        {
            try
            {
                var socketClient = GCHandle.FromIntPtr(inst).Target as IExchangeSocketClient;
                var tradeClient = socketClient?.GetTradeClient((TradingMode)tradingMode, new string(exchange));
                return GCHandle.ToIntPtr(GCHandle.Alloc(tradeClient));
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return IntPtr.Zero; }
        }

        /// <summary>
        /// 获取K线客户端
        /// </summary>
        /// <param name="inst">实例</param>
        /// <param name="tradingMode">交易模式</param>
        /// <param name="exchange">交易所</param>
        /// <returns>K线客户端</returns>
        [UnmanagedCallersOnly(EntryPoint = "get_kline_socket_client")]
        public unsafe static IntPtr GetKlineSocketClient(IntPtr inst, int tradingMode, sbyte* exchange)
        {
            try
            {
                var socketClient = GCHandle.FromIntPtr(inst).Target as IExchangeSocketClient;
                var exchangeName = new string(exchange);
                var klineClient = socketClient?.GetKlineClient((TradingMode)tradingMode, exchangeName);
                return GCHandle.ToIntPtr(GCHandle.Alloc(klineClient));
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return IntPtr.Zero; }
        }

        /// <summary>
        /// 获取余额客户端
        /// </summary>
        /// <param name="inst">实例</param>
        /// <param name="tradingMode">交易模式</param>
        /// <param name="exchange">交易所</param>
        /// <returns>余额客户端</returns>
        [UnmanagedCallersOnly(EntryPoint = "get_balance_socket_client")]
        public unsafe static IntPtr GetBalanceSocketClient(IntPtr inst, int tradingMode, sbyte* exchange)
        {
            try
            {
                var socketClient = GCHandle.FromIntPtr(inst).Target as IExchangeSocketClient;
                var balanceClient = socketClient?.GetBalanceClient((TradingMode)tradingMode, new string(exchange));
                return GCHandle.ToIntPtr(GCHandle.Alloc(balanceClient));
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return IntPtr.Zero; }
        }

        /// <summary>
        /// 获取用户交易客户端
        /// </summary>
        /// <param name="inst">实例</param>
        /// <param name="tradingMode">交易模式</param>
        /// <param name="exchange">交易所</param>
        /// <returns>用户交易客户端</returns>
        [UnmanagedCallersOnly(EntryPoint = "get_user_trade_socket_client")]
        public unsafe static IntPtr GetUserTradeSocketClient(IntPtr inst, int tradingMode, sbyte* exchange)
        {
            try
            {
                var socketClient = GCHandle.FromIntPtr(inst).Target as IExchangeSocketClient;
                var userTradeClient = socketClient?.GetUserTradeClient((TradingMode)tradingMode, new string(exchange));
                return GCHandle.ToIntPtr(GCHandle.Alloc(userTradeClient));
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return IntPtr.Zero; }
        }

        /// <summary>
        /// 获取现货订单客户端
        /// </summary>
        /// <param name="inst">实例</param>
        /// <param name="exchange">交易所</param>
        /// <returns>现货订单客户端</returns>
        [UnmanagedCallersOnly(EntryPoint = "get_spot_order_socket_client")]
        public unsafe static IntPtr GetSpotOrderSocketClient(IntPtr inst, sbyte* exchange)
        {
            try
            {
                var socketClient = GCHandle.FromIntPtr(inst).Target as IExchangeSocketClient;
                var spotOrderClient = socketClient?.GetSpotOrderClient(new string(exchange));
                return GCHandle.ToIntPtr(GCHandle.Alloc(spotOrderClient));
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return IntPtr.Zero; }
        }

        /// <summary>
        /// 获取期货订单客户端
        /// </summary>
        /// <param name="inst">实例</param>
        /// <param name="tradingMode">交易模式</param>
        /// <param name="exchange">交易所</param>
        /// <returns>期货订单客户端</returns>
        [UnmanagedCallersOnly(EntryPoint = "get_futures_order_socket_client")]
        public unsafe static IntPtr GetFuturesOrderSocketClient(IntPtr inst, int tradingMode, sbyte* exchange)
        {
            try
            {
                var socketClient = GCHandle.FromIntPtr(inst).Target as IExchangeSocketClient;
                var futuresOrderClient = socketClient?.GetFuturesOrderClient((TradingMode)tradingMode, new string(exchange));
                return GCHandle.ToIntPtr(GCHandle.Alloc(futuresOrderClient));
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return IntPtr.Zero; }
        }

        /// <summary>
        /// 获取持仓客户端
        /// </summary>
        /// <param name="inst">实例</param>
        /// <param name="tradingMode">交易模式</param>
        /// <param name="exchange">交易所</param>
        /// <returns>持仓客户端</returns>
        [UnmanagedCallersOnly(EntryPoint = "get_position_socket_client")]
        public unsafe static IntPtr GetPositionSocketClient(IntPtr inst, int tradingMode, sbyte* exchange)
        {
            try
            {
                var socketClient = GCHandle.FromIntPtr(inst).Target as IExchangeSocketClient;
                var positionClient = socketClient?.GetPositionClient((TradingMode)tradingMode, new string(exchange));
                return GCHandle.ToIntPtr(GCHandle.Alloc(positionClient));
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return IntPtr.Zero; }
        }

        /// <summary>
        /// 订阅所有现货行情更新
        /// </summary>
        /// <param name="inst">实例</param>
        /// <param name="tradingMode">交易模式</param>
        /// <param name="target">目标</param>
        /// <param name="callbackPtr">回调函数指针</param>
        /// <param name="result">结果</param>
        /// <returns>0: 成功, 1: 失败</returns>
        [UnmanagedCallersOnly(EntryPoint = "subscribe_to_all_tickers_updates")]
        [DNNE.C99DeclCode(@"
struct ExExchangeNativeResult{
    int8_t Success;
    int32_t ErrorCode;
    char ErrorMessage[4096];
    char Exchange[64];
    void* Data;
};")]
        public unsafe static int SubscribeToAllTickersUpdates(IntPtr inst, int tradingMode, IntPtr target, IntPtr callbackPtr, [DNNE.C99Type("struct ExExchangeNativeResult*")] ExExchangeNativeResult* result)
        {
            try
            {
                var tickersClient = GCHandle.FromIntPtr(inst).Target as ITickersSocketClient;
                var exchange = tickersClient?.Exchange!;
                var callback = Marshal.GetDelegateForFunctionPointer<TickerCallback>(callbackPtr);
                var webResult = tickersClient!.SubscribeToAllTickersUpdatesAsync(new SubscribeAllTickersRequest((TradingMode)tradingMode, exchangeParameters: null), (update) =>
                {
                    if (update != null && update.Data != null)
                    {
                        foreach (var ticker in update.Data)
                        {
                            callback(target,
                                exchange,
                                ticker.Symbol,
                                (double)(ticker.LastPrice ?? 0m),
                                (double)(ticker.HighPrice ?? 0m),
                                (double)(ticker.LowPrice ?? 0m),
                                (double)ticker.Volume,
                                (double)(ticker.ChangePercentage ?? 0m)
                            );
                        }
                    }
                    else
                    {
                        Console.WriteLine("data is null");
                    }
                }, CancellationToken.None).GetAwaiter().GetResult();
                SetExchangeNativeResult(result, webResult!.Success, webResult.Error?.Code ?? 0, webResult.Error?.Message ?? string.Empty, exchange);
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                SetExchangeNativeResult(result, false, 1, ex.Message, string.Empty);
                return 1;
            }
        }

        /// <summary>
        /// 订阅现货行情更新
        /// </summary>
        /// <param name="inst">实例</param>
        /// <param name="symbol">符号</param>
        /// <param name="target">目标</param>
        /// <param name="callbackPtr">回调函数指针</param>
        /// <param name="result">结果</param>
        /// <returns>0: 成功, 1: 失败</returns>
        [UnmanagedCallersOnly(EntryPoint = "subscribe_to_ticker_updates")]
        public unsafe static int SubscribeToTickerUpdates(IntPtr inst, IntPtr symbol, IntPtr target, IntPtr callbackPtr, [DNNE.C99Type("struct ExExchangeNativeResult*")] ExExchangeNativeResult* result)
        {
            try
            {
                var tickerClient = GCHandle.FromIntPtr(inst).Target as ITickerSocketClient;
                var exchange = tickerClient?.Exchange!;
                var callback = Marshal.GetDelegateForFunctionPointer<TickerCallback>(callbackPtr);
                var sharedSymbol = GCHandle.FromIntPtr(symbol).Target as SharedSymbol;
                var webResult = tickerClient?.SubscribeToTickerUpdatesAsync(new SubscribeTickerRequest(sharedSymbol!, exchangeParameters: null), (update) =>
                {
                    callback(target,
                        exchange,
                        update.Data.Symbol,
                        (double)(update.Data.LastPrice ?? 0m),
                        (double)(update.Data.HighPrice ?? 0m),
                        (double)(update.Data.LowPrice ?? 0m),
                        (double)update.Data.Volume,
                        (double)(update.Data.ChangePercentage ?? 0m)
                        );
                }, CancellationToken.None).GetAwaiter().GetResult();
                SetExchangeNativeResult(result, webResult!.Success, webResult.Error?.Code ?? 0, webResult.Error?.Message ?? string.Empty, exchange);
                return 0;
            }
            catch (Exception ex)
            {
                SetExchangeNativeResult(result, false, 1, ex.Message, string.Empty);
                return 1;
            }
        }

        /// <summary>
        /// 订阅订单簿更新
        /// </summary>
        /// <param name="inst">实例</param>
        /// <param name="symbol">符号</param>
        /// <param name="target">目标</param>
        /// <param name="callbackPtr">回调函数指针</param>
        /// <param name="result">结果</param>
        /// <returns>0: 成功, 1: 失败</returns>
        [UnmanagedCallersOnly(EntryPoint = "subscribe_to_order_book_updates")]
        public unsafe static int SubscribeToOrderBookUpdates(IntPtr inst, IntPtr symbol, IntPtr target, IntPtr callbackPtr, [DNNE.C99Type("struct ExExchangeNativeResult*")] ExExchangeNativeResult* result)
        {
            try
            {
                var orderBookClient = GCHandle.FromIntPtr(inst).Target as IOrderBookSocketClient;
                var exchange = orderBookClient?.Exchange!;
                var callback = Marshal.GetDelegateForFunctionPointer<OrderBookCallback>(callbackPtr);
                var sharedSymbol = GCHandle.FromIntPtr(symbol).Target as SharedSymbol;
                var symbolName = $"{sharedSymbol!.BaseAsset}/{sharedSymbol!.QuoteAsset}";
                var webResult = orderBookClient?.SubscribeToOrderBookUpdatesAsync(new SubscribeOrderBookRequest(sharedSymbol, exchangeParameters: null), (update) =>
                {
                    var bids = update.Data.Bids as List<ISymbolOrderBookEntry> ?? update.Data.Bids.ToList();
                    var asks = update.Data.Asks as List<ISymbolOrderBookEntry> ?? update.Data.Asks.ToList();
                    var bidsArray = bids.ConvertAll(x => new ExSymbolOrderBookEntry { Quantity = (double)x.Quantity, Price = (double)x.Price }).ToArray();
                    var asksArray = asks.ConvertAll(x => new ExSymbolOrderBookEntry { Quantity = (double)x.Quantity, Price = (double)x.Price }).ToArray();
                    callback(target, exchange, symbolName, bidsArray, bids.Count, asksArray, asks.Count);
                }, CancellationToken.None).GetAwaiter().GetResult();
                SetExchangeNativeResult(result, webResult!.Success, webResult.Error?.Code ?? 0, webResult.Error?.Message ?? string.Empty, exchange);
                return 0;
            }
            catch (Exception ex)
            {
                SetExchangeNativeResult(result, false, 1, ex.Message, string.Empty);
                return 1;
            }
        }

        /// <summary>
        /// 订阅BookTicker更新
        /// </summary>
        /// <param name="inst">实例</param>
        /// <param name="symbol">符号</param>
        /// <param name="target">目标</param>
        /// <param name="callbackPtr">回调函数指针</param>
        /// <param name="result">结果</param>
        /// <returns>0: 成功, 1: 失败</returns>
        [UnmanagedCallersOnly(EntryPoint = "subscribe_to_book_ticker_updates")]
        public unsafe static int SubscribeToBookTickerUpdates(IntPtr inst, IntPtr symbol, IntPtr target, IntPtr callbackPtr, [DNNE.C99Type("struct ExExchangeNativeResult*")] ExExchangeNativeResult* result)
        {
            try
            {
                var bookTickerClient = GCHandle.FromIntPtr(inst).Target as IBookTickerSocketClient;
                var exchange = bookTickerClient?.Exchange!;
                var callback = Marshal.GetDelegateForFunctionPointer<BookTickerCallback>(callbackPtr);
                var sharedSymbol = GCHandle.FromIntPtr(symbol).Target as SharedSymbol;
                var symbolName = $"{sharedSymbol!.BaseAsset}/{sharedSymbol!.QuoteAsset}";
                var webResult = bookTickerClient?.SubscribeToBookTickerUpdatesAsync(new SubscribeBookTickerRequest(sharedSymbol, exchangeParameters: null), (update) =>
                {
                    try
                    {
                        callback(target,
                            exchange,
                            symbolName,
                            (double)update.Data.BestAskPrice,
                            (double)update.Data.BestAskQuantity,
                            (double)update.Data.BestBidPrice,
                            (double)update.Data.BestBidQuantity
                        );
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.StackTrace);
                    }
                }, CancellationToken.None).GetAwaiter().GetResult();
                SetExchangeNativeResult(result, webResult!.Success, webResult.Error?.Code ?? 0, webResult.Error?.Message ?? string.Empty, exchange);
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("SubscribeToBookTickerUpdates error");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                SetExchangeNativeResult(result, false, 1, ex.Message, string.Empty);
                return 1;
            }
        }

        /// <summary>
        /// 订阅交易更新
        /// </summary>
        /// <param name="inst">实例</param>
        /// <param name="symbol">符号</param>
        /// <param name="target">目标</param>
        /// <param name="callbackPtr">回调函数指针</param>
        /// <param name="result">结果</param>
        [UnmanagedCallersOnly(EntryPoint = "subscribe_to_trade_updates")]
        public unsafe static int SubscribeToTradeUpdates(IntPtr inst, IntPtr symbol, IntPtr target, IntPtr callbackPtr, [DNNE.C99Type("struct ExExchangeNativeResult*")] ExExchangeNativeResult* result)
        {
            try
            {
                var tradeClient = GCHandle.FromIntPtr(inst).Target as ITradeSocketClient;
                var exchange = tradeClient?.Exchange!;
                var callback = Marshal.GetDelegateForFunctionPointer<TradeCallback>(callbackPtr);
                var sharedSymbol = GCHandle.FromIntPtr(symbol).Target as SharedSymbol;
                var symbolName = $"{sharedSymbol!.BaseAsset}/{sharedSymbol!.QuoteAsset}";
                var webResult = tradeClient?.SubscribeToTradeUpdatesAsync(new SubscribeTradeRequest(sharedSymbol, exchangeParameters: null), (update) =>
                {
                    var data = update.Data;
                    foreach (var trade in data)
                    {
                        callback(target,
                            exchange,
                            symbolName,
                            (double)trade.Quantity,
                            (double)trade.Price,
                            trade.Timestamp.ToString(NativeUtils.DATE_TIME_FORMAT),
                            NativeUtils.DateTimeToUnixTimestampMs(trade.Timestamp),
                            (int)(trade.Side ?? SharedOrderSide.Buy)
                        );
                    }
                }, CancellationToken.None).GetAwaiter().GetResult();
                SetExchangeNativeResult(result, webResult!.Success, webResult.Error?.Code ?? 0, webResult.Error?.Message ?? string.Empty, exchange);
                return 0;
            }
            catch (Exception ex)
            {
                SetExchangeNativeResult(result, false, 1, ex.Message, string.Empty);
                return 1;
            }
        }

        /// <summary>
        /// 订阅K线更新
        /// </summary>
        /// <param name="inst">实例</param>
        /// <param name="symbol">符号</param>
        /// <param name="interval">K线间隔</param>
        /// <param name="target">目标</param>
        /// <param name="callbackPtr">回调函数指针</param>
        /// <param name="result">结果</param>
        /// <returns>0: 成功, 1: 失败</returns>
        [UnmanagedCallersOnly(EntryPoint = "subscribe_to_kline_updates")]
        public unsafe static int SubscribeToKlineUpdates(IntPtr inst, IntPtr symbol, int interval, IntPtr target, IntPtr callbackPtr, [DNNE.C99Type("struct ExExchangeNativeResult*")] ExExchangeNativeResult* result)
        {
            try
            {
                var klineClient = GCHandle.FromIntPtr(inst).Target as IKlineSocketClient;
                var exchange = klineClient?.Exchange!;
                var callback = Marshal.GetDelegateForFunctionPointer<KlineCallback>(callbackPtr);
                var sharedSymbol = GCHandle.FromIntPtr(symbol).Target as SharedSymbol;
                var sharedInterval = (SharedKlineInterval)interval;
                var symbolName = $"{sharedSymbol!.BaseAsset}/{sharedSymbol!.QuoteAsset}";
                var webResult = klineClient?.SubscribeToKlineUpdatesAsync(new SubscribeKlineRequest(sharedSymbol, sharedInterval, exchangeParameters: null), (update) =>
                {
                    var openTimeBytes = Encoding.UTF8.GetBytes(update.Data.OpenTime.ToString(NativeUtils.DATE_TIME_FORMAT));
                    fixed (byte* openTimeBytesPtr = openTimeBytes)
                    {
                        callback(target,
                            exchange,
                            symbolName,
                            update.Data.OpenTime.ToString(NativeUtils.DATE_TIME_FORMAT),
                            NativeUtils.DateTimeToUnixTimestampMs(update.Data.OpenTime),
                            (double)update.Data.ClosePrice,
                            (double)update.Data.HighPrice,
                            (double)update.Data.LowPrice,
                            (double)update.Data.OpenPrice,
                            (double)update.Data.Volume
                            );
                    }
                }, CancellationToken.None).GetAwaiter().GetResult();
                SetExchangeNativeResult(result, webResult!.Success, webResult.Error?.Code ?? 0, webResult.Error?.Message ?? string.Empty, exchange);
                return 0;
            }
            catch (Exception ex)
            {
                SetExchangeNativeResult(result, false, 1, ex.Message, string.Empty);
                return 1;
            }
        }

        /// <summary>
        /// 订阅余额更新
        /// </summary>
        /// <param name="inst">实例</param>
        /// <param name="tradingMode">交易模式</param>
        /// <param name="listenKey">监听密钥</param>
        /// <param name="target">目标</param>
        /// <param name="callbackPtr">回调函数指针</param>
        /// <param name="result">结果</param>
        /// <returns>0: 成功, 1: 失败</returns>
        [UnmanagedCallersOnly(EntryPoint = "subscribe_to_balance_updates")]
        public unsafe static int SubscribeToBalanceUpdates(IntPtr inst, int tradingMode, sbyte* listenKey, IntPtr target, IntPtr callbackPtr, [DNNE.C99Type("struct ExExchangeNativeResult*")] ExExchangeNativeResult* result)
        {
            try
            {
                var listenKeyStr = new string(listenKey);
                if (string.IsNullOrEmpty(listenKeyStr))
                {
                    listenKeyStr = null;
                }
                var balanceClient = GCHandle.FromIntPtr(inst).Target as IBalanceSocketClient;
                var exchange = balanceClient?.Exchange!;
                var callback = Marshal.GetDelegateForFunctionPointer<BalanceCallback>(callbackPtr);
                TradingMode? tradingModeEnum = tradingMode == -1 ? null : (TradingMode)tradingMode;
                var webResult = balanceClient?.SubscribeToBalanceUpdatesAsync(new SubscribeBalancesRequest(listenKeyStr, tradingModeEnum, exchangeParameters: null), (update) =>
                {
                    foreach (var balance in update.Data)
                    {
                        // Console.WriteLine(balance);
                        callback(target,
                            exchange,
                            balance.Asset,
                            (double)balance.Available,
                            (double)balance.Total
                        );
                    }
                }, CancellationToken.None).GetAwaiter().GetResult();
                SetExchangeNativeResult(result, webResult!.Success, webResult.Error?.Code ?? 0, webResult.Error?.Message ?? string.Empty, exchange);
                return 0;
            }
            catch (Exception ex)
            {
                SetExchangeNativeResult(result, false, 1, ex.Message, string.Empty);
                return 1;
            }
        }

        /// <summary>
        /// 订阅现货订单更新
        /// </summary>
        /// <param name="inst">实例</param>
        /// <param name="listenKey">监听密钥</param>
        /// <param name="target">目标</param>
        /// <param name="callbackPtr">回调函数指针</param>
        /// <param name="result">结果</param>
        [UnmanagedCallersOnly(EntryPoint = "subscribe_to_spot_order_updates")]
        public unsafe static int SubscribeToSpotOrderUpdates(IntPtr inst, sbyte* listenKey, IntPtr target, IntPtr callbackPtr, [DNNE.C99Type("struct ExExchangeNativeResult*")] ExExchangeNativeResult* result)
        {
            try
            {
                var listenKeyStr = new string(listenKey);
                if (string.IsNullOrEmpty(listenKeyStr))
                {
                    listenKeyStr = null;
                }
                var spotOrderClient = GCHandle.FromIntPtr(inst).Target as ISpotOrderSocketClient;
                var exchange = spotOrderClient?.Exchange!;
                var callback = Marshal.GetDelegateForFunctionPointer<SpotOrderCallback>(callbackPtr);
                var webResult = spotOrderClient?.SubscribeToSpotOrderUpdatesAsync(new SubscribeSpotOrderRequest(listenKeyStr, exchangeParameters: null), (update) =>
                {
                    foreach (var order in update.Data)
                    {
                        callback(target,
                            exchange,
                            order.Symbol,
                            order.OrderId,
                            order.OrderType,
                            order.Side,
                            order.Status,
                            order.TimeInForce ?? SharedTimeInForce.GoodTillCanceled,
                            (double)(order.Quantity ?? 0m),
                            (double)(order.QuantityFilled ?? 0m),
                            (double)(order.QuoteQuantity ?? 0m),
                            (double)(order.QuoteQuantityFilled ?? 0m),
                            (double)(order.OrderPrice ?? 0m),
                            (double)(order.AveragePrice ?? 0m),
                            order.ClientOrderId ?? string.Empty,
                            order.FeeAsset ?? string.Empty,
                            (double)(order.Fee ?? 0m),
                            order.CreateTime?.ToString(NativeUtils.DATE_TIME_FORMAT) ?? string.Empty,
                            order.UpdateTime?.ToString(NativeUtils.DATE_TIME_FORMAT) ?? string.Empty,
                            NativeUtils.DateTimeToUnixTimestampMs(order.CreateTime ?? DateTime.MinValue),
                            NativeUtils.DateTimeToUnixTimestampMs(order.UpdateTime ?? DateTime.MinValue)
                        );
                    }
                }, CancellationToken.None).GetAwaiter().GetResult();
                SetExchangeNativeResult(result, webResult!.Success, webResult.Error?.Code ?? 0, webResult.Error?.Message ?? string.Empty, exchange);
                return 0;
            }
            catch (Exception ex)
            {
                SetExchangeNativeResult(result, false, 1, ex.Message, string.Empty);
                return 1;
            }
        }

        /// <summary>
        /// 订阅期货订单更新
        /// </summary>
        /// <param name="inst">实例</param>
        /// <param name="listenKey">监听密钥</param>
        /// <param name="target">目标</param>
        /// <param name="callbackPtr">回调函数指针</param>
        /// <param name="result">结果</param>
        /// <returns>0: 成功, 1: 失败</returns>
        [UnmanagedCallersOnly(EntryPoint = "subscribe_to_futures_order_updates")]
        public unsafe static int SubscribeToFuturesOrderUpdates(IntPtr inst, sbyte* listenKey, IntPtr target, IntPtr callbackPtr, [DNNE.C99Type("struct ExExchangeNativeResult*")] ExExchangeNativeResult* result)
        {
            try
            {
                var listenKeyStr = new string(listenKey);
                if (string.IsNullOrEmpty(listenKeyStr))
                {
                    listenKeyStr = null;
                }
                var futuresOrderClient = GCHandle.FromIntPtr(inst).Target as IFuturesOrderSocketClient;
                var exchange = futuresOrderClient?.Exchange!;
                var callback = Marshal.GetDelegateForFunctionPointer<FuturesOrderCallback>(callbackPtr);
                var webResult = futuresOrderClient?.SubscribeToFuturesOrderUpdatesAsync(new SubscribeFuturesOrderRequest(listenKeyStr, exchangeParameters: null), (update) =>
                {
                    foreach (var order in update.Data)
                    {
                        callback(target,
                            exchange,
                            order.Symbol,
                            order.OrderId,
                            order.OrderType,
                            order.Side,
                            order.Status,
                            order.TimeInForce ?? SharedTimeInForce.GoodTillCanceled,
                            order.PositionSide ?? SharedPositionSide.Long,
                            order.ReduceOnly ?? false,
                            (double)(order.Quantity ?? 0m),
                            (double)(order.QuantityFilled ?? 0m),
                            (double)(order.QuoteQuantity ?? 0m),
                            (double)(order.QuoteQuantityFilled ?? 0m),
                            (double)(order.OrderPrice ?? 0m),
                            (double)(order.AveragePrice ?? 0m),
                            order.ClientOrderId ?? string.Empty,
                            order.FeeAsset ?? string.Empty,
                            (double)(order.Fee ?? 0m),
                            (double)(order.Leverage ?? 0m),
                            order.CreateTime?.ToString(NativeUtils.DATE_TIME_FORMAT) ?? string.Empty,
                            order.UpdateTime?.ToString(NativeUtils.DATE_TIME_FORMAT) ?? string.Empty,
                            NativeUtils.DateTimeToUnixTimestampMs(order.CreateTime ?? DateTime.MinValue),
                            NativeUtils.DateTimeToUnixTimestampMs(order.UpdateTime ?? DateTime.MinValue)
                        );
                    }
                }, CancellationToken.None).GetAwaiter().GetResult();
                SetExchangeNativeResult(result, webResult!.Success, webResult.Error?.Code ?? 0, webResult.Error?.Message ?? string.Empty, exchange);
                return 0;
            }
            catch (Exception ex)
            {
                SetExchangeNativeResult(result, false, 1, ex.Message, string.Empty);
                return 1;
            }
        }

        /// <summary>
        /// 订阅持仓更新
        /// </summary>
        /// <param name="inst">实例</param>
        /// <param name="listenKey">监听密钥</param>
        /// <param name="target">目标</param>
        /// <param name="callbackPtr">回调函数指针</param>
        /// <param name="result">结果</param>
        /// <returns>0: 成功, 1: 失败</returns>
        [UnmanagedCallersOnly(EntryPoint = "subscribe_to_position_updates")]
        public unsafe static int SubscribeToPositionUpdates(IntPtr inst, sbyte* listenKey, IntPtr target, IntPtr callbackPtr, [DNNE.C99Type("struct ExExchangeNativeResult*")] ExExchangeNativeResult* result)
        {
            try
            {
                var listenKeyStr = new string(listenKey);
                if (string.IsNullOrEmpty(listenKeyStr))
                {
                    listenKeyStr = null;
                }
                var positionClient = GCHandle.FromIntPtr(inst).Target as IPositionSocketClient;
                var exchange = positionClient?.Exchange!;
                var callback = Marshal.GetDelegateForFunctionPointer<PositionCallback>(callbackPtr);
                var webResult = positionClient?.SubscribeToPositionUpdatesAsync(new SubscribePositionRequest(listenKeyStr, exchangeParameters: null), (update) =>
                {
                    foreach (var position in update.Data)
                    {
                        callback(
                            target,
                            exchange,
                            position.Symbol,
                            (double)position.PositionSize,
                            position.PositionSide,
                            (double)(position.AverageOpenPrice ?? 0m),
                            (double)(position.UnrealizedPnl ?? 0m),
                            (double)(position.LiquidationPrice ?? 0m),
                            (double)(position.Leverage ?? 0m),
                            position.UpdateTime?.ToString(NativeUtils.DATE_TIME_FORMAT) ?? string.Empty,
                            NativeUtils.DateTimeToUnixTimestampMs(position.UpdateTime ?? DateTime.MinValue)
                        );
                    }
                }, CancellationToken.None).GetAwaiter().GetResult();
                SetExchangeNativeResult(result, webResult!.Success, webResult.Error?.Code ?? 0, webResult.Error?.Message ?? string.Empty, exchange);
                return 0;
            }
            catch (Exception ex)
            {
                SetExchangeNativeResult(result, false, 1, ex.Message, string.Empty);
                return 1;
            }
        }

        /// <summary>
        /// 设置ExExchangeWebResult error code, error message, exchange
        /// </summary>
        /// <param name="result"></param>
        /// <param name="success"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorMessage"></param>
        /// <param name="exchange"></param>
        private unsafe static void SetExchangeNativeResult(ExExchangeNativeResult* result, bool success, int errorCode, string errorMessage, string exchange)
        {
            if (result == null)
            {
                Console.WriteLine("result is null");
                return;
            }
            result->Success = success;
            NativeRestMethods.SetFixedByteArray(result->ErrorMessage, NativeUtils.MAX_ERROR_MESSAGE_LENGTH, errorMessage);
            result->ErrorCode = errorCode;
            NativeRestMethods.SetFixedByteArray(result->Exchange, NativeUtils.MAX_EXCHANGE_LENGTH, exchange);
        }

        /// <summary>
        /// 现货行情回调函数
        /// </summary>
        /// <param name="target">目标</param>
        /// <param name="exchange">交易所</param>
        /// <param name="symbol">符号</param>
        /// <param name="lastPrice">最新价格</param>
        /// <param name="highPrice">最高价格</param>
        /// <param name="lowPrice">最低价格</param>
        /// <param name="volume">成交量</param>
        /// <param name="changePercentage">涨跌幅</param>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void TickerCallback(
            IntPtr target,
            [MarshalAs(UnmanagedType.LPStr)] string exchange,
            [MarshalAs(UnmanagedType.LPStr)] string symbol,
            double lastPrice,
            double highPrice,
            double lowPrice,
            double volume,
            double changePercentage);

        [StructLayout(LayoutKind.Sequential)]
        internal struct ExSymbolOrderBookEntry
        {
            public double Quantity; // 使用 double 来表示数量
            public double Price;    // 使用 double 来表示价格
        }

        /// <summary>
        /// 订单簿回调函数
        /// </summary>
        /// <param name="target">目标</param>
        /// <param name="exchange">交易所</param>
        /// <param name="symbol">符号</param>
        /// <param name="bids">买单</param>
        /// <param name="asks">卖单</param>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void OrderBookCallback(
            IntPtr target,
            [MarshalAs(UnmanagedType.LPStr)] string exchange,
            [MarshalAs(UnmanagedType.LPStr)] string symbol,
            ExSymbolOrderBookEntry[] Bids, // 使用数组来表示买单
            int BidsCount,               // 买单数量
            ExSymbolOrderBookEntry[] Asks, // 使用数组来表示卖单
            int AsksCount);              // 卖单数量

        /// <summary>
        /// BookTicker回调函数
        /// </summary>
        /// <param name="target">目标</param>
        /// <param name="exchange">交易所</param>
        /// <param name="symbol">符号</param>
        /// <param name="BestAskPrice">最佳卖价</param>
        /// <param name="BestAskQuantity">最佳卖量</param>
        /// <param name="BestBidPrice">最佳买价</param>
        /// <param name="BestBidQuantity"></param>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void BookTickerCallback(
            IntPtr target,
            [MarshalAs(UnmanagedType.LPStr)] string exchange,
            [MarshalAs(UnmanagedType.LPStr)] string symbol,
            double BestAskPrice,
            double BestAskQuantity,
            double BestBidPrice,
            double BestBidQuantity);

        /// <summary>
        /// 交易回调函数
        /// </summary>
        /// <param name="target">目标</param>
        /// <param name="exchange">交易所</param>
        /// <param name="symbol">符号</param>
        /// <param name="price">价格</param>
        /// <param name="quantity">数量</param>
        /// <param name="side">方向</param>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void TradeCallback(
            IntPtr target,
            [MarshalAs(UnmanagedType.LPStr)] string exchange,
            [MarshalAs(UnmanagedType.LPStr)] string symbol,
            double quantity,
            double price,
            [MarshalAs(UnmanagedType.LPStr)] string timestamp,
            Int64 timestampMs,
            int side);

        /// <summary>
        /// K线回调函数
        /// </summary>
        /// <param name="target">目标</param>
        /// <param name="exchange">交易所</param>
        /// <param name="symbol">符号</param>
        /// <param name="openTime">开盘时间</param>
        /// <param name="closePrice">收盘价格</param>
        /// <param name="highPrice">最高价格</param>
        /// <param name="lowPrice">最低价格</param>
        /// <param name="openPrice">开盘价格</param>
        /// <param name="volume">成交量</param>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void KlineCallback(
            IntPtr target,
            [MarshalAs(UnmanagedType.LPStr)] string exchange,
            [MarshalAs(UnmanagedType.LPStr)] string symbol,
            [MarshalAs(UnmanagedType.LPStr)] string openTime,
            Int64 openTimeMs,
            double closePrice,
            double highPrice,
            double lowPrice,
            double openPrice,
            double volume
        );

        /// <summary>
        /// 余额回调函数
        /// </summary>
        /// <param name="target">目标</param>
        /// <param name="exchange">交易所</param>
        /// <param name="asset">资产</param>
        /// <param name="available">可用余额</param>
        /// <param name="total">总余额</param>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void BalanceCallback(
            IntPtr target,
            [MarshalAs(UnmanagedType.LPStr)] string exchange,
            [MarshalAs(UnmanagedType.LPStr)] string asset,
            double available,
            double total
            );

        /// <summary>
        /// 现货订单回调函数
        /// </summary>
        /// <param name="target">目标</param>
        /// <param name="exchange"></param>
        /// <param name="symbol"></param>
        /// <param name="orderId"></param>
        /// <param name="orderType"></param>
        /// <param name="side"></param>
        /// <param name="status"></param>
        /// <param name="timeInForce"></param>
        /// <param name="quantity"></param>
        /// <param name="quantityFilled"></param>
        /// <param name="quoteQuantity"></param>
        /// <param name="quoteQuantityFilled"></param>
        /// <param name="orderPrice"></param>
        /// <param name="averagePrice"></param>
        /// <param name="clientOrderId"></param>
        /// <param name="feeAsset"></param>
        /// <param name="fee"></param>
        /// <param name="createTime"></param>
        /// <param name="updateTime"></param>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void SpotOrderCallback(
            IntPtr target,
            [MarshalAs(UnmanagedType.LPStr)] string exchange,
            [MarshalAs(UnmanagedType.LPStr)] string symbol,
            [MarshalAs(UnmanagedType.LPStr)] string orderId,
            SharedOrderType orderType,
            SharedOrderSide side,
            SharedOrderStatus status,
            SharedTimeInForce timeInForce,
            double quantity,
            double quantityFilled,
            double quoteQuantity,
            double quoteQuantityFilled,
            double orderPrice,
            double averagePrice,
            [MarshalAs(UnmanagedType.LPStr)] string clientOrderId,
            [MarshalAs(UnmanagedType.LPStr)] string feeAsset,
            double fee,
            [MarshalAs(UnmanagedType.LPStr)] string createTime,
            [MarshalAs(UnmanagedType.LPStr)] string updateTime,
            Int64 createTimeMs,
            Int64 updateTimeMs
            );

        /// <summary>
        /// 期货订单回调函数
        /// </summary>
        /// <param name="target">目标</param>
        /// <param name="exchange"></param>
        /// <param name="symbol"></param>
        /// <param name="orderId"></param>
        /// <param name="orderType"></param>
        /// <param name="side"></param>
        /// <param name="status"></param>
        /// <param name="timeInForce"></param>
        /// <param name="positionSide"></param>
        /// <param name="reduceOnly"></param>
        /// <param name="quantity"></param>
        /// <param name="quantityFilled"></param>
        /// <param name="quoteQuantity"></param>
        /// <param name="quoteQuantityFilled"></param>
        /// <param name="orderPrice"></param>
        /// <param name="averagePrice"></param>
        /// <param name="clientOrderId"></param>
        /// <param name="feeAsset"></param>
        /// <param name="fee"></param>
        /// <param name="leverage"></param>
        /// <param name="createTime"></param>
        /// <param name="updateTime"></param>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void FuturesOrderCallback(
            IntPtr target,
            [MarshalAs(UnmanagedType.LPStr)] string exchange,
            [MarshalAs(UnmanagedType.LPStr)] string symbol,
            [MarshalAs(UnmanagedType.LPStr)] string orderId,
            SharedOrderType orderType,
            SharedOrderSide side,
            SharedOrderStatus status,
            SharedTimeInForce timeInForce,
            SharedPositionSide positionSide,
            bool reduceOnly,
            double quantity,
            double quantityFilled,
            double quoteQuantity,
            double quoteQuantityFilled,
            double orderPrice,
            double averagePrice,
            [MarshalAs(UnmanagedType.LPStr)] string clientOrderId,
            [MarshalAs(UnmanagedType.LPStr)] string feeAsset,
            double fee,
            double leverage,
            [MarshalAs(UnmanagedType.LPStr)] string createTime,
            [MarshalAs(UnmanagedType.LPStr)] string updateTime,
            Int64 createTimeMs,
            Int64 updateTimeMs
        );

        /// <summary>
        /// 持仓回调函数
        /// </summary>
        /// <param name="target">目标</param>
        /// <param name="exchange">交易所</param>
        /// <param name="symbol">符号</param>
        /// <param name="positionSize">持仓数量</param>
        /// <param name="positionSide">持仓方向</param>
        /// <param name="averageOpenPrice">平均开仓价格</param>
        /// <param name="unrealizedPnl">未实现盈亏</param>
        /// <param name="liquidationPrice">强平价格</param>
        /// <param name="leverage">杠杆</param>
        /// <param name="updateTime">更新时间</param>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void PositionCallback(
            IntPtr target,
            [MarshalAs(UnmanagedType.LPStr)] string exchange,
            [MarshalAs(UnmanagedType.LPStr)] string symbol,
            double positionSize,
            SharedPositionSide positionSide,
            double averageOpenPrice,
            double unrealizedPnl,
            double liquidationPrice,
            double leverage,
            [MarshalAs(UnmanagedType.LPStr)] string updateTime,
            Int64 updateTimeMs
        );
    }
}