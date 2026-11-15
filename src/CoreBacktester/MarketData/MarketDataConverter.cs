using PricingLibrary.MarketDataFeed;

namespace CoreBacktester.MarketData;

public static class MarketDataConverter
{
    public static List<DataFeed> ToDataFeeds(
        IEnumerable<ShareValue> shareValues)
    {
        return shareValues
            .GroupBy(x => x.DateOfPrice)
            .OrderBy(group => group.Key)
            .Select(group =>
                new DataFeed(
                    group.Key,
                    group.ToDictionary(
                        x => x.Id,
                        x => x.Value
                    )
                )
            )
            .ToList();
    }
}