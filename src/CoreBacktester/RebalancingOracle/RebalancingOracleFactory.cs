using PricingLibrary.RebalancingOracleDescriptions;

namespace CoreBacktester.RebalancingOracle;

public static class RebalancingOracleFactory
{
    public static IRebalancingOracle Create(IRebalancingOracleDescription description)
    {
        return description switch
        {
            RegularOracleDescription regular => new RegularRebalancingOracle(regular.Period),
            SpotOracleDescription spot => new SpotRebalancingOracle(spot.Threshold),
            _ => throw new NotSupportedException("Unsupported rebalancing oracle description.")
        };
    }
}
