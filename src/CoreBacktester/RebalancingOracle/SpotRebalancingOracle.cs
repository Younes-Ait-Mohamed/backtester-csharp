using CoreBacktester.MarketData;

namespace CoreBacktester.RebalancingOracle;

public class SpotRebalancingOracle : IRebalancingOracle
{
    public double Threshold { get; }

    public SpotRebalancingOracle(double threshold)
    {
        Threshold = threshold;
    }

    public bool ShouldRebalance(
        double[] currentSpots,
        double[] lastRebalancingSpots)
    {
        for (int i = 0; i < currentSpots.Length; i++)
        {
            double absoluteReturn =
                Math.Abs(currentSpots[i] / lastRebalancingSpots[i] - 1.0);

            if (absoluteReturn >= Threshold)
            {
                return true;
            }
        }

        return false;
    }

    public bool ShouldRebalance(
        int daysSinceLastRebalancing,
        MarketDataPoint currentMarketData,
        MarketDataPoint lastRebalancingMarketData)
    {
        return currentMarketData.HasMovedEnoughFrom(lastRebalancingMarketData, Threshold);
    }
}
