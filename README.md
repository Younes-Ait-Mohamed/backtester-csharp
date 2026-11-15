# C# Backtester

A .NET 10 console application for simulating delta hedging of basket options.
Supports periodic rebalancing and rebalancing triggered by relative price movements.

## Structure

- `src/BacktestConsole`: command-line entry point.
- `src/CoreBacktester`: portfolio valuation, rebalancing rules and backtest orchestration.
- `src/CoreBacktester/IO`: JSON parameter reader, CSV market data reader and JSON results writer.

## Requirements

- .NET 10 SDK.
- An independently supplied `PricingLibrary` NuGet package, version `2.0.9`.

The pricing dependency is not distributed in this repository. Configure an authorized
NuGet source providing it before restoring. Without this package the solution cannot build.
For a local package directory:

```powershell
dotnet restore Backtester.slnx --source ./local-packages --source https://api.nuget.org/v3/index.json
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

This repository contains source code only; input datasets and generated results are not included.
