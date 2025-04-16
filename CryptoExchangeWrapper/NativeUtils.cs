using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CryptoExchangeWrapper
{
    /// <summary>
    /// NativeUtils
    /// </summary>
    internal static class NativeUtils
    {
        // public const string DATE_TIME_FORMAT = "yyyy-MM-dd HH:mm:ss.fff";
        public const string DATE_TIME_FORMAT = "yyyy-MM-ddTHH:mm:ss.fffzzz";

        public const int MAX_ERROR_MESSAGE_LENGTH = 4096;
        // 最大symbol长度
        public const int MAX_SYMBOL_LENGTH = 64;
        // 最大orderId长度
        public const int MAX_ORDER_ID_LENGTH = 64;
        // 最大exchange长度
        public const int MAX_EXCHANGE_LENGTH = 64;
        // 最大listenKey长度
        public const int MAX_LISTEN_KEY_LENGTH = 128;

        // 最大DateTime长度
        public const int MAX_DATE_TIME_LENGTH = 64;

        // TimeInForce
        public const int MAX_TIME_IN_FORCE_LENGTH = 64;

        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// 将DateTime转换为Unix时间戳（毫秒）
        /// </summary>
        /// <param name="dateTime">DateTime</param>
        /// <returns>Unix时间戳（毫秒）</returns>
        internal static long DateTimeToUnixTimestampMs(DateTime dateTime)
        {
            if (dateTime.Kind == DateTimeKind.Utc)
            {
                return (long)(dateTime - UnixEpoch).TotalMilliseconds;
            }
            else
            {
                return (long)(dateTime.ToUniversalTime() - UnixEpoch).TotalMilliseconds;
            }
        }
    }

    /// <summary>
    /// Global exchange options
    /// </summary>
    internal class ExGlobalExchangeOptions
    {
        /// <summary>
        /// Proxy
        /// </summary>
        [JsonPropertyName("proxy")]
        public ExApiProxy? Proxy { get; set; }

        /// <summary>
        /// Testnet
        /// </summary>
        [JsonPropertyName("testnet")]
        public bool? Testnet { get; set; }

        /// <summary>
        /// Api credentials
        /// </summary>
        [JsonPropertyName("api_credentials")]
        public List<ExApiCredentials>? ApiCredentials { get; set; }
    }

    /// <summary>
    /// Api proxy
    /// </summary>
    internal class ExApiProxy
    {
        [JsonPropertyName("host")]
        public string? Host { get; set; }

        [JsonPropertyName("port")]
        public int Port { get; set; }

        [JsonPropertyName("username")]
        public string? Username { get; set; }

        [JsonPropertyName("password")]
        public string? Password { get; set; }
    }

    /// <summary>
    /// Api credentials
    /// </summary>
    internal class ExApiCredentials
    {
        [JsonPropertyName("exchange")]
        public string? Exchange { get; set; }

        [JsonPropertyName("api_key")]
        public string? ApiKey { get; set; }

        [JsonPropertyName("api_secret")]
        public string? ApiSecret { get; set; }

        [JsonPropertyName("passphrase")]
        public string? Passphrase { get; set; }
    }

    /// <summary>
    /// Exchange web result
    /// </summary>
    public unsafe struct ExExchangeWebResult
    {
        public bool Success; // 0: false, 1: true
        public Int32 ErrorCode;
        public fixed byte ErrorMessage[NativeUtils.MAX_ERROR_MESSAGE_LENGTH];
        public fixed byte Exchange[NativeUtils.MAX_EXCHANGE_LENGTH];
        public IntPtr Data;
    }

    /// <summary>
    /// Exchange native result
    /// </summary>
    public unsafe struct ExExchangeNativeResult
    {
        public bool Success; // 0: false, 1: true
        public Int32 ErrorCode;
        public fixed byte ErrorMessage[NativeUtils.MAX_ERROR_MESSAGE_LENGTH];
        public fixed byte Exchange[NativeUtils.MAX_EXCHANGE_LENGTH];
        public IntPtr Data;
    }

    /// <summary>
    /// Spot ticker
    /// </summary>
    internal unsafe struct ExSharedSpotTicker
    {
        /// <summary>
        /// Symbol
        /// </summary>
        public fixed byte Symbol[NativeUtils.MAX_SYMBOL_LENGTH];

        /// <summary>
        /// Last trade price
        /// </summary>
        public double LastPrice;

        /// <summary>
        /// Highest price in last 24h
        /// </summary>
        public double HighPrice;

        /// <summary>
        /// Lowest price in last 24h
        /// </summary>
        public double LowPrice;

        /// <summary>
        /// Trade volume in base asset in the last 24h
        /// </summary>
        public double Volume;

        /// <summary>
        /// Change percentage in the last 24h
        /// </summary>
        public double ChangePercentage;
    }

    /// <summary>
    /// StartListenKeyAsync
    /// </summary>
    internal unsafe struct ExSharedSpotListenKey
    {
        public fixed byte ListenKey[NativeUtils.MAX_LISTEN_KEY_LENGTH];
    }

    /// <summary>
    /// Spot tickers
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct ExSharedSpotTickers
    {
        public Int32 Count;
        public readonly delegate* unmanaged[Cdecl]<IntPtr, sbyte*, int, double, double, double, double, double, void> OnTickerAdded;
    }

    /// <summary>
    /// Futures symbols
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct ExSharedFuturesSymbols
    {
        public Int32 Count;

        public readonly delegate* unmanaged[Cdecl]<IntPtr, // this
            sbyte*, int,    // baseAssetBytesPtr
            sbyte*, int,    // quoteAssetBytesPtr
            sbyte*, int,    // nameBytesPtr
            double, // MinTradeQuantity
            double, // MinNotionalValue
            double, // MaxTradeQuantity
            double, // QuantityStep
            double, // PriceStep
            int, // QuantityDecimals
            int, // PriceDecimals
            int, // PriceSignificantFigures
            byte, // Trading
            byte, // SymbolType
            double, // ContractSize
            sbyte*, int, // deliveryTimeBytesPtr
            void> OnFuturesSymbolAdd;
    }

    /// <summary>
    /// Futures ticker
    /// </summary>
    internal unsafe struct ExSharedFuturesTicker
    {
        public fixed byte Symbol[NativeUtils.MAX_SYMBOL_LENGTH];
        public double LastPrice;
        public double HighPrice;
        public double LowPrice;
        public double Volume;
        public double ChangePercentage;
        public double MarkPrice;
        public double IndexPrice;
        public double FundingRate;
        public fixed byte NextFundingTime[NativeUtils.MAX_DATE_TIME_LENGTH];
    }

    /// <summary>
    /// Futures tickers
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct ExSharedFuturesTickers
    {
        public Int32 Count;

        public readonly delegate* unmanaged[Cdecl]<IntPtr, // this
            sbyte*, int, // symbolBytesPtr
            double, // lastPrice
            double, // highPrice
            double, // lowPrice
            double, // volume
            double, // changePercentage
            double, // markPrice
            double, // indexPrice
            double, // fundingRate
            sbyte*, int, // nextFundingTimeBytesPtr
            void> OnFuturesTickerAdd;
    }

    /// <summary>
    /// Order book
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct ExSharedOrderBook
    {
        public Int32 AsksCount;
        public Int32 BidsCount;
        public readonly delegate* unmanaged[Cdecl]<IntPtr, // this
            double, // quantity
            double, // price
            void> OnOrderBookAskAdded;
        public readonly delegate* unmanaged[Cdecl]<IntPtr, // this
            double, // quantity
            double, // price
            void> OnOrderBookBidAdded;
    }

    /// <summary>
    /// Kline
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct ExSharedKlines
    {
        public Int32 Count;
        public readonly delegate* unmanaged[Cdecl]<IntPtr, // this
            sbyte*, int, // openTimeBytesPtr
            Int64, // openTimeMs
            double, // closePrice
            double, // highPrice
            double, // lowPrice
            double, // openPrice
            double, // volume
            void> OnKlineAdded;
    }

    /// <summary>
    /// Balances
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct ExSharedBalances
    {
        public Int32 Count;

        public readonly delegate* unmanaged[Cdecl]<IntPtr, // this
            sbyte*, // assetBytesPtr
            int, // assetLength
            double, // available
            double, // total
            sbyte*, // isolatedMarginSymbolBytesPtr
            int, // isolatedMarginSymbolLength
            void> OnBalanceAdded;
    }

    /// <summary>
    /// Futures order
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct ExSharedFuturesOrder
    {
        public fixed byte Symbol[NativeUtils.MAX_SYMBOL_LENGTH];
        public fixed byte OrderId[NativeUtils.MAX_ORDER_ID_LENGTH];
        public int OrderType;
        public int Side;
        public int Status;
        public int TimeInForce;
        public int PositionSide;
        public byte ReduceOnly;
        public double Quantity;
        public double QuantityFilled;
        public double QuoteQuantity;
        public double QuoteQuantityFilled;
        internal double OrderPrice;
        internal double AveragePrice;
        internal fixed byte ClientOrderId[NativeUtils.MAX_ORDER_ID_LENGTH];
        internal fixed byte FeeAsset[NativeUtils.MAX_SYMBOL_LENGTH];
        internal double Fee;
        internal double Leverage;
        internal fixed byte CreateTime[NativeUtils.MAX_DATE_TIME_LENGTH];
        internal fixed byte UpdateTime[NativeUtils.MAX_DATE_TIME_LENGTH];
    }

    /// <summary>
    /// Futures orders
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct ExSharedFuturesOrders
    {
        public Int32 Count;
        public readonly delegate* unmanaged[Cdecl]<IntPtr, // this
            sbyte*, int, // symbolBytesPtr
            sbyte*, int, // orderIdBytesPtr
            int, // orderType
            int, // side
            int, // status
            int, // timeInForce
            int, // positionSide
            byte, // reduceOnly
            double, // quantity
            double, // quantityFilled
            double, // quoteQuantity
            double, // quoteQuantityFilled
            double, // orderPrice
            double, // averagePrice
            sbyte*, int, // clientOrderIdBytesPtr
            sbyte*, int, // feeAssetBytesPtr
            double, // fee
            double, // leverage
            sbyte*, int, // createTimeBytesPtr
            long, // createTime
            sbyte*, int, // updateTimeBytesPtr
            long,// updateTime
            void> OnFuturesOrderAdded;
    }

    /// <summary>
    /// Positions
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct ExSharedPositions
    {
        internal Int32 Count;

        internal readonly delegate* unmanaged[Cdecl]<IntPtr, // this
            sbyte*, int, // symbolBytesPtr
            double, // positionSize
            int, // side
            double, // averageOpenPrice
            double, // unrealizedPnl
            double, // liquidationPrice
            double, // leverage
            sbyte*, int, // updateTimeBytesPtr
            void> OnPositionAdded;
    }

    /// <summary>
    /// Leverage
    /// </summary>
    internal unsafe struct ExSharedLeverage
    {
        internal double Leverage;
        internal int PositionSide;
        internal int MarginMode;
    }

    /// <summary>
    /// Position mode
    /// </summary>
    internal unsafe struct ExSharedPositionMode
    {
        internal int TradeMode;
    }

    /// <summary>
    /// Order id
    /// </summary>
    internal unsafe struct ExSharedId
    {
        internal fixed byte Id[NativeUtils.MAX_ORDER_ID_LENGTH];
    }
}