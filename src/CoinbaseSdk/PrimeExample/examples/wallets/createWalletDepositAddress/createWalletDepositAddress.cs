namespace CoinbaseSdk.PrimeExample.examples.wallets.createWalletDepositAddress
{
    using CoinbaseSdk.Core.Credentials;
    using CoinbaseSdk.Core.Serialization;
    using CoinbaseSdk.Prime.Client;
  using CoinbaseSdk.Prime.Wallets;

  class Example
    {
        static void Main()
        {
            string? credentialsBlob = Environment.GetEnvironmentVariable(
                "COINBASE_PRIME_CREDENTIALS"
            );
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
                Console.WriteLine(
                    "Failed to parse COINBASE_PRIME_CREDENTIALS environment variable"
                );
                return;
            }

            var client = new CoinbasePrimeClient(credentials!);

            var walletService = new WalletsService(client);

            var request = new CreateWalletDepositAddressRequest.CreateWalletDepositAddressRequestBuilder()
                .WithPortfolioId(portfolioId)
                .WithWalletId("sample-wallet-id")
                .WithNetworkId("ethereum")
                .Build();
        }
    }
}