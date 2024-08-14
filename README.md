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
public class Main {
    static void Main()
    {
      string? value = Environment.GetEnvironmentVariable("COINBASE_PRIME_CREDENTIALS");
      if (value == null)
      {
        Console.WriteLine("COINBASE_PRIME_CREDENTIALS environment variable not set");
        return;
      }
      var credentials = new CoinbaseCredentials(value);
      var client = new CoinbasePrimeClient(credentials);
      var service = new PortfoliosService(client);
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

For an example of how to use the client, see the [`Example`](src/Coinbase/PrimeExample/example/Example.cs) class under the com.coinbase.examples package.

**Warning**: this does place a market order for a very small amount of ADA. Please ensure that you have the necessary funds in your account before running this code.
