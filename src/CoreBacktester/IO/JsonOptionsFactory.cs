using System.Text.Json;
using PricingLibrary.RebalancingOracleDescriptions;

namespace CoreBacktester.IO;

public static class JsonOptionsFactory
{
    public static JsonSerializerOptions Create()
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        options.Converters.Add(new RebalancingOracleDescriptionConverter());
        return options;
    }
}
