namespace Coinbase.Prime.Client
{
  using Coinbase.Core.Client;
  using Coinbase.Core.Credentials;
  using Coinbase.Prime.Portfolios;

  public class CoinbasePrimeClient : CoinbaseClient
  {
    private const string DefaultApiBasePath = "api.prime.coinbase.com/v1";

    public CoinbasePrimeClient(CoinbaseCredentials credentials) : base(credentials, DefaultApiBasePath)
    {
    }

    public CoinbasePrimeClient(CoinbaseCredentials credentials, string apiBasePath) : base(credentials, apiBasePath)
    {
    }
  }
}