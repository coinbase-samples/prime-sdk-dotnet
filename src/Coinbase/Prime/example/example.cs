namespace Coinbase.Prime.Examples
{
  using Coinbase.Core.Credentials;
  using Coinbase.Prime.Client;
  using Coinbase.Prime.Portfolios;
  class Example
  {
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
      var response = service.ListPortfolios();
      foreach (var portfolio in response.Portfolios)
      {
        Console.WriteLine(portfolio);
        var getById = service.GetPortfolioById(portfolio.Id);
        Console.WriteLine(getById.Portfolio.Id);
      }
    }
  }
}