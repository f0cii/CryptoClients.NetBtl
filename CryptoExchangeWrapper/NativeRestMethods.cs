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
using Microsoft.Extensions.Logging;
using System.Globalization;
using Binance.Net.Clients;

// Suppress since users can define custom platforms for DNNE scenarios.
#pragma warning disable CA1418 // Validate platform compatibility

[assembly: SupportedOSPlatform("SET_ASSEMBLY_PLATFORM")]
[assembly: UnsupportedOSPlatform("UNSET_ASSEMBLY_PLATFORM1")]
[assembly: UnsupportedOSPlatform("UNSET_ASSEMBLY_PLATFORM2")]
[module: SupportedOSPlatform("SET_MODULE_PLATFORM")]
[module: UnsupportedOSPlatform("UNSET_MODULE_PLATFORM1")]
[module: UnsupportedOSPlatform("UNSET_MODULE_PLATFORM2")]
[module: UnsupportedOSPlatform("UNSET_MODULE_PLATFORM3")]

namespace CryptoExchangeWrapper
{
    [SupportedOSPlatform("SET_TYPE_PLATFORM")]
    [UnsupportedOSPlatform("UNSET_TYPE_PLATFORM1")]
    [UnsupportedOSPlatform("UNSET_TYPE_PLATFORM2")]
    [UnsupportedOSPlatform("UNSET_TYPE_PLATFORM3")]
    [UnsupportedOSPlatform("UNSET_TYPE_PLATFORM4")]
    public static class NativeRestMethods
    {
        public delegate void DontExportNameDelegate();

        [UnmanagedCallersOnly(EntryPoint = "UnmanagedSetViaEntryPointProperty")]
        public static void UnmanagedDontExportName()
        {
        }

        [UnmanagedCallersOnly]
        [DNNE.C99DeclCode(
@"#define SET_ASSEMBLY_PLATFORM
#define SET_MODULE_PLATFORM
#define SET_TYPE_PLATFORM")]
        [SupportedOSPlatform("windows")]
        public static void OnlyOnWindows()
        {
        }

        [UnmanagedCallersOnly]
        [SupportedOSPlatform("osx")]
        public static void OnlyOnOSX()
        {
        }

        [UnmanagedCallersOnly]
        [SupportedOSPlatform("linux")]
        public static void OnlyOnLinux()
        {
        }

        [UnmanagedCallersOnly]
        [SupportedOSPlatform("freebsd")]
        public static void OnlyOnFreeSBD()
        {
        }

        [UnmanagedCallersOnly]
        [DNNE.C99DeclCode("#define __SET_PLATFORM__")]
        [SupportedOSPlatform("__SET_PLATFORM__")]
        [SupportedOSPlatform("SET_METHOD_PLATFORM")]
        [UnsupportedOSPlatform("UNSET_METHOD_PLATFORM1")]
        [UnsupportedOSPlatform("UNSET_METHOD_PLATFORM2")]
        [UnsupportedOSPlatform("UNSET_METHOD_PLATFORM3")]
        [UnsupportedOSPlatform("UNSET_METHOD_PLATFORM4")]
        [UnsupportedOSPlatform("UNSET_METHOD_PLATFORM5")]
        public static void ManuallySetPlatform()
        {
        }

        [UnmanagedCallersOnly]
        [SupportedOSPlatform("__NEVER_SUPPORTED_PLATFORM__")]
        public static void NeverSupportedPlatform()
        {
        }

        [UnmanagedCallersOnly]
        [UnsupportedOSPlatform("__NEVER_UNSUPPORTED_PLATFORM__")]
        public static void NeverUnsupportedPlatform()
        {
        }

        /// <summary>
        /// 创建CryptoClients.Net.Interfaces.IExchangeRestClient实例
        /// </summary>
        /// <param name="globalOptions">CryptoClients.Net.Models.GlobalExchangeOptions实例的json字符串</param>
        /// <returns>CryptoClients.Net.Interfaces.IExchangeRestClient实例的指针</returns>
        [UnmanagedCallersOnly(EntryPoint = "create_exchange_rest_client")]
        public unsafe static IntPtr CreateExchangeRestClient(sbyte* globalOptions)
        {
            try
            {
                var globalOptionsString = new string(globalOptions);
                var exGlobalOptions = JsonSerializer.Deserialize<ExGlobalExchangeOptions>(globalOptionsString);
                //var exGlobalOptionsString = JsonSerializer.Serialize(exGlobalOptions);
                var restClient = new ExchangeRestClient(globalOptions =>
                {
                    if (exGlobalOptions?.Proxy != null)
                    {
                        globalOptions.Proxy = new CryptoExchange.Net.Objects.ApiProxy(exGlobalOptions.Proxy.Host!, exGlobalOptions.Proxy.Port, exGlobalOptions.Proxy.Username, exGlobalOptions.Proxy.Password);
                    }
                    globalOptions.RequestTimeout = TimeSpan.FromSeconds(10);
                    globalOptions.ApiCredentials = new ExchangeCredentials();
                    // globalOptions.RateLimiterEnabled = true;
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
                }, binanceRestOptions: (options) =>
                {
                    if (exGlobalOptions!.Testnet ?? false)
                    {
                        options.Environment = Binance.Net.BinanceEnvironment.Testnet;
                    }
                }, okxRestOptions: (options) =>
                {
                    if (exGlobalOptions!.Testnet ?? false)
                    {
                        options.Environment = OKX.Net.OKXEnvironment.Demo;
                    }
                }, coinExRestOptions: (options) =>
                {

                }, gateIoRestOptions: (options) =>
                {

                }, bybitRestOptions: (options) =>
                {
                    if (exGlobalOptions!.Testnet ?? false)
                    {
                        options.Environment = Bybit.Net.BybitEnvironment.Testnet;
                    }
                });
                return GCHandle.ToIntPtr(GCHandle.Alloc(restClient));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return IntPtr.Zero;
            }
        }

        /// <summary>
        /// 获取ListenKeyClient实例
        /// </summary>
        /// <param name="inst">IExchangeRestClient实例的指针</param>
        /// <param name="tradingMode">TradingMode</param>
        /// <param name="exchange">交易所名称</param>
        /// <returns>ListenKeyClient实例的指针</returns>
        [UnmanagedCallersOnly(EntryPoint = "get_listen_key_client")]
        public unsafe static IntPtr GetListenKeyClient(IntPtr inst, int tradingMode, sbyte* exchange)
        {
            try
            {
                var exchangeString = exchange != null ? new string(exchange) : string.Empty;
                var restClient = GCHandle.FromIntPtr(inst).Target as IExchangeRestClient;
                var listenKeyClient = restClient?.GetListenKeyClient((TradingMode)tradingMode, exchangeString);
                return GCHandle.ToIntPtr(GCHandle.Alloc(listenKeyClient));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return IntPtr.Zero;
            }
        }

        /// <summary>
        /// 获取ListenKey
        /// </summary>
        /// <param name="inst">IListenKeyRestClient实例的指针</param>
        /// <param name="tradingMode">TradingMode</param>
        /// <param name="exchange">交易所名称</param>
        /// <param name="result">ExExchangeWebResult实例的指针</param>
        /// <returns>0: 成功, 1: 失败</returns>
        [UnmanagedCallersOnly(EntryPoint = "start_listen_key")]
        public unsafe static int StartListenKey(IntPtr inst, int tradingMode, sbyte* exchange, [DNNE.C99Type("struct ExExchangeWebResult*")] ExExchangeWebResult* result)
        {
            var listenKeyClient = GCHandle.FromIntPtr(inst).Target as IListenKeyRestClient;
            try
            {
                var webResult = listenKeyClient?.StartListenKeyAsync(new StartListenKeyRequest((TradingMode)tradingMode, exchangeParameters: null)).GetAwaiter().GetResult();
                if (webResult == null)
                {
                    return 1;
                }
                if (!webResult.Success)
                {
                    SetExchangeWebResult(result, false, webResult.Error?.Code ?? 0, webResult.Error?.Message ?? string.Empty, webResult.Exchange);
                    return 2;
                }

                SetExchangeWebResult(result, true, 0, string.Empty, webResult.Exchange);

                ExSharedSpotListenKey* data = (ExSharedSpotListenKey*)result->Data;
                SetFixedByteArray(data->ListenKey, NativeUtils.MAX_LISTEN_KEY_LENGTH, webResult.Data);
                return 0;
            }
            catch (Exception ex)
            {
                SetExchangeWebResult(result, false, 3, ex.Message, string.Empty);
                return 3;
            }
        }

        /// <summary>
        /// 创建统一交易对符号
        /// </summary>
        /// <param name="baseAsset">基础资产</param>
        /// <param name="quoteAsset">报价资产</param>
        /// <param name="symbolName">符号名称</param>
        /// <param name="tradingMode">交易模式</param>
        /// <param name="deliverTime">交割时间</param>
        /// <returns>统一交易对符号</returns>
        [UnmanagedCallersOnly(EntryPoint = "create_shared_symbol")]
        public unsafe static IntPtr CreateSharedSymbol(sbyte* baseAsset, sbyte* quoteAsset, sbyte* symbolName, int tradingMode, sbyte* deliverTime)
        {
            // var deliverTimeString = deliverTime != null ? new string(deliverTime) : string.Empty;
            var deliverTimeString = new string(deliverTime);
            DateTime? deliverDateTime = !string.IsNullOrEmpty(deliverTimeString) ? DateTime.Parse(deliverTimeString) : null;
            var sharedSymbol = new SharedSymbol((TradingMode)tradingMode, new string(baseAsset), new string(quoteAsset), deliverDateTime);
            // Console.WriteLine($"Symbol: {sharedSymbol.BaseAsset}, {sharedSymbol.QuoteAsset}, {sharedSymbol.TradingMode}, {sharedSymbol.DeliverTime}");
            return GCHandle.ToIntPtr(GCHandle.Alloc(sharedSymbol));
        }

        /// <summary>
        /// 创建ExchangeParameters
        /// </summary>
        /// <returns>ExchangeParameters实例的指针</returns>
        [UnmanagedCallersOnly(EntryPoint = "create_exchange_parameters")]
        public unsafe static IntPtr CreateExchangeParameters()
        {
            var exchangeParameters = new ExchangeParameters();
            return GCHandle.ToIntPtr(GCHandle.Alloc(exchangeParameters));
        }

        /// <summary>
        /// ExchangeParameters添加值
        /// </summary>
        /// <param name="inst">ExchangeParameters实例的指针</param>
        /// <param name="exchange">交易所</param>
        /// <param name="name">名称</param>
        /// <param name="value">值</param>
        [UnmanagedCallersOnly(EntryPoint = "exchange_parameters_add")]
        public unsafe static void ExchangeParametersAdd(IntPtr inst, sbyte* exchange, sbyte* name, sbyte* value)
        {
            var exchangeParameters = GCHandle.FromIntPtr(inst).Target as ExchangeParameters;
            exchangeParameters?.AddValue(new ExchangeParameter(new string(exchange), new string(name), new string(value)));
        }

        /// <summary>
        /// 获取现货行情客户端
        /// </summary>
        /// <param name="inst">实例</param>
        /// <param name="exchange">交易所</param>
        /// <returns>现货行情客户端</returns>
        [UnmanagedCallersOnly(EntryPoint = "get_spot_ticker_client")]
        public unsafe static IntPtr GetSpotTickerClient(IntPtr inst, sbyte* exchange)
        {
            var exchangeName = new string(exchange);
            var restClient = GCHandle.FromIntPtr(inst).Target as IExchangeRestClient;
            var spotTickerClient = restClient?.GetSpotTickerClient(exchangeName);
            if (spotTickerClient != null)
            {
                return GCHandle.ToIntPtr(GCHandle.Alloc(spotTickerClient));
            }
            else
            {
                return IntPtr.Zero;
            }
        }

        /// <summary>
        /// 获取期货符号客户端
        /// </summary>
        /// <param name="inst">实例</param>
        /// <param name="tradingMode">交易模式</param>
        /// <param name="exchange">交易所</param>
        /// <returns>期货符号客户端</returns>
        [UnmanagedCallersOnly(EntryPoint = "get_futures_symbol_client")]
        public unsafe static IntPtr GetFuturesSymbolClient(IntPtr inst, int tradingMode, sbyte* exchange)
        {
            var exchangeName = new string(exchange);
            var restClient = GCHandle.FromIntPtr(inst).Target as IExchangeRestClient;
            var futuresSymbolClient = restClient?.GetFuturesSymbolClient((TradingMode)tradingMode, exchangeName);
            if (futuresSymbolClient != null)
            {
                return GCHandle.ToIntPtr(GCHandle.Alloc(futuresSymbolClient));
            }
            else
            {
                return IntPtr.Zero;
            }
        }

        /// <summary>
        /// 获取期货行情客户端
        /// </summary>
        /// <param name="inst">实例</param>
        /// <param name="tradingMode">交易模式</param>
        /// <param name="exchange">交易所</param>
        /// <returns>期货行情客户端</returns>
        [UnmanagedCallersOnly(EntryPoint = "get_futures_ticker_client")]
        public unsafe static IntPtr GetFuturesTickerClient(IntPtr inst, int tradingMode, sbyte* exchange)
        {
            var exchangeName = new string(exchange);
            var restClient = GCHandle.FromIntPtr(inst).Target as IExchangeRestClient;
            var futuresTickerClient = restClient?.GetFuturesTickerClient((TradingMode)tradingMode, exchangeName);
            if (futuresTickerClient != null)
            {
                return GCHandle.ToIntPtr(GCHandle.Alloc(futuresTickerClient));
            }
            else
            {
                return IntPtr.Zero;
            }
        }

        /// <summary>
        /// 获取订单簿客户端
        /// </summary>
        /// <param name="inst">实例</param>
        /// <param name="tradingMode">交易模式</param>
        /// <param name="exchange">交易所</param>
        /// <returns>订单簿客户端</returns>
        [UnmanagedCallersOnly(EntryPoint = "get_order_book_client")]
        public unsafe static IntPtr GetOrderBookClient(IntPtr inst, int tradingMode, sbyte* exchange)
        {
            var exchangeName = new string(exchange);
            var restClient = GCHandle.FromIntPtr(inst).Target as IExchangeRestClient;
            var orderBookClient = restClient?.GetOrderBookClient((TradingMode)tradingMode, exchangeName);
            if (orderBookClient != null)
            {
                return GCHandle.ToIntPtr(GCHandle.Alloc(orderBookClient));
            }
            else
            {
                return IntPtr.Zero;
            }
        }

        /// <summary>
        /// 获取K线客户端
        /// </summary>
        /// <param name="inst">实例</param>
        /// <param name="tradingMode">交易模式</param>
        /// <param name="exchange">交易所</param>
        /// <returns>K线客户端</returns>
        [UnmanagedCallersOnly(EntryPoint = "get_kline_client")]
        public unsafe static IntPtr GetKlineClient(IntPtr inst, int tradingMode, sbyte* exchange)
        {
            var exchangeName = new string(exchange);
            var restClient = GCHandle.FromIntPtr(inst).Target as IExchangeRestClient;
            var klineClient = restClient?.GetKlineClient((TradingMode)tradingMode, exchangeName);
            if (klineClient != null)
            {
                return GCHandle.ToIntPtr(GCHandle.Alloc(klineClient));
            }
            return IntPtr.Zero;
        }

        /// <summary>
        /// 获取期货账户客户端
        /// </summary>
        /// <param name="inst">实例</param>
        /// <param name="tradingMode">交易模式</param>
        /// <param name="exchange">交易所</param>
        /// <returns>期货账户客户端</returns>
        [UnmanagedCallersOnly(EntryPoint = "get_balances_client")]
        public unsafe static IntPtr GetBalancesClient(IntPtr inst, int tradingMode, sbyte* exchange)
        {
            var exchangeName = new string(exchange);
            var restClient = GCHandle.FromIntPtr(inst).Target as IExchangeRestClient;
            var balancesClient = restClient?.GetBalancesClient((TradingMode)tradingMode, exchangeName);
            if (balancesClient != null)
            {
                return GCHandle.ToIntPtr(GCHandle.Alloc(balancesClient));
            }
            else
            {
                return IntPtr.Zero;
            }
        }

        /// <summary>
        /// 获取期货订单客户端
        /// </summary>
        /// <param name="inst">实例</param>
        /// <param name="tradingMode">交易模式</param>
        /// <param name="exchange">交易所</param>
        /// <returns>期货订单客户端</returns>
        [UnmanagedCallersOnly(EntryPoint = "get_futures_order_client")]
        public unsafe static IntPtr GetFuturesOrderClient(IntPtr inst, int tradingMode, sbyte* exchange)
        {
            var exchangeName = new string(exchange);
            var restClient = GCHandle.FromIntPtr(inst).Target as IExchangeRestClient;
            var futuresOrderClient = restClient?.GetFuturesOrderClient((TradingMode)tradingMode, exchangeName);
            if (futuresOrderClient != null)
            {
                return GCHandle.ToIntPtr(GCHandle.Alloc(futuresOrderClient));
            }
            else
            {
                return IntPtr.Zero;
            }
        }

        /// <summary>
        /// 获取杠杆客户端
        /// </summary>
        /// <param name="inst">实例</param>
        /// <param name="tradingMode">交易模式</param>
        /// <param name="exchange">交易所</param>
        /// <returns>杠杆客户端</returns>
        [UnmanagedCallersOnly(EntryPoint = "get_leverage_client")]
        public unsafe static IntPtr GetLeverageClient(IntPtr inst, int tradingMode, sbyte* exchange)
        {
            var exchangeName = new string(exchange);
            var restClient = GCHandle.FromIntPtr(inst).Target as IExchangeRestClient;
            var leverageClient = restClient?.GetLeverageClient((TradingMode)tradingMode, exchangeName);
            if (leverageClient != null)
            {
                return GCHandle.ToIntPtr(GCHandle.Alloc(leverageClient));
            }
            else
            {
                return IntPtr.Zero;
            }
        }

        /// <summary>
        /// 获取持仓模式客户端
        /// </summary>
        /// <param name="inst">实例</param>
        /// <param name="tradingMode">交易模式</param>
        /// <param name="exchange">交易所</param>
        /// <returns>持仓模式客户端</returns>
        [UnmanagedCallersOnly(EntryPoint = "get_position_mode_client")]
        public unsafe static IntPtr GetPositionModeClient(IntPtr inst, int tradingMode, sbyte* exchange)
        {
            var exchangeName = new string(exchange);
            var restClient = GCHandle.FromIntPtr(inst).Target as IExchangeRestClient;
            var positionModeClient = restClient?.GetPositionModeClient((TradingMode)tradingMode, exchangeName);
            if (positionModeClient != null)
            {
                return GCHandle.ToIntPtr(GCHandle.Alloc(positionModeClient));
            }
            else
            {
                return IntPtr.Zero;
            }
        }

        /// <summary>
        /// 获取现货行情
        /// </summary>
        /// <param name="inst">实例</param>
        /// <param name="symbol">符号</param>
        /// <param name="result">结果</param>
        /// <returns>0: 成功, 1: 失败, 2: 错误</returns>
        [UnmanagedCallersOnly(EntryPoint = "get_spot_ticker")]
        [DNNE.C99DeclCode(@"
struct ExExchangeWebResult{
    int8_t Success;
    int32_t ErrorCode;
    char ErrorMessage[4096];
    char Exchange[64];
    void* Data;
};")]
        public unsafe static int GetSpotTicker(IntPtr inst, IntPtr symbol, [DNNE.C99Type("struct ExExchangeWebResult*")] ExExchangeWebResult* result)
        {
            var sharedSymbol = GCHandle.FromIntPtr(symbol).Target as SharedSymbol;
            var spotTickerClient = GCHandle.FromIntPtr(inst).Target as ISpotTickerRestClient;
            try
            {
                var ticker = spotTickerClient?.GetSpotTickerAsync(new GetTickerRequest(sharedSymbol!, exchangeParameters: null)).GetAwaiter().GetResult();
                // Console.WriteLine($"Success: {ticker.Success}, Error: {ticker.Error}");
                if (ticker == null)
                {
                    return 1;
                }
                if (!ticker.Success)
                {
                    SetExchangeWebResult(result, false, ticker.Error?.Code ?? 0, ticker.Error?.Message ?? string.Empty, ticker.Exchange);
                    return 2;
                }

                // Console.WriteLine($"Symbol: {ticker.Data?.Symbol}, Last Price: {ticker.Data?.LastPrice}, High Price: {ticker.Data?.HighPrice}, Low Price: {ticker.Data?.LowPrice}, Volume: {ticker.Data?.Volume}, Change Percentage: {ticker.Data?.ChangePercentage}");

                SetExchangeWebResult(result, true, 0, string.Empty, ticker.Exchange);

                if (ticker.Data != null)
                {
                    // ExSharedSpotTicker retrievedData = Marshal.PtrToStructure<ExSharedSpotTicker>(result->Data);
                    ExSharedSpotTicker* data = (ExSharedSpotTicker*)result->Data;
                    // Console.WriteLine($"retrievedData: {FixedStringToString(data->Symbol, NativeUtils.MAX_SYMBOL_LENGTH)}, {data->LastPrice}, {data->HighPrice}, {data->LowPrice}, {data->Volume}, {data->ChangePercentage}");
                    SetFixedByteArray(data->Symbol, NativeUtils.MAX_SYMBOL_LENGTH, ticker.Data.Symbol);
                    data->LastPrice = (double)ticker.Data.LastPrice!;
                    data->HighPrice = (double)ticker.Data.HighPrice!;
                    data->LowPrice = (double)ticker.Data.LowPrice!;
                    data->Volume = (double)ticker.Data.Volume;
                    data->ChangePercentage = (double)ticker.Data.ChangePercentage!;
                    // Console.WriteLine($"data Last Price: {result->Data.LastPrice}, High Price: {result->Data.HighPrice}, Low Price: {result->Data.LowPrice}, Volume: {result->Data.Volume}, Change Percentage: {result->Data.ChangePercentage}");
                    // Marshal.StructureToPtr(retrievedData, result->Data, false);
                }
                return 0;
            }
            catch (Exception ex)
            {
                SetExchangeWebResult(result, false, 3, ex.Message, string.Empty);
                return 3;
            }
        }

        /// <summary>
        /// 获取现货行情
        /// </summary>
        /// <param name="inst">实例</param>
        /// <param name="tradeMode">交易模式</param>
        /// <param name="result">结果</param>
        /// <returns>0: 成功, 1: 失败, 2: 错误</returns>
        [UnmanagedCallersOnly(EntryPoint = "get_spot_tickers")]
        public unsafe static int GetSpotTickers(IntPtr inst, int tradeMode, [DNNE.C99Type("struct ExExchangeWebResult*")] ExExchangeWebResult* result)
        {
            var spotTickerClient = GCHandle.FromIntPtr(inst).Target as ISpotTickerRestClient;
            try
            {
                var tickers = spotTickerClient?.GetSpotTickersAsync(new GetTickersRequest((TradingMode)tradeMode, exchangeParameters: null)).GetAwaiter().GetResult();
                if (tickers == null)
                {
                    SetExchangeWebResult(result, false, 1, "GetSpotTickersAsync failed", string.Empty);
                    return 1;
                }
                if (!tickers.Success)
                {
                    SetExchangeWebResult(result, false, tickers.Error?.Code ?? 0, tickers.Error?.Message ?? string.Empty, tickers.Exchange);
                    return 2;
                }

                SetExchangeWebResult(result, true, 0, string.Empty, tickers.Exchange);

                if (tickers.Data != null)
                {
                    ExSharedSpotTickers* data = (ExSharedSpotTickers*)result->Data;

                    foreach (var ticker in tickers.Data)
                    {
                        byte[] symbolBytes = Encoding.UTF8.GetBytes(ticker.Symbol);
                        // Array.Resize(ref symbolBytes, symbolBytes.Length + 1); // 扩展空间
                        // symbolBytes[symbolBytes.Length - 1] = 0; // 添加终止符
                        fixed (byte* symbolBytesPtr = symbolBytes)
                        {
                            data->OnTickerAdded((IntPtr)data, (sbyte*)symbolBytesPtr, symbolBytes.Length, (double)ticker.LastPrice!, (double)ticker.HighPrice!, (double)ticker.LowPrice!, (double)ticker.Volume!, (double)ticker.ChangePercentage!);
                        }

                        data->Count++;
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                SetExchangeWebResult(result, false, 3, ex.Message, string.Empty);
                return 3;
            }
        }

        /// <summary>
        /// 获取期货符号
        /// </summary>
        /// <param name="inst">实例</param>
        /// <param name="tradeMode">交易模式</param>
        /// <param name="result">结果</param>
        /// <returns>0: 成功, 1: 失败, 2: 错误</returns>
        [UnmanagedCallersOnly(EntryPoint = "get_futures_symbols")]
        public unsafe static int GetFuturesSymbols(IntPtr inst, int tradeMode, [DNNE.C99Type("struct ExExchangeWebResult*")] ExExchangeWebResult* result)
        {
            var futuresSymbolClient = GCHandle.FromIntPtr(inst).Target as IFuturesSymbolRestClient;
            try
            {
                var symbols = futuresSymbolClient?.GetFuturesSymbolsAsync(new GetSymbolsRequest((TradingMode)tradeMode, exchangeParameters: null)).GetAwaiter().GetResult();
                if (symbols == null)
                {
                    SetExchangeWebResult(result, false, 1, "GetFuturesSymbolsAsync failed", string.Empty);
                    return 1;
                }
                if (!symbols.Success)
                {
                    SetExchangeWebResult(result, false, symbols.Error?.Code ?? 0, symbols.Error?.Message ?? string.Empty, symbols.Exchange);
                    return 2;
                }

                SetExchangeWebResult(result, true, 0, string.Empty, symbols.Exchange);

                ExSharedFuturesSymbols* data = (ExSharedFuturesSymbols*)result->Data;

                data->Count = 0;
                foreach (var symbol in symbols.Data)
                {
                    var baseAssetBytes = Encoding.UTF8.GetBytes(symbol.BaseAsset);
                    var quoteAssetBytes = Encoding.UTF8.GetBytes(symbol.QuoteAsset);
                    var nameBytes = Encoding.UTF8.GetBytes(symbol.Name);
                    var deliveryTimeBytes = Encoding.UTF8.GetBytes((symbol.DeliveryTime ?? DateTime.MinValue).ToString(NativeUtils.DATE_TIME_FORMAT));

                    fixed (byte* baseAssetBytesPtr = baseAssetBytes)
                    fixed (byte* quoteAssetBytesPtr = quoteAssetBytes)
                    fixed (byte* nameBytesPtr = nameBytes)
                    fixed (byte* deliveryTimeBytesPtr = deliveryTimeBytes)
                    {
                        data->OnFuturesSymbolAdd((IntPtr)data,
                            (sbyte*)baseAssetBytesPtr,
                            baseAssetBytes.Length,
                            (sbyte*)quoteAssetBytesPtr,
                            quoteAssetBytes.Length,
                            (sbyte*)nameBytesPtr,
                            nameBytes.Length,
                            (double)(symbol.MinTradeQuantity ?? 0m),
                            (double)(symbol.MinNotionalValue ?? 0m),
                            (double)(symbol.MaxTradeQuantity ?? 0m),
                            (double)(symbol.QuantityStep ?? 0m),
                            (double)(symbol.PriceStep ?? 0m),
                            symbol.QuantityDecimals ?? 0,
                            symbol.PriceDecimals ?? 0,
                            symbol.PriceSignificantFigures ?? 0,
                            symbol.Trading ? (byte)1 : (byte)0,
                            (byte)symbol.SymbolType,
                            (double)(symbol.ContractSize ?? 0),
                            (sbyte*)deliveryTimeBytesPtr,
                            deliveryTimeBytes.Length
                        );
                    }
                    data->Count++;
                }
                return 0;
            }
            catch (Exception ex)
            {
                SetExchangeWebResult(result, false, 3, ex.Message, string.Empty);
                return 3;
            }
        }

        /// <summary>
        /// 获取期货行情
        /// </summary>
        /// <param name="inst">实例</param>
        /// <param name="tradeMode">交易模式</param>
        /// <param name="symbol">符号</param>
        /// <param name="result">结果</param>
        /// <returns>0: 成功, 1: 失败, 2: 错误</returns>
        [UnmanagedCallersOnly(EntryPoint = "get_futures_ticker")]
        public unsafe static int GetFuturesTicker(IntPtr inst, int tradeMode, IntPtr symbol, [DNNE.C99Type("struct ExExchangeWebResult*")] ExExchangeWebResult* result)
        {
            var futuresTickerClient = GCHandle.FromIntPtr(inst).Target as IFuturesTickerRestClient;
            try
            {
                var sharedSymbol = GCHandle.FromIntPtr(symbol).Target as SharedSymbol;
                var ticker = futuresTickerClient?.GetFuturesTickerAsync(new GetTickerRequest(sharedSymbol!, exchangeParameters: null)).GetAwaiter().GetResult();
                if (ticker == null)
                {
                    SetExchangeWebResult(result, false, 1, "GetFuturesTickerAsync failed", string.Empty);
                    return 1;
                }
                if (!ticker.Success)
                {
                    SetExchangeWebResult(result, false, ticker.Error?.Code ?? 0, ticker.Error?.Message ?? string.Empty, ticker.Exchange);
                    return 2;
                }

                SetExchangeWebResult(result, true, 0, string.Empty, ticker.Exchange);

                if (ticker.Data != null)
                {
                    ExSharedFuturesTicker* data = (ExSharedFuturesTicker*)result->Data;
                    SetFixedByteArray(data->Symbol, NativeUtils.MAX_SYMBOL_LENGTH, ticker.Data.Symbol);
                    data->LastPrice = (double)ticker.Data.LastPrice!;
                    data->HighPrice = (double)ticker.Data.HighPrice!;
                    data->LowPrice = (double)ticker.Data.LowPrice!;
                    data->Volume = (double)ticker.Data.Volume!;
                    data->ChangePercentage = (double)ticker.Data.ChangePercentage!;
                    data->MarkPrice = (double)ticker.Data.MarkPrice!;
                    data->IndexPrice = (double)ticker.Data.IndexPrice!;
                    data->FundingRate = (double)ticker.Data.FundingRate!;
                    SetFixedByteArray(data->NextFundingTime, NativeUtils.MAX_DATE_TIME_LENGTH, ticker.Data.NextFundingTime?.ToString(NativeUtils.DATE_TIME_FORMAT) ?? string.Empty);
                }

                return 0;
            }
            catch (Exception ex)
            {
                SetExchangeWebResult(result, false, 3, ex.Message, string.Empty);
                return 3;
            }
        }

        /// <summary>
        /// 获取订单簿
        /// </summary>
        /// <param name="inst">实例</param>
        /// <param name="symbol">符号</param>
        /// <param name="limit">深度</param>
        /// <param name="result">结果</param>
        /// <returns>0: 成功, 1: 失败, 2: 错误</returns>
        [UnmanagedCallersOnly(EntryPoint = "get_order_book")]
        public unsafe static int GetOrderBook(IntPtr inst, IntPtr symbol, int limit, [DNNE.C99Type("struct ExExchangeWebResult*")] ExExchangeWebResult* result)
        {
            var orderBookClient = GCHandle.FromIntPtr(inst).Target as IOrderBookRestClient;
            try
            {
                var sharedSymbol = GCHandle.FromIntPtr(symbol).Target as SharedSymbol;
                var orderBook = orderBookClient?.GetOrderBookAsync(new GetOrderBookRequest(sharedSymbol!, limit, exchangeParameters: null)).GetAwaiter().GetResult();
                if (orderBook == null)
                {
                    SetExchangeWebResult(result, false, 1, "GetOrderBookAsync failed", string.Empty);
                    return 1;
                }
                if (!orderBook.Success)
                {
                    SetExchangeWebResult(result, false, orderBook.Error?.Code ?? 0, orderBook.Error?.Message ?? string.Empty, orderBook.Exchange);
                    return 2;
                }

                SetExchangeWebResult(result, true, 0, string.Empty, orderBook.Exchange);

                if (orderBook.Data != null)
                {
                    ExSharedOrderBook* data = (ExSharedOrderBook*)result->Data;
                    data->AsksCount = 0;
                    data->BidsCount = 0;
                    foreach (var ask in orderBook.Data.Asks)
                    {
                        data->OnOrderBookAskAdded((IntPtr)data, (double)ask.Quantity, (double)ask.Price);
                        data->AsksCount++;
                    }
                    foreach (var bid in orderBook.Data.Bids)
                    {
                        data->OnOrderBookBidAdded((IntPtr)data, (double)bid.Quantity, (double)bid.Price);
                        data->BidsCount++;
                    }
                }

                return 0;
            }
            catch (Exception ex)
            {
                SetExchangeWebResult(result, false, 3, ex.Message, string.Empty);
                return 3;
            }
        }

        /// <summary>
        /// 获取K线
        /// </summary>
        /// <param name="inst">实例</param>
        /// <param name="symbol">符号</param>
        /// <param name="interval">K线间隔</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="limit">限制</param>
        /// <param name="result">结果</param>
        /// <returns>0: 成功, 1: 失败, 2: 错误</returns>
        [UnmanagedCallersOnly(EntryPoint = "get_klines")]
        public unsafe static int GetKlines(IntPtr inst, IntPtr symbol, int interval, sbyte* startTime, sbyte* endTime, int limit, [DNNE.C99Type("struct ExExchangeWebResult*")] ExExchangeWebResult* result)
        {
            var klineClient = GCHandle.FromIntPtr(inst).Target as IKlineRestClient;
            try
            {
                var sharedSymbol = GCHandle.FromIntPtr(symbol).Target as SharedSymbol;
                var sharedInterval = (SharedKlineInterval)interval;
                var startTimeString = new string(startTime);
                var endTimeString = new string(endTime);
                DateTime? startTimeDateTime = null;
                DateTime? endTimeDateTime = null;
                if (!string.IsNullOrEmpty(startTimeString))
                {
                    startTimeDateTime = DateTime.ParseExact(startTimeString, NativeUtils.DATE_TIME_FORMAT, CultureInfo.InvariantCulture);
                }
                if (!string.IsNullOrEmpty(endTimeString))
                {
                    endTimeDateTime = DateTime.ParseExact(endTimeString, NativeUtils.DATE_TIME_FORMAT, CultureInfo.InvariantCulture);
                }
                var klines = klineClient?.GetKlinesAsync(new GetKlinesRequest(sharedSymbol!, sharedInterval, startTimeDateTime, endTimeDateTime, limit, exchangeParameters: null)).GetAwaiter().GetResult();
                if (klines == null)
                {
                    SetExchangeWebResult(result, false, 1, "GetKlinesAsync failed", string.Empty);
                    return 1;
                }
                if (!klines.Success)
                {
                    SetExchangeWebResult(result, false, klines.Error?.Code ?? 0, klines.Error?.Message ?? string.Empty, klines.Exchange);
                    return 2;
                }

                SetExchangeWebResult(result, true, 0, string.Empty, klines.Exchange);

                if (klines.Data != null)
                {
                    ExSharedKlines* data = (ExSharedKlines*)result->Data;
                    data->Count = 0;
                    foreach (var kline in klines.Data)
                    {
                        var openTimeBytes = Encoding.UTF8.GetBytes(kline.OpenTime.ToString(NativeUtils.DATE_TIME_FORMAT));
                        fixed (byte* openTimeBytesPtr = openTimeBytes)
                        {
                            data->OnKlineAdded((IntPtr)data,
                                (sbyte*)openTimeBytesPtr,
                                openTimeBytes.Length,
                                NativeUtils.DateTimeToUnixTimestampMs(kline.OpenTime),
                                (double)kline.ClosePrice,
                                (double)kline.HighPrice,
                                (double)kline.LowPrice,
                                (double)kline.OpenPrice,
                                (double)kline.Volume
                            );
                        }
                        data->Count++;
                    }
                }

                return 0;
            }
            catch (Exception ex)
            {
                SetExchangeWebResult(result, false, 3, ex.Message, string.Empty);
                return 3;
            }
        }

        /// <summary>
        /// 获取期货行情
        /// </summary>
        /// <param name="inst">实例</param>
        /// <param name="tradeMode">交易模式</param>
        /// <param name="result">结果</param>
        /// <returns>0: 成功, 1: 失败, 2: 错误</returns>
        [UnmanagedCallersOnly(EntryPoint = "get_futures_tickers")]
        public unsafe static int GetFuturesTickers(IntPtr inst, int tradeMode, [DNNE.C99Type("struct ExExchangeWebResult*")] ExExchangeWebResult* result)
        {
            var futuresTickerClient = GCHandle.FromIntPtr(inst).Target as IFuturesTickerRestClient;
            try
            {
                var tickers = futuresTickerClient?.GetFuturesTickersAsync(new GetTickersRequest((TradingMode)tradeMode, exchangeParameters: null)).GetAwaiter().GetResult();
                if (tickers == null)
                {
                    SetExchangeWebResult(result, false, 1, "GetFuturesTickersAsync failed", string.Empty);
                    return 1;
                }
                if (!tickers.Success)
                {
                    SetExchangeWebResult(result, false, tickers.Error?.Code ?? 0, tickers.Error?.Message ?? string.Empty, tickers.Exchange);
                    return 2;
                }

                SetExchangeWebResult(result, true, 0, string.Empty, tickers.Exchange);

                if (tickers.Data != null)
                {
                    ExSharedFuturesTickers* data = (ExSharedFuturesTickers*)result->Data;

                    foreach (var ticker in tickers.Data)
                    {
                        byte[] symbolBytes = Encoding.UTF8.GetBytes(ticker.Symbol);
                        byte[] nextFundingTimeBytes = Encoding.UTF8.GetBytes(ticker.NextFundingTime?.ToString(NativeUtils.DATE_TIME_FORMAT) ?? string.Empty);
                        fixed (byte* symbolBytesPtr = symbolBytes)
                        fixed (byte* nextFundingTimeBytesPtr = nextFundingTimeBytes)
                        {
                            data->OnFuturesTickerAdd((IntPtr)data,
                                (sbyte*)symbolBytesPtr,
                                symbolBytes.Length,
                                (double)ticker.LastPrice!,
                                (double)ticker.HighPrice!,
                                (double)ticker.LowPrice!,
                                (double)ticker.Volume!,
                                (double)ticker.ChangePercentage!,
                                (double)ticker.MarkPrice!,
                                (double)ticker.IndexPrice!,
                                (double)ticker.FundingRate!,
                                (sbyte*)nextFundingTimeBytesPtr,
                                nextFundingTimeBytes.Length
                            );
                        }

                        data->Count++;
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                SetExchangeWebResult(result, false, 3, ex.Message, string.Empty);
                return 3;
            }
        }

        /// <summary>
        /// 获取余额
        /// </summary>
        /// <param name="inst">实例</param>
        /// <param name="tradeMode">交易模式</param>
        /// <param name="result">结果</param>
        /// <returns>0: 成功, 1: 失败, 2: 错误</returns>
        [UnmanagedCallersOnly(EntryPoint = "get_balances")]
        public unsafe static int GetBalances(IntPtr inst, int tradeMode, [DNNE.C99Type("struct ExExchangeWebResult*")] ExExchangeWebResult* result)
        {
            var balancesClient = GCHandle.FromIntPtr(inst).Target as IBalanceRestClient;
            try
            {
                var balances = balancesClient?.GetBalancesAsync(new GetBalancesRequest((TradingMode)tradeMode, exchangeParameters: null)).GetAwaiter().GetResult();
                if (balances == null)
                {
                    SetExchangeWebResult(result, false, 1, "GetBalancesAsync failed", string.Empty);
                    return 1;
                }
                if (!balances.Success)
                {
                    SetExchangeWebResult(result, false, balances.Error?.Code ?? 0, balances.Error?.Message ?? string.Empty, balances.Exchange);
                    return 2;
                }

                SetExchangeWebResult(result, true, 0, string.Empty, balances.Exchange);

                if (balances.Data != null)
                {
                    ExSharedBalances* data = (ExSharedBalances*)result->Data;

                    foreach (var balance in balances.Data)
                    {
                        var assetBytes = Encoding.UTF8.GetBytes(balance.Asset);
                        var isolatedMarginSymbolBytes = Encoding.UTF8.GetBytes(balance.IsolatedMarginSymbol ?? string.Empty);
                        fixed (byte* assetBytesPtr = assetBytes)
                        fixed (byte* isolatedMarginSymbolBytesPtr = isolatedMarginSymbolBytes)
                        {
                            data->OnBalanceAdded((IntPtr)data,
                                (sbyte*)assetBytesPtr,
                                assetBytes.Length,
                                (double)balance.Available,
                                (double)balance.Total,
                                (sbyte*)isolatedMarginSymbolBytesPtr,
                                isolatedMarginSymbolBytes.Length);
                        }
                    }
                }

                return 0;
            }
            catch (Exception ex)
            {
                SetExchangeWebResult(result, false, 3, ex.Message, string.Empty);
                return 3;
            }
        }

        /// <summary>
        /// 获取期货订单
        /// </summary>
        /// <param name="inst">实例</param>
        /// <param name="tradeMode">交易模式</param>
        /// <param name="symbol">符号</param>
        /// <param name="result">结果</param>
        /// <returns>0: 成功, 1: 失败, 2: 错误</returns>
        [UnmanagedCallersOnly(EntryPoint = "get_open_futures_orders")]
        public unsafe static int GetOpenFuturesOrders(IntPtr inst, int tradeMode, IntPtr symbol, [DNNE.C99Type("struct ExExchangeWebResult*")] ExExchangeWebResult* result)
        {
            var futuresOrderClient = GCHandle.FromIntPtr(inst).Target as IFuturesOrderRestClient;
            try
            {
                GetOpenOrdersRequest request;
                if (symbol != IntPtr.Zero)
                {
                    var sharedSymbol = GCHandle.FromIntPtr(symbol).Target as SharedSymbol;
                    request = new GetOpenOrdersRequest(sharedSymbol!, exchangeParameters: null);
                }
                else
                {
                    request = new GetOpenOrdersRequest((TradingMode)tradeMode, exchangeParameters: null);
                }
                var orders = futuresOrderClient?.GetOpenFuturesOrdersAsync(request).GetAwaiter().GetResult();
                if (orders == null)
                {
                    SetExchangeWebResult(result, false, 1, "GetOpenFuturesOrdersAsync failed", string.Empty);
                    return 1;
                }
                if (!orders.Success)
                {
                    SetExchangeWebResult(result, false, orders.Error?.Code ?? 0, orders.Error?.Message ?? string.Empty, orders.Exchange);
                    return 2;
                }

                SetExchangeWebResult(result, true, 0, string.Empty, orders.Exchange);

                if (orders.Data != null)
                {
                    ExSharedFuturesOrders* data = (ExSharedFuturesOrders*)result->Data;
                    data->Count = 0;
                    foreach (var order in orders.Data)
                    {
                        var symbolBytes = Encoding.UTF8.GetBytes(order.Symbol);
                        var orderIdBytes = Encoding.UTF8.GetBytes(order.OrderId);
                        var clientOrderIdBytes = Encoding.UTF8.GetBytes(order.ClientOrderId ?? string.Empty);
                        var feeAssetBytes = Encoding.UTF8.GetBytes(order.FeeAsset ?? string.Empty);
                        var createTimeBytes = Encoding.UTF8.GetBytes(order.CreateTime?.ToString(NativeUtils.DATE_TIME_FORMAT) ?? string.Empty);
                        var updateTimeBytes = Encoding.UTF8.GetBytes(order.UpdateTime?.ToString(NativeUtils.DATE_TIME_FORMAT) ?? string.Empty);
                        fixed (byte* symbolBytesPtr = symbolBytes)
                        fixed (byte* orderIdBytesPtr = orderIdBytes)
                        fixed (byte* clientOrderIdBytesPtr = clientOrderIdBytes)
                        fixed (byte* feeAssetBytesPtr = feeAssetBytes)
                        fixed (byte* createTimeBytesPtr = createTimeBytes)
                        fixed (byte* updateTimeBytesPtr = updateTimeBytes)
                        {
                            data->OnFuturesOrderAdded((IntPtr)data,
                                (sbyte*)symbolBytesPtr,
                                symbolBytes.Length,
                                (sbyte*)orderIdBytesPtr,
                                orderIdBytes.Length,
                                (int)order.OrderType,
                                (int)order.Side,
                                (int)order.Status,
                                (int)(order.TimeInForce ?? SharedTimeInForce.GoodTillCanceled),
                                (int)(order.PositionSide ?? SharedPositionSide.Long),
                                order.ReduceOnly ?? false ? (byte)1 : (byte)0,
                                (double)(order.Quantity ?? 0m),
                                (double)(order.QuantityFilled ?? 0m),
                                (double)(order.QuoteQuantity ?? 0m),
                                (double)(order.QuoteQuantityFilled ?? 0m),
                                (double)(order.OrderPrice ?? 0m),
                                (double)(order.AveragePrice ?? 0m),
                                (sbyte*)clientOrderIdBytesPtr,
                                clientOrderIdBytes.Length,
                                (sbyte*)feeAssetBytesPtr,
                                feeAssetBytes.Length,
                                (double)(order.Fee ?? 0m),
                                (double)(order.Leverage ?? 0m),
                                (sbyte*)createTimeBytesPtr,
                                createTimeBytes.Length,
                                NativeUtils.DateTimeToUnixTimestampMs(order.CreateTime ?? DateTime.MinValue),
                                (sbyte*)updateTimeBytesPtr,
                                updateTimeBytes.Length,
                                NativeUtils.DateTimeToUnixTimestampMs(order.UpdateTime ?? DateTime.MinValue)
                            );
                        }
                        data->Count++;
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                SetExchangeWebResult(result, false, 3, ex.Message, string.Empty);
                return 3;
            }
        }

        /// <summary>
        /// 获取期货订单
        /// </summary>
        /// <param name="inst">实例</param>
        /// <param name="symbol">符号</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        [UnmanagedCallersOnly(EntryPoint = "get_closed_futures_orders")]
        public unsafe static int GetClosedFuturesOrders(IntPtr inst, IntPtr symbol, sbyte* startTime, sbyte* endTime, int limit, [DNNE.C99Type("struct ExExchangeWebResult*")] ExExchangeWebResult* result)
        {
            var futuresOrderClient = GCHandle.FromIntPtr(inst).Target as IFuturesOrderRestClient;
            try
            {
                var sharedSymbol = GCHandle.FromIntPtr(symbol).Target as SharedSymbol;
                var startTimeString = new string(startTime);
                var endTimeString = new string(endTime);
                DateTime? startTimeDateTime = null;
                DateTime? endTimeDateTime = null;
                if (!string.IsNullOrEmpty(startTimeString))
                {
                    if (DateTime.TryParseExact(startTimeString, NativeUtils.DATE_TIME_FORMAT, CultureInfo.InvariantCulture, DateTimeStyles.None, out var _startTime))
                    {
                        startTimeDateTime = _startTime;
                    }
                }
                if (!string.IsNullOrEmpty(endTimeString))
                {
                    if (DateTime.TryParseExact(endTimeString, NativeUtils.DATE_TIME_FORMAT, CultureInfo.InvariantCulture, DateTimeStyles.None, out var _endTime))
                    {
                        endTimeDateTime = _endTime;
                    }
                }
                int? limitInt = limit > 0 ? limit : null;
                // Console.WriteLine($"startTimeDateTime: {startTimeDateTime}, endTimeDateTime: {endTimeDateTime}, limitInt: {limitInt}");
                var request = new GetClosedOrdersRequest(sharedSymbol!, startTime: startTimeDateTime, endTime: endTimeDateTime, limit: limitInt, exchangeParameters: null);
                var orders = futuresOrderClient?.GetClosedFuturesOrdersAsync(request).GetAwaiter().GetResult();
                if (orders == null)
                {
                    SetExchangeWebResult(result, false, 1, "GetClosedFuturesOrdersAsync failed", string.Empty);
                    return 1;
                }
                if (!orders.Success)
                {
                    SetExchangeWebResult(result, false, orders.Error?.Code ?? 0, orders.Error?.Message ?? string.Empty, orders.Exchange);
                    return 2;
                }

                SetExchangeWebResult(result, true, 0, string.Empty, orders.Exchange);

                if (orders.Data != null)
                {
                    ExSharedFuturesOrders* data = (ExSharedFuturesOrders*)result->Data;
                    data->Count = 0;
                    foreach (var order in orders.Data)
                    {
                        var symbolBytes = Encoding.UTF8.GetBytes(order.Symbol);
                        var orderIdBytes = Encoding.UTF8.GetBytes(order.OrderId);
                        var clientOrderIdBytes = Encoding.UTF8.GetBytes(order.ClientOrderId ?? string.Empty);
                        var feeAssetBytes = Encoding.UTF8.GetBytes(order.FeeAsset ?? string.Empty);
                        var createTimeBytes = Encoding.UTF8.GetBytes(order.CreateTime?.ToString(NativeUtils.DATE_TIME_FORMAT) ?? string.Empty);
                        var updateTimeBytes = Encoding.UTF8.GetBytes(order.UpdateTime?.ToString(NativeUtils.DATE_TIME_FORMAT) ?? string.Empty);
                        fixed (byte* symbolBytesPtr = symbolBytes)
                        fixed (byte* orderIdBytesPtr = orderIdBytes)
                        fixed (byte* clientOrderIdBytesPtr = clientOrderIdBytes)
                        fixed (byte* feeAssetBytesPtr = feeAssetBytes)
                        fixed (byte* createTimeBytesPtr = createTimeBytes)
                        fixed (byte* updateTimeBytesPtr = updateTimeBytes)
                        {
                            data->OnFuturesOrderAdded((IntPtr)data,
                                (sbyte*)symbolBytesPtr,
                                symbolBytes.Length,
                                (sbyte*)orderIdBytesPtr,
                                orderIdBytes.Length,
                                (int)order.OrderType,
                                (int)order.Side,
                                (int)order.Status,
                                (int)(order.TimeInForce ?? SharedTimeInForce.GoodTillCanceled),
                                (int)(order.PositionSide ?? SharedPositionSide.Long),
                                order.ReduceOnly ?? false ? (byte)1 : (byte)0,
                                (double)(order.Quantity ?? 0m),
                                (double)(order.QuantityFilled ?? 0m),
                                (double)(order.QuoteQuantity ?? 0m),
                                (double)(order.QuoteQuantityFilled ?? 0m),
                                (double)(order.OrderPrice ?? 0m),
                                (double)(order.AveragePrice ?? 0m),
                                (sbyte*)clientOrderIdBytesPtr,
                                clientOrderIdBytes.Length,
                                (sbyte*)feeAssetBytesPtr,
                                feeAssetBytes.Length,
                                (double)(order.Fee ?? 0m),
                                (double)(order.Leverage ?? 0m),
                                (sbyte*)createTimeBytesPtr,
                                createTimeBytes.Length,
                                NativeUtils.DateTimeToUnixTimestampMs(order.CreateTime ?? DateTime.MinValue),
                                (sbyte*)updateTimeBytesPtr,
                                updateTimeBytes.Length,
                                NativeUtils.DateTimeToUnixTimestampMs(order.UpdateTime ?? DateTime.MinValue)
                            );
                        }
                        data->Count++;
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                SetExchangeWebResult(result, false, 3, ex.Message, string.Empty);
                return 3;
            }
        }

        /// <summary>
        /// 获取期货订单
        /// </summary>
        /// <param name="inst">实例</param>
        /// <param name="symbol">符号</param>
        /// <param name="orderId">订单ID</param>
        /// <param name="result">结果</param>
        /// <returns>0: 成功, 1: 失败, 2: 错误</returns>
        [UnmanagedCallersOnly(EntryPoint = "get_futures_order")]
        public unsafe static int GetFuturesOrder(IntPtr inst, IntPtr symbol, sbyte* orderId, [DNNE.C99Type("struct ExExchangeWebResult*")] ExExchangeWebResult* result)
        {
            var futuresOrderClient = GCHandle.FromIntPtr(inst).Target as IFuturesOrderRestClient;
            try
            {
                var sharedSymbol = GCHandle.FromIntPtr(symbol).Target as SharedSymbol;
                var orderIdString = new string(orderId);
                var order = futuresOrderClient?.GetFuturesOrderAsync(new GetOrderRequest(sharedSymbol!, orderIdString, exchangeParameters: null)).GetAwaiter().GetResult();
                if (order == null)
                {
                    SetExchangeWebResult(result, false, 1, "GetFuturesOrderAsync failed", string.Empty);
                    return 1;
                }
                if (!order.Success)
                {
                    SetExchangeWebResult(result, false, order.Error?.Code ?? 0, order.Error?.Message ?? string.Empty, order.Exchange);
                    return 2;
                }

                SetExchangeWebResult(result, true, 0, string.Empty, order.Exchange);

                if (order.Data != null)
                {
                    ExSharedFuturesOrder* data = (ExSharedFuturesOrder*)result->Data;

                    SetFixedByteArray(data->Symbol, NativeUtils.MAX_SYMBOL_LENGTH, order.Data.Symbol);
                    SetFixedByteArray(data->OrderId, NativeUtils.MAX_ORDER_ID_LENGTH, order.Data.OrderId);
                    data->OrderType = (int)order.Data.OrderType;
                    data->Side = (int)order.Data.Side;
                    data->Status = (int)order.Data.Status;
                    data->TimeInForce = (int)(order.Data.TimeInForce ?? SharedTimeInForce.GoodTillCanceled);
                    data->PositionSide = (int)(order.Data.PositionSide ?? SharedPositionSide.Long);
                    data->ReduceOnly = order.Data.ReduceOnly ?? false ? (byte)1 : (byte)0;
                    data->Quantity = (double)(order.Data.Quantity ?? 0m);
                    data->QuantityFilled = (double)(order.Data.QuantityFilled ?? 0m);
                    data->QuoteQuantity = (double)(order.Data.QuoteQuantity ?? 0m);
                    data->QuoteQuantityFilled = (double)(order.Data.QuoteQuantityFilled ?? 0m);
                    data->OrderPrice = (double)(order.Data.OrderPrice ?? 0m);
                    data->AveragePrice = (double)(order.Data.AveragePrice ?? 0m);
                    SetFixedByteArray(data->ClientOrderId, NativeUtils.MAX_ORDER_ID_LENGTH, order.Data.ClientOrderId ?? string.Empty);
                    SetFixedByteArray(data->FeeAsset, NativeUtils.MAX_SYMBOL_LENGTH, order.Data.FeeAsset ?? string.Empty);
                    data->Fee = (double)(order.Data.Fee ?? 0m);
                    data->Leverage = (double)(order.Data.Leverage ?? 0m);
                    SetFixedByteArray(data->CreateTime, NativeUtils.MAX_DATE_TIME_LENGTH, order.Data.CreateTime?.ToString(NativeUtils.DATE_TIME_FORMAT) ?? string.Empty);
                    SetFixedByteArray(data->UpdateTime, NativeUtils.MAX_DATE_TIME_LENGTH, order.Data.UpdateTime?.ToString(NativeUtils.DATE_TIME_FORMAT) ?? string.Empty);
                }

                return 0;
            }
            catch (Exception ex)
            {
                SetExchangeWebResult(result, false, 3, ex.Message, string.Empty);
                return 3;
            }
        }

        /// <summary>
        /// 获取期货持仓
        /// </summary>
        /// <param name="inst">实例</param>
        /// <param name="tradeMode">交易模式</param>
        /// <param name="symbol">符号</param>
        /// <param name="exchangeParameters">交易所参数</param>
        /// <param name="result">结果</param>
        /// <returns>0: 成功, 1: 失败, 2: 错误</returns>
        [UnmanagedCallersOnly(EntryPoint = "get_positions")]
        public unsafe static int GetPositions(IntPtr inst, int tradeMode, IntPtr symbol, IntPtr exchangeParameters, [DNNE.C99Type("struct ExExchangeWebResult*")] ExExchangeWebResult* result)
        {
            var futuresOrderClient = GCHandle.FromIntPtr(inst).Target as IFuturesOrderRestClient;
            try
            {
                GetPositionsRequest request;
                var exchangeParameters_ = exchangeParameters != IntPtr.Zero ? GCHandle.FromIntPtr(exchangeParameters).Target as ExchangeParameters : null;
                if (symbol != IntPtr.Zero)
                {
                    var sharedSymbol = GCHandle.FromIntPtr(symbol).Target as SharedSymbol;
                    request = new GetPositionsRequest(sharedSymbol!, exchangeParameters: exchangeParameters_);
                }
                else
                {
                    request = new GetPositionsRequest((TradingMode)tradeMode, exchangeParameters: exchangeParameters_);
                }
                var positions = futuresOrderClient?.GetPositionsAsync(request).GetAwaiter().GetResult();
                if (positions == null)
                {
                    SetExchangeWebResult(result, false, 1, "GetPositionsAsync failed", string.Empty);
                    return 1;
                }
                if (!positions.Success)
                {
                    SetExchangeWebResult(result, false, positions.Error?.Code ?? 0, positions.Error?.Message ?? string.Empty, positions.Exchange);
                    return 2;
                }
                SetExchangeWebResult(result, true, 0, string.Empty, positions.Exchange);
                if (positions.Data != null)
                {
                    ExSharedPositions* data = (ExSharedPositions*)result->Data;
                    data->Count = 0;
                    foreach (var position in positions.Data)
                    {
                        var symbolBytes = Encoding.UTF8.GetBytes(position.Symbol);
                        var updateTimeBytes = Encoding.UTF8.GetBytes(position.UpdateTime?.ToString(NativeUtils.DATE_TIME_FORMAT) ?? string.Empty);
                        fixed (byte* symbolBytesPtr = symbolBytes)
                        fixed (byte* updateTimeBytesPtr = updateTimeBytes)
                        {
                            data->OnPositionAdded((IntPtr)data,
                                (sbyte*)symbolBytesPtr,
                                symbolBytes.Length,
                                (double)position.PositionSize,
                                (int)position.PositionSide,
                                (double)(position.AverageOpenPrice ?? 0m),
                                (double)(position.UnrealizedPnl ?? 0m),
                                (double)(position.LiquidationPrice ?? 0m),
                                (double)(position.Leverage ?? 0m),
                                (sbyte*)updateTimeBytesPtr,
                                updateTimeBytes.Length);
                        }
                        data->Count++;
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                SetExchangeWebResult(result, false, 3, ex.Message, string.Empty);
                return 3;
            }
        }

        /// <summary>
        /// 获取期货杠杆
        /// </summary>
        /// <param name="inst">实例</param>
        /// <param name="symbol">符号</param>
        /// <param name="positionSide">持仓方向</param>
        /// <param name="marginMode">保证金模式</param>
        /// <param name="result">结果</param>
        /// <returns>0: 成功, 1: 失败, 2: 错误</returns>
        [UnmanagedCallersOnly(EntryPoint = "get_leverage")]
        public unsafe static int GetLeverage(IntPtr inst, IntPtr symbol, int positionSide, int marginMode, [DNNE.C99Type("struct ExExchangeWebResult*")] ExExchangeWebResult* result)
        {
            var leverageClient = GCHandle.FromIntPtr(inst).Target as ILeverageRestClient;
            try
            {
                var sharedSymbol = GCHandle.FromIntPtr(symbol).Target as SharedSymbol;
                var sharedPositionSide = (SharedPositionSide)positionSide;
                SharedMarginMode? sharedMarginMode = marginMode >= 0 ? (SharedMarginMode)marginMode : null;
                var leverage = leverageClient?.GetLeverageAsync(new GetLeverageRequest(sharedSymbol!, sharedPositionSide, sharedMarginMode, exchangeParameters: null)).GetAwaiter().GetResult();
                if (leverage == null)
                {
                    SetExchangeWebResult(result, false, 1, "GetLeverageAsync failed", string.Empty);
                    return 1;
                }
                if (!leverage.Success)
                {
                    SetExchangeWebResult(result, false, leverage.Error?.Code ?? 0, leverage.Error?.Message ?? string.Empty, leverage.Exchange);
                    return 2;
                }
                SetExchangeWebResult(result, true, 0, string.Empty, leverage.Exchange);

                if (leverage.Data != null)
                {
                    ExSharedLeverage* data = (ExSharedLeverage*)result->Data;
                    data->Leverage = (double)leverage.Data.Leverage;
                    data->PositionSide = (int)(leverage.Data.Side ?? SharedPositionSide.Long);
                    data->MarginMode = leverage.Data.MarginMode == null ? -1 : (int)leverage.Data.MarginMode;
                }

                return 0;
            }
            catch (Exception ex)
            {
                SetExchangeWebResult(result, false, 3, ex.Message, string.Empty);
                return 3;
            }
        }

        /// <summary>
        /// 设置期货杠杆
        /// </summary>
        /// <param name="inst">实例</param>
        /// <param name="symbol">符号</param>
        /// <param name="positionSide">持仓方向</param>
        /// <param name="marginMode">保证金模式</param>
        /// <param name="leverage">杠杆</param>
        /// <param name="result">结果</param>
        /// <returns>0: 成功, 1: 失败, 2: 错误</returns>
        [UnmanagedCallersOnly(EntryPoint = "set_leverage")]
        public unsafe static int SetLeverage(IntPtr inst, IntPtr symbol, int positionSide, int marginMode, double leverage, [DNNE.C99Type("struct ExExchangeWebResult*")] ExExchangeWebResult* result)
        {
            var leverageClient = GCHandle.FromIntPtr(inst).Target as ILeverageRestClient;
            try
            {
                var sharedSymbol = GCHandle.FromIntPtr(symbol).Target as SharedSymbol;
                SharedMarginMode? sharedMarginMode = marginMode >= 0 ? (SharedMarginMode)marginMode : null;
                var ret = leverageClient?.SetLeverageAsync(new SetLeverageRequest(sharedSymbol!, (decimal)leverage, (SharedPositionSide)positionSide, sharedMarginMode, exchangeParameters: null)).GetAwaiter().GetResult();
                if (ret == null)
                {
                    SetExchangeWebResult(result, false, 1, "SetLeverageAsync failed", string.Empty);
                    return 1;
                }
                if (!ret.Success)
                {
                    SetExchangeWebResult(result, false, ret.Error?.Code ?? 0, ret.Error?.Message ?? string.Empty, ret.Exchange);
                    return 2;
                }

                SetExchangeWebResult(result, true, 0, string.Empty, ret.Exchange);

                if (ret.Data != null)
                {
                    ExSharedLeverage* data = (ExSharedLeverage*)result->Data;
                    data->Leverage = (double)ret.Data.Leverage;
                    data->PositionSide = (int)(ret.Data.Side ?? SharedPositionSide.Long);
                    data->MarginMode = ret.Data.MarginMode == null ? -1 : (int)ret.Data.MarginMode;
                }

                return 0;
            }
            catch (Exception ex)
            {
                SetExchangeWebResult(result, false, 3, ex.Message, string.Empty);
                return 3;
            }
        }

        /// <summary>
        /// 获取期货持仓模式
        /// </summary>
        /// <param name="inst">实例</param>
        /// <param name="tradeMode">交易模式</param>
        /// <param name="symbol">符号</param>
        /// <param name="exchangeParameters">交易所参数</param>
        /// <param name="result">结果</param>
        /// <returns>0: 成功, 1: 失败, 2: 错误</returns>
        [UnmanagedCallersOnly(EntryPoint = "get_position_mode")]
        public unsafe static int GetPositionMode(IntPtr inst, int tradeMode, IntPtr symbol, IntPtr exchangeParameters, [DNNE.C99Type("struct ExExchangeWebResult*")] ExExchangeWebResult* result)
        {
            var positionModeClient = GCHandle.FromIntPtr(inst).Target as IPositionModeRestClient;
            try
            {
                var exchangeParameters_ = exchangeParameters != IntPtr.Zero ? GCHandle.FromIntPtr(exchangeParameters).Target as ExchangeParameters : null;
                GetPositionModeRequest? request;
                if (symbol != IntPtr.Zero)
                {
                    var sharedSymbol = GCHandle.FromIntPtr(symbol).Target as SharedSymbol;
                    request = new GetPositionModeRequest(sharedSymbol!, exchangeParameters: exchangeParameters_);
                }
                else
                {
                    request = new GetPositionModeRequest((TradingMode)tradeMode, exchangeParameters: exchangeParameters_);
                }
                var positionMode = positionModeClient?.GetPositionModeAsync(request).GetAwaiter().GetResult();
                if (positionMode == null)
                {
                    SetExchangeWebResult(result, false, 1, "GetPositionModeAsync failed", string.Empty);
                    return 1;
                }
                if (!positionMode.Success)
                {
                    SetExchangeWebResult(result, false, positionMode.Error?.Code ?? 0, positionMode.Error?.Message ?? string.Empty, positionMode.Exchange);
                    return 2;
                }

                SetExchangeWebResult(result, true, 0, string.Empty, positionMode.Exchange);

                if (positionMode.Data != null)
                {
                    ExSharedPositionMode* data = (ExSharedPositionMode*)result->Data;
                    data->TradeMode = (int)positionMode.Data.PositionMode;
                }

                return 0;
            }
            catch (Exception ex)
            {
                SetExchangeWebResult(result, false, 3, ex.Message, string.Empty);
                return 3;
            }
        }

        /// <summary>
        /// 设置期货持仓模式
        /// </summary>
        /// <param name="inst">实例</param>
        /// <param name="tradeMode">交易模式</param>
        /// <param name="symbol">符号</param>
        /// <param name="positionMode">持仓模式</param>
        /// <param name="result">结果</param>
        [UnmanagedCallersOnly(EntryPoint = "set_position_mode")]
        public unsafe static int SetPositionMode(IntPtr inst, int tradeMode, IntPtr symbol, int positionMode, [DNNE.C99Type("struct ExExchangeWebResult*")] ExExchangeWebResult* result)
        {
            var positionModeClient = GCHandle.FromIntPtr(inst).Target as IPositionModeRestClient;
            try
            {
                var sharedPositionMode = (SharedPositionMode)positionMode;
                SetPositionModeRequest? request;
                if (symbol != IntPtr.Zero)
                {
                    var sharedSymbol = GCHandle.FromIntPtr(symbol).Target as SharedSymbol;
                    Console.WriteLine($"sharedSymbol: {sharedSymbol}");
                    request = new SetPositionModeRequest(sharedSymbol!, sharedPositionMode, exchangeParameters: null);
                }
                else
                {
                    request = new SetPositionModeRequest(sharedPositionMode, (TradingMode)tradeMode, exchangeParameters: null);
                }
                Console.WriteLine($"request: {request}");
                var ret = positionModeClient?.SetPositionModeAsync(request).GetAwaiter().GetResult();
                Console.WriteLine($"ret: {ret}");
                if (ret == null)
                {
                    SetExchangeWebResult(result, false, 1, "SetPositionModeAsync failed", string.Empty);
                    return 1;
                }
                if (!ret.Success)
                {
                    SetExchangeWebResult(result, false, ret.Error?.Code ?? 0, ret.Error?.Message ?? string.Empty, ret.Exchange);
                    return 2;
                }

                SetExchangeWebResult(result, true, 0, string.Empty, ret.Exchange);

                if (ret.Data != null)
                {
                    ExSharedPositionMode* data = (ExSharedPositionMode*)result->Data;
                    data->TradeMode = (int)ret.Data.PositionMode;
                }

                return 0;
            }
            catch (Exception ex)
            {
                SetExchangeWebResult(result, false, 3, ex.Message, string.Empty);
                return 3;
            }
        }

        /// <summary>
        /// 平仓
        /// </summary>
        /// <param name="inst">实例</param>
        /// <param name="symbol">符号</param>
        /// <param name="positionMode">持仓模式</param>
        /// <param name="positionSide">持仓方向</param>
        /// <param name="marginMode">保证金模式</param>
        /// <param name="quantity">数量</param>
        /// <param name="result">结果</param>
        /// <returns>0: 成功, 1: 失败, 2: 错误</returns>
        [UnmanagedCallersOnly(EntryPoint = "close_position")]
        public unsafe static int ClosePosition(IntPtr inst, IntPtr symbol, int positionMode, int positionSide, int marginMode, double quantity, [DNNE.C99Type("struct ExExchangeWebResult*")] ExExchangeWebResult* result)
        {
            var futuresOrderClient = GCHandle.FromIntPtr(inst).Target as IFuturesOrderRestClient;
            try
            {
                var sharedSymbol = GCHandle.FromIntPtr(symbol).Target as SharedSymbol;
                SharedPositionMode sharedPositionMode = (SharedPositionMode)positionMode;
                SharedPositionSide? sharedPositionSide = positionSide >= 0 ? (SharedPositionSide)positionSide : null;
                SharedMarginMode? sharedMarginMode = marginMode >= 0 ? (SharedMarginMode)marginMode : null;
                decimal? sharedQuantity = quantity > 0 ? (decimal)quantity : null;
                var ret = futuresOrderClient?.ClosePositionAsync(new ClosePositionRequest(sharedSymbol!, sharedPositionMode, sharedPositionSide, sharedMarginMode, sharedQuantity, exchangeParameters: null)).GetAwaiter().GetResult();
                if (ret == null)
                {
                    SetExchangeWebResult(result, false, 1, "ClosePositionAsync failed", string.Empty);
                    return 1;
                }
                if (!ret.Success)
                {
                    SetExchangeWebResult(result, false, ret.Error?.Code ?? 0, ret.Error?.Message ?? string.Empty, ret.Exchange);
                    return 2;
                }

                SetExchangeWebResult(result, true, 0, string.Empty, ret.Exchange);

                if (ret.Data != null)
                {
                    ExSharedId* data = (ExSharedId*)result->Data;
                    SetFixedByteArray(data->Id, NativeUtils.MAX_ORDER_ID_LENGTH, ret.Data.Id);
                }

                return 0;
            }
            catch (Exception ex)
            {
                SetExchangeWebResult(result, false, 3, ex.Message, string.Empty);
                return 3;
            }
        }

        /// <summary>
        /// 下单
        /// </summary>
        /// <param name="inst">实例</param>
        /// <param name="symbol">符号</param>
        /// <param name="side">方向</param>
        /// <param name="type">类型</param>
        /// <param name="quantity">数量</param>
        /// <param name="quoteQuantity">数量(quote)</param>
        /// <param name="price">价格</param>
        /// <param name="reduceOnly">只减仓</param>
        /// <param name="leverage">杠杆</param>
        /// <param name="timeInForce">时间类型</param>
        /// <param name="positionSide">持仓方向</param>
        /// <param name="marginMode">保证金模式</param>
        /// <param name="clientOrderId">客户端订单ID</param>
        /// <param name="exchangeParameters">交易所参数</param>
        /// <param name="result">结果</param>
        /// <returns>0: 成功, 1: 失败, 2: 错误</returns>
        [UnmanagedCallersOnly(EntryPoint = "place_futures_order")]
        public unsafe static int PlaceFuturesOrder(IntPtr inst,
            IntPtr symbol,
            int side,
            int type,
            double quantity,
            double quoteQuantity,
            double price,
            int reduceOnly,
            double leverage,
            int timeInForce,
            int positionSide,
            int marginMode,
            sbyte* clientOrderId,
            IntPtr exchangeParameters,
            [DNNE.C99Type("struct ExExchangeWebResult*")] ExExchangeWebResult* result)
        {
            var futuresOrderClient = GCHandle.FromIntPtr(inst).Target as IFuturesOrderRestClient;
            try
            {
                var sharedSymbol = GCHandle.FromIntPtr(symbol).Target as SharedSymbol;
                var clientOrderIdString = new string(clientOrderId);
                var exchangeParameters_ = exchangeParameters != IntPtr.Zero ? GCHandle.FromIntPtr(exchangeParameters).Target as ExchangeParameters : null;
                var placeFutureRequest = new PlaceFuturesOrderRequest(
                    sharedSymbol!,
                    (SharedOrderSide)side,
                    (SharedOrderType)type,
                    quantity: (decimal)quantity,
                    quoteQuantity: quoteQuantity == 0 ? null : (decimal)quoteQuantity,
                    price: (decimal)price,
                    reduceOnly: reduceOnly == 1 ? true : null,
                    leverage: leverage == 0 ? null : (decimal)leverage,
                    timeInForce: timeInForce >= 0 ? (SharedTimeInForce)timeInForce : null,
                    positionSide: positionSide >= 0 ? (SharedPositionSide)positionSide : null,
                    marginMode: marginMode >= 0 ? (SharedMarginMode)marginMode : null,
                    clientOrderId: string.IsNullOrWhiteSpace(clientOrderIdString) ? null : clientOrderIdString,
                    exchangeParameters: exchangeParameters_);
                var ret = futuresOrderClient?.PlaceFuturesOrderAsync(placeFutureRequest).GetAwaiter().GetResult();
                if (ret == null)
                {
                    SetExchangeWebResult(result, false, 1, "PlaceFuturesOrderAsync failed", string.Empty);
                    return 1;
                }
                if (!ret.Success)
                {
                    SetExchangeWebResult(result, false, ret.Error?.Code ?? 0, ret.Error?.Message ?? string.Empty, ret.Exchange);
                    return 2;
                }

                SetExchangeWebResult(result, true, 0, string.Empty, ret.Exchange);

                if (ret.Data != null)
                {
                    ExSharedId* data = (ExSharedId*)result->Data;
                    SetFixedByteArray(data->Id, NativeUtils.MAX_ORDER_ID_LENGTH, ret.Data.Id);
                }

                return 0;
            }
            catch (Exception ex)
            {
                SetExchangeWebResult(result, false, 3, ex.Message, string.Empty);
                return 3;
            }
        }

        /// <summary>
        /// 撤单
        /// </summary>
        /// <param name="inst">实例</param>
        /// <param name="symbol">符号</param>
        /// <param name="orderId">订单ID</param>
        /// <param name="result">结果</param>
        /// <returns>0: 成功, 1: 失败, 2: 错误</returns>
        [UnmanagedCallersOnly(EntryPoint = "cancel_futures_order")]
        public unsafe static int CancelFuturesOrder(IntPtr inst, IntPtr symbol, sbyte* orderId, [DNNE.C99Type("struct ExExchangeWebResult*")] ExExchangeWebResult* result)
        {
            var futuresOrderClient = GCHandle.FromIntPtr(inst).Target as IFuturesOrderRestClient;
            try
            {
                var sharedSymbol = GCHandle.FromIntPtr(symbol).Target as SharedSymbol;
                var orderIdString = new string(orderId);
                var ret = futuresOrderClient?.CancelFuturesOrderAsync(new CancelOrderRequest(sharedSymbol!, orderIdString, exchangeParameters: null)).GetAwaiter().GetResult();
                if (ret == null)
                {
                    SetExchangeWebResult(result, false, 1, "CancelFuturesOrderAsync failed", string.Empty);
                    return 1;
                }
                if (!ret.Success)
                {
                    SetExchangeWebResult(result, false, ret.Error?.Code ?? 0, ret.Error?.Message ?? string.Empty, ret.Exchange);
                    return 2;
                }

                SetExchangeWebResult(result, true, 0, string.Empty, ret.Exchange);

                if (ret.Data != null)
                {
                    ExSharedId* data = (ExSharedId*)result->Data;
                    SetFixedByteArray(data->Id, NativeUtils.MAX_ORDER_ID_LENGTH, ret.Data.Id);
                }

                return 0;
            }
            catch (Exception ex)
            {
                SetExchangeWebResult(result, false, 3, ex.Message, string.Empty);
                return 3;
            }
        }

        /// <summary>
        /// 测试日志
        /// </summary>
        /// <returns>0: 成功, 1: 失败, 2: 错误</returns>
        [UnmanagedCallersOnly(EntryPoint = "test_log")]
        public unsafe static int TestLog()
        {
            // var logFactory = new LoggerFactory();
            // logFactory.AddProvider(new ConsoleLoggerProvider());
            // var binanceClient = new BinanceRestClient(new HttpClient(), logFactory, options => { });
            return 0;
        }

        /// <summary>
        /// 设置ExExchangeWebResult error code, error message, exchange
        /// </summary>
        /// <param name="result"></param>
        /// <param name="success"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorMessage"></param>
        /// <param name="exchange"></param>
        private unsafe static void SetExchangeWebResult(ExExchangeWebResult* result, bool success, int errorCode, string errorMessage, string exchange)
        {
            result->Success = success;
            SetFixedByteArray(result->ErrorMessage, NativeUtils.MAX_ERROR_MESSAGE_LENGTH, errorMessage);
            result->ErrorCode = errorCode;
            SetFixedByteArray(result->Exchange, NativeUtils.MAX_EXCHANGE_LENGTH, exchange);
        }

        /// <summary>
        /// 设置固定长度的字节数组
        /// </summary>
        /// <param name="array">字节数组</param>
        /// <param name="length">字节数组长度</param>
        /// <param name="value">字符串值</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal unsafe static void SetFixedByteArray(byte* array, int length, string value)
        {
            // var bytes = Encoding.UTF8.GetBytes(value);
            // bytes.CopyTo(new Span<byte>(array, length));
            // array[bytes.Length] = 0;
            if (value.Length >= length)
            {
                var strLength = length - 1;
                Encoding.UTF8.GetBytes(value.Substring(0, strLength), new Span<byte>(array, strLength));
                array[strLength] = 0;
            }
            else
            {
                var strLength = value.Length;
                Encoding.UTF8.GetBytes(value, new Span<byte>(array, strLength));
                array[strLength] = 0;
            }
        }

        /// <summary>
        /// 将固定长度的字节数组转换为字符串
        /// </summary>
        /// <param name="array">字节数组</param>
        /// <param name="length">字节数组长度</param>
        /// <returns>字符串</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal unsafe static string FixedStringToString(byte* array, int length)
        {
            var strLength = 0;
            while (array[strLength] != 0 && strLength < length)
            {
                strLength++;
            }
            return Encoding.UTF8.GetString(new Span<byte>(array, strLength));
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="handle">句柄</param>
        [UnmanagedCallersOnly(EntryPoint = "release_resource")]
        public static void ReleaseResource(IntPtr handle)
        {
            if (handle != IntPtr.Zero)
            {
                try
                {
                    GCHandle.FromIntPtr(handle).Free();
                }
                catch (InvalidOperationException ex)
                {
                    // 添加异常日志记录[4](@ref)
                    Debug.WriteLine($"资源释放异常: {ex.Message}");
                }
            }
        }
    }
}