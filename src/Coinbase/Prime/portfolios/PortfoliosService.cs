namespace Coinbase.Prime.Portfolios
{
  using System.Net;
  using Coinbase.Core.Client;
  using Coinbase.Core.Credentials;
  using Coinbase.Core.Service;
  using Coinbase.Prime.Client;

  public class PortfoliosService(ICoinbaseClient client) :
   CoinbaseService(client)
  {
    public ListPortfoliosResponse ListPortfolios()
    {
      return Request<ListPortfoliosResponse>(
        HttpMethod.Get,
        "/portfolios",
        [HttpStatusCode.OK]);
    }

    public GetPortfolioByIdResponse GetPortfolioById(string portfolioId)
    {
      return Request<GetPortfolioByIdResponse>(
        HttpMethod.Get,
        $"/portfolios/{portfolioId}",
        [HttpStatusCode.OK]);
    }

    public GetPortfolioCreditInformationResponse GetPortfolioCreditInformation(string portfolioId)
    {
      return Request<GetPortfolioCreditInformationResponse>(
        HttpMethod.Post,
        $"/portfolios/{portfolioId}/credit",
        [HttpStatusCode.OK]);
    }
  }
}