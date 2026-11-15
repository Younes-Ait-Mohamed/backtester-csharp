# C# Backtester

A .NET 10 console application for simulating delta hedging of basket options.
Supports periodic rebalancing and rebalancing triggered by relative price movements.

## Structure

- `src/BacktestConsole`: command-line entry point.
- `src/CoreBacktester`: portfolio valuation, rebalancing rules and backtest orchestration.
- `src/CoreBacktester/IO`: JSON parameter reader, CSV market data reader and JSON results writer.

## Requirements

- .NET 10 SDK.
- Internet access to restore public NuGet dependencies.

`PricingLibrary` version `2.0.9` is bundled in `packages/`. The repository's
`nuget.config` restores this package locally and public dependencies from NuGet.org.

```powershell
dotnet restore Backtester.slnx
dotnet build Backtester.slnx --configuration Release --no-restore
```

## Run

```powershell
dotnet run --project src/BacktestConsole --configuration Release --no-build -- params.json data.csv outputs/result.json
```

The parameter JSON describes the basket option, pricing inputs and rebalancing rule.
The market CSV contains a header followed by three comma-separated fields:
asset identifier, date (`MM/dd/yyyy HH:mm:ss`) and price (decimal point).

Results are a JSON array with `date`, `value`, `deltas`, `deltasStdDev`, `price`,
`priceStdDev` and `transactionCosts` fields. Transaction costs are currently set to zero.

Input datasets and generated results are not included as project files.

## Bundled Dependency

`packages/PricingLibrary.2.0.9.nupkg` is an unchanged third-party binary package
by Mnacho Echenim. Its original metadata and embedded sample data are preserved.
The package declares no redistribution license; no license for it is granted by
this repository. It depends on CsvHelper 33.1.0 and MathNet.Numerics 5.0.0.
