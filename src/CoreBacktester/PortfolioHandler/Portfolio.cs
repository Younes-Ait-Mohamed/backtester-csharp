namespace CoreBacktester.PortfolioHandler;

public class Portfolio 
{ 
    public double[] Composition { get; private set; } 

    public double CashQuantity { get; private set; }

    public double Value { get; private set; }
    
    public DateTime LastUpdateDate { get; private set; }     

    public Portfolio(
        double value, // Valeur totale du portefeuille V
        double[] composition, // Quantités des actifs dans le portefeuille (q1, q2, ..., qn) (deltas)
        double[] spots, // Prix des actifs sous-jacents (S1, S2, ..., Sn)
        DateTime lastUpdateDate)
    {
        Value = value;
        Composition = composition;
        LastUpdateDate = lastUpdateDate;

        double compositionValue = 0;

        for (int j = 0; j < composition.Length; j++)
        {
            compositionValue += composition[j] * spots[j]; // total investi en actifs sous-jacents (actions)
        }
        CashQuantity = Value - compositionValue; // total investi en cash au taux sans risque (V - total investi en actifs sous-jacents)
    }

    public void UpdateValue(double[] spots, DateTime date)
    {
        double compositionValue = 0;

        for (int j = 0; j < Composition.Length; j++)
        {
            compositionValue += Composition[j] * spots[j];
        }
        
        double accruedFactor = PricingLibrary.MarketDataFeed.RiskFreeRateProvider.GetRiskFreeRateAccruedValue(LastUpdateDate, date); // facteur de capitalisatiion
        double accruedCash = CashQuantity * accruedFactor; // cash accumulé au taux sans risque
        Value = compositionValue + accruedCash; // valeur totale du portefeuille = total investi en actifs sous-jacents + cash accumulé au taux sans risque
    }

    public void UpdateComposition(
    double[] newComposition,
    double[] spots,
    DateTime date)
{
    double compositionValue = 0.0;

    for (int j = 0; j < newComposition.Length; j++)
    {
        compositionValue += newComposition[j] * spots[j];
    }

    CashQuantity = Value - compositionValue;

    Composition = newComposition;

    LastUpdateDate = date;
}
}