using CoreBacktester.MarketData;
using CoreBacktester.PortfolioHandler;
using PricingLibrary.Computations;
using PricingLibrary.DataClasses;

namespace CoreBacktester;

public static class OutputDataFactory
{
    public static OutputData Create(
        MarketDataPoint marketData,
        Portfolio portfolio,
        PricingResults pricing)
    {
        return new OutputData
        {
            Date = marketData.Date,
            Value = portfolio.Value,
            Deltas = portfolio.Composition,
            DeltasStdDev = pricing.DeltaStdDev,
            Price = pricing.Price,
            PriceStdDev = pricing.PriceStdDev,
            TransactionCosts = 0.0
        };
    }
}
