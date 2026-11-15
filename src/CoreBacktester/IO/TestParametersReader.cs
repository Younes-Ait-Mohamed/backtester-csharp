using System.Text.Json;
using PricingLibrary.DataClasses;

namespace CoreBacktester.IO;

public static class TestParametersReader
{
    public static BasketTestParameters Read(string paramsFile)
    {
        string json = File.ReadAllText(paramsFile);

        return JsonSerializer.Deserialize<BasketTestParameters>(
            json,
            JsonOptionsFactory.Create()
        ) ?? throw new Exception("Unable to deserialize test parameters.");
    }
}
