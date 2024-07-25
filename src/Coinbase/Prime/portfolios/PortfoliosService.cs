namespace Coinbase.Prime.Portfolios
{
  using Coinbase.Core.Client;
  using Coinbase.Core.Credentials;
  using Coinbase.Core.Service;
  using Coinbase.Prime.Client;

  public class PortfoliosService(ICoinbaseClient client) :
   CoinbaseService(client)
  {
    public ListPortfoliosResponse ListPortfolios()
    {
      return Request<ListPortfoliosResponse>(HttpMethod.Get, "/portfolios");
    }

    public GetPortfolioByIdResponse GetPortfolioById(string portfolioId)
    {
      return Request<GetPortfolioByIdResponse>(HttpMethod.Get, $"/portfolios/{portfolioId}");
    }

    public GetPortfolioCreditInformationResponse(string portfolioId)
    {
      return Request<GetPortfolioCreditInformationResponse>(HttpMethod.Post, $"/portfolios/{portfolioId}/credit");
    }
  }
}