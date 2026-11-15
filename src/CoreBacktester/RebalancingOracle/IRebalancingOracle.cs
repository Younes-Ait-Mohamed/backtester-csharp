using CoreBacktester.MarketData;

namespace CoreBacktester.RebalancingOracle;

public interface IRebalancingOracle
{
    bool ShouldRebalance(
        int daysSinceLastRebalancing,
        MarketDataPoint currentMarketData,
        MarketDataPoint lastRebalancingMarketData);
}
