# Coinbase Prime .NET SDK

## Overview

The _Coinbase Prime .NET SDK_ is a sample library that demonstrates the structure of a [Coinbase Prime](https://prime.coinbase.com/) driver for
the [REST APIs](https://docs.cdp.coinbase.com/prime/reference).

## License

The _Coinbase Prime .NET SDK_ sample library is free and open source and released under the [Apache License, Version 2.0](LICENSE).

The application and code are only available for demonstration purposes.

## Installation

The _Coinbase Prime .NET SDK_ is vended through NuGet and available for installation via the `dotnet` CLI.

```bash
dotnet add package CoinbaseSdk.Prime
```

## Usage

To use the _Coinbase Prime .NET SDK_, initialize the Credentials class and create a new client. The Credentials struct is JSON
enabled. Ensure that Prime API credentials are stored in a secure manner.

```c#
namespace CoinbaseSdk.PrimeExample.Example
{
  using CoinbaseSdk.Core.Credentials;
  using CoinbaseSdk.Core.Serialization;
  using CoinbaseSdk.Prime.Client;
  using CoinbaseSdk.Prime.Model;
  using CoinbaseSdk.Prime.Orders;
  using CoinbaseSdk.Prime.Portfolios;

  class Example
  {
    static void Main()
    {
      string? credentialsBlob = Environment.GetEnvironmentVariable("COINBASE_PRIME_CREDENTIALS");
      if (credentialsBlob == null)
      {
        Console.WriteLine("COINBASE_PRIME_CREDENTIALS environment variable not set");
        return;
      }

      string? portfolioId = Environment.GetEnvironmentVariable("COINBASE_PRIME_PORTFOLIO_ID");
      if (portfolioId == null)
      {
        Console.WriteLine("COINBASE_PRIME_PORTFOLIO_ID environment variable not set");
        return;
      }

      var serializer = new JsonUtility();

      var credentials = serializer.Deserialize<CoinbaseCredentials>(credentialsBlob);

      if (credentials == null)
      {
        Console.WriteLine("Failed to parse COINBASE_PRIME_CREDENTIALS environment variable");
        return;
      }

      var client = new CoinbasePrimeClient(credentials!);

      var portfoliosService = new PortfoliosService(client);

      var portfolio = portfoliosService.GetPortfolioById(
        new GetPortfolioByIdRequest(portfolioId)).Portfolio!;

      Console.WriteLine($"Portfolio: {serializer.Serialize(portfolio)}");
    }
  }
}
```

The JSON format expected for `COINBASE_PRIME_CREDENTIALS` is:

```
{
  "accessKey": "",
  "passphrase": "",
  "signingKey": ""
}
```

For an example of how to use the client, see the [`Example`](src/CoinbaseSdk/PrimeExample/example/Example.cs) file. To execute the example, run the following command:

```bash
dotnet run --project src/CoinbaseSdk.PrimeExample/CoinbaseSdk.PrimeExample.csproj
```

**Warning**: this does place a limit order for a very small amount of ADA.
Please ensure that you have the necessary funds in your account before running this code.
The example code should cancel the order, however if something breaks you may need to go manually cancel the order.
