namespace Coinbase.PrimeExample.Example
{
  using System.Text.Json;
  using System.Text.Json.Serialization;
  using Coinbase.Core.Credentials;
  using Coinbase.Prime.Client;
  using Coinbase.Prime.Orders;
  using Coinbase.Prime.Portfolios;
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
      Console.WriteLine(credentialsBlob);

      string? portfolioId = Environment.GetEnvironmentVariable("COINBASE_PRIME_PORTFOLIO_ID");
      if (portfolioId == null)
      {
        Console.WriteLine("COINBASE_PRIME_PORTFOLIO_ID environment variable not set");
        return;
      }

      var credentials = JsonSerializer.Deserialize<CoinbaseCredentials>(credentialsBlob, new JsonSerializerOptions(JsonSerializerDefaults.Web));
      var client = new CoinbasePrimeClient(credentials);

      var portfoliosService = new PortfoliosService(client);

      var portfolio = portfoliosService.GetPortfolioById(portfolioId).Portfolio!;
      Console.WriteLine(portfolio);

      Console.WriteLine(portfolio.Id!);

      var orderService = new OrdersService(client);

      Console.WriteLine(OrderType.MARKET);

      var request = new CreateOrderRequest
      {
        BaseQuantity = "0.001",
        Side = OrderSide.BUY,
        ProductId = "ADA-USD",
        Type = OrderType.MARKET,
        ClientOrderId = Guid.NewGuid().ToString()
      };

      var createOrderResponse = orderService.CreateOrder(
        portfolio.Id,
        new CreateOrderRequest
        {
          BaseQuantity = "0.001",
          Side = OrderSide.BUY,
          ProductId = "ADA-USD",
          Type = OrderType.MARKET,
          ClientOrderId = Guid.NewGuid().ToString()
        }
      );

      Console.WriteLine(createOrderResponse.OrderId);
    }
  }
}