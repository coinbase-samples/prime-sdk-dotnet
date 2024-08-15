namespace CoinbaseSdk.PrimeExample.Example
{
  using CoinbaseSdk.Core.Credentials;
  using CoinbaseSdk.Core.Serialization;
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

      var orderService = new OrdersService(client);

      var request = new CreateOrderRequest(portfolio.Id!)
      {
        BaseQuantity = "5",
        LimitPrice = "0.32",
        Side = OrderSide.BUY,
        ProductId = "ADA-USD",
        Type = OrderType.LIMIT,
        ExpiryTime = new DateTimeOffset(DateTime.UtcNow.AddMinutes(5)).ToString("o"),
        ClientOrderId = Guid.NewGuid().ToString()
      };

      var createOrderResponse = orderService.CreateOrder(request);

      Console.WriteLine($"CreateOrderResponse: {serializer.Serialize(createOrderResponse)}");

      // sleep for 1 second and then call GetOrder
      Thread.Sleep(1000);

      var getOrderResponse = orderService.GetOrderByOrderId(
          new GetOrderByOrderIdRequest(portfolio.Id!, createOrderResponse.OrderId!));

      Console.WriteLine($"GetOrderResponse after Create: {serializer.Serialize(getOrderResponse)}");

      var cancelOrderResponse = orderService.CancelOrder(
          new CancelOrderRequest(portfolio.Id!, createOrderResponse.OrderId!));

      Console.WriteLine($"CancelOrderResponse: {serializer.Serialize(cancelOrderResponse)}");

      Thread.Sleep(1000);

      getOrderResponse = orderService.GetOrderByOrderId(
          new GetOrderByOrderIdRequest(portfolio.Id!, createOrderResponse.OrderId!));

      Console.WriteLine($"Post Cancel Order: {serializer.Serialize(getOrderResponse)}");
    }
  }
}
