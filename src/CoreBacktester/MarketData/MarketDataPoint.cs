using CoreBacktester.PortfolioHandler;
using PricingLibrary.Computations;
using PricingLibrary.DataClasses;
using PricingLibrary.MarketDataFeed;

namespace CoreBacktester.MarketData;

public class MarketDataPoint
{
    private readonly double[] spots;

    public DateTime Date { get; }

    public MarketDataPoint(DataFeed dataFeed, IEnumerable<string> underlyingShareIds)
    {
        Date = dataFeed.Date;
        spots = underlyingShareIds.Select(id => dataFeed.PriceList[id]).ToArray();
    }

    public PricingResults Price(Pricer pricer)
    {
        return pricer.Price(Date, spots);
    }

    public Portfolio CreatePortfolio(PricingResults pricing)
    {
        return new Portfolio(pricing.Price, pricing.Deltas, spots, Date);
    }

    public void UpdatePortfolioValue(Portfolio portfolio)
    {
        portfolio.UpdateValue(spots, Date);
    }

    public void UpdatePortfolioComposition(Portfolio portfolio, PricingResults pricing)
    {
        portfolio.UpdateComposition(pricing.Deltas, spots, Date);
    }

    public bool HasMovedEnoughFrom(MarketDataPoint reference, double threshold)
    {
        for (int i = 0; i < spots.Length; i++)
        {
            if (Math.Abs(spots[i] / reference.spots[i] - 1.0) >= threshold)
            {
                return true;
            }
        }

        return false;
    }
}
