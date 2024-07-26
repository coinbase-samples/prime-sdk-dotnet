namespace Coinbase.Prime.Orders
{
  using System.Net;
  using Coinbase.Core.Client;
  using Coinbase.Core.Credentials;
  using Coinbase.Core.Service;
  using Coinbase.Prime.Client;

  public class OrdersService(ICoinbaseClient client) :
   CoinbaseService(client)
  {
    public ListOpenOrdersResponse ListOpenOrders(string portfolioId, ListOpenOrdersRequest request)
    {
      return Request<ListOpenOrdersResponse>(
        HttpMethod.Get,
        $"/portfolios/{portfolioId}/open_orders",
        request,
        [HttpStatusCode.OK]);
    }

    public ListPortfolioOrdersResponse ListPortfolioOrders(string portfolioId, ListPortfolioOrdersRequest request)
    {
      return Request<ListPortfolioOrdersResponse>(
        HttpMethod.Get,
        $"/portfolios/{portfolioId}/orders",
        request,
        [HttpStatusCode.OK]);
    }
  }
}