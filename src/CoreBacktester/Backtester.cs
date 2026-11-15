using CoreBacktester.MarketData;
using CoreBacktester.PortfolioHandler;
using CoreBacktester.RebalancingOracle;
using PricingLibrary.Computations;
using PricingLibrary.DataClasses;
using PricingLibrary.MarketDataFeed;

namespace CoreBacktester;

public class Backtester
{
    private readonly Pricer pricer;
    private readonly IRebalancingOracle oracle;

    public Backtester(BasketTestParameters testParameters)
    {
        pricer = new Pricer(testParameters);
        oracle = RebalancingOracleFactory.Create(testParameters.RebalancingOracleDescription);
    }

    public List<OutputData> Run(List<DataFeed> dataFeeds)
    {
        var outputs = new List<OutputData>();

        if (dataFeeds.Count == 0)
        {
            return outputs;
        }

        MarketDataPoint marketData = CreateMarketDataPoint(dataFeeds[0]);
        PricingResults pricing = marketData.Price(pricer);
        Portfolio portfolio = marketData.CreatePortfolio(pricing);
        outputs.Add(OutputDataFactory.Create(marketData, portfolio, pricing));
        int daysSinceLastRebalancing = 0;
        MarketDataPoint lastRebalancingMarketData = marketData;

        foreach (DataFeed dataFeed in dataFeeds.Skip(1))
        {
            marketData = CreateMarketDataPoint(dataFeed);
            marketData.UpdatePortfolioValue(portfolio);
            pricing = marketData.Price(pricer);
            daysSinceLastRebalancing++;

            if (oracle.ShouldRebalance(daysSinceLastRebalancing, marketData, lastRebalancingMarketData))
            {
                marketData.UpdatePortfolioComposition(portfolio, pricing);
                daysSinceLastRebalancing = 0;
                lastRebalancingMarketData = marketData;
            }

            outputs.Add(OutputDataFactory.Create(marketData, portfolio, pricing));
        }

        return outputs;
    }

    private MarketDataPoint CreateMarketDataPoint(DataFeed dataFeed)
    {
        return new MarketDataPoint(dataFeed, pricer.UnderlyingShareIds);
    }
}
