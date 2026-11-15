using CoreBacktester.MarketData;

namespace CoreBacktester.RebalancingOracle;

public class RegularRebalancingOracle : IRebalancingOracle
{
    public int Period { get; }

    public RegularRebalancingOracle(int period)
    {
        Period = period;
    }
    public bool ShouldRebalance(int daysSinceLastRebalancing)
    {
        return daysSinceLastRebalancing >= Period;
    }

    public bool ShouldRebalance(
        int daysSinceLastRebalancing,
        MarketDataPoint currentMarketData,
        MarketDataPoint lastRebalancingMarketData)
    {
        return ShouldRebalance(daysSinceLastRebalancing);
    }
}
