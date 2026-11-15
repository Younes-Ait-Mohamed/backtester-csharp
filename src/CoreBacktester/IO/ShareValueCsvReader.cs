using System.Globalization;
using PricingLibrary.MarketDataFeed;

namespace CoreBacktester.IO;

public static class ShareValueCsvReader
{
    public static List<ShareValue> Read(string dataFile)
    {
        var shareValues = new List<ShareValue>();
        string[] lines = File.ReadAllLines(dataFile);

        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i].Trim();

            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            string[] parts = line.Split(',');

            if (parts.Length != 3)
            {
                continue;
            }

            shareValues.Add(new ShareValue
            {
                Id = parts[0].Trim(),
                DateOfPrice = DateTime.ParseExact(
                    parts[1].Trim(),
                    "MM/dd/yyyy HH:mm:ss",
                    CultureInfo.InvariantCulture
                ),
                Value = double.Parse(
                    parts[2].Trim(),
                    CultureInfo.InvariantCulture
                )
            });
        }

        return shareValues;
    }
}
