using CoreBacktester;
using CoreBacktester.IO;
using CoreBacktester.MarketData;
using PricingLibrary.DataClasses;
using PricingLibrary.MarketDataFeed;

if (args.Length != 3)
{
    Console.WriteLine("Usage: BacktestConsole <params-file> <data-file> <output-file>");
    return;
}

string paramsFile = args[0];
string dataFile = args[1];
string outputFile = args[2];

BasketTestParameters testParameters = TestParametersReader.Read(paramsFile);
List<ShareValue> shareValues = ShareValueCsvReader.Read(dataFile);
List<DataFeed> dataFeeds = MarketDataConverter.ToDataFeeds(shareValues);

var backtester = new Backtester(testParameters);
List<OutputData> outputs = backtester.Run(dataFeeds);

BacktestOutputWriter.Write(outputFile, outputs);

Console.WriteLine($"Backtest completed: {outputs.Count} dates.");
Console.WriteLine($"Output written to: {outputFile}");
