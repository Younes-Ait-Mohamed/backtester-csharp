using System.Text.Json;
using PricingLibrary.DataClasses;

namespace CoreBacktester.IO;

public static class BacktestOutputWriter
{
    public static void Write(string outputFile, IEnumerable<OutputData> outputs)
    {
        string? outputDirectory = Path.GetDirectoryName(outputFile);

        if (!string.IsNullOrEmpty(outputDirectory))
        {
            Directory.CreateDirectory(outputDirectory);
        }

        File.WriteAllText(
            outputFile,
            JsonSerializer.Serialize(ToNotebookFormat(outputs), JsonOptionsFactory.Create())
        );
    }

    private static IEnumerable<object> ToNotebookFormat(IEnumerable<OutputData> outputs)
    {
        return outputs.Select(output => new
        {
            date = output.Date,
            value = output.Value,
            deltas = output.Deltas,
            deltasStdDev = output.DeltasStdDev,
            price = output.Price,
            priceStdDev = output.PriceStdDev,
            transactionCosts = output.TransactionCosts
        });
    }
}
