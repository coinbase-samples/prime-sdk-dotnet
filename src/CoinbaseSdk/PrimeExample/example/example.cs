namespace CoinbaseSdk.PrimeExample.Example
{
  using System.Text.Json;
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Core.Credentials;
  using CoinbaseSdk.Prime.Client;
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

      var credentials = JsonSerializer.Deserialize<CoinbaseCredentials>(credentialsBlob, new JsonSerializerOptions(JsonSerializerDefaults.Web));
      var client = new CoinbasePrimeClient(credentials);

      var portfoliosService = new PortfoliosService(client);

      var portfolio = portfoliosService.GetPortfolioById(
        new GetPortfolioByIdRequest(portfolioId)).Portfolio!;
      Console.WriteLine(portfolio);

      Console.WriteLine(portfolio.Id!);

      var orderService = new OrdersService(client);

      Console.WriteLine(OrderType.MARKET);

      var request = new CreateOrderRequest(portfolio.Id)
      {
        BaseQuantity = "0.001",
        Side = OrderSide.BUY,
        ProductId = "ADA-USD",
        Type = OrderType.MARKET,
        ClientOrderId = Guid.NewGuid().ToString()
      };

      var createOrderResponse = orderService.CreateOrder(request);

      Console.WriteLine(createOrderResponse.OrderId);
    }
  }
}