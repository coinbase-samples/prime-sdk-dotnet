/*
 * Copyright 2026-present Coinbase Global, Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

namespace CoinbaseSdk.Prime.IntegrationTests.Infrastructure
{
  using System.Linq;
  using System.Threading.Tasks;
  using CoinbaseSdk.Prime.Activities;
  using CoinbaseSdk.Prime.AddressBook;
  using CoinbaseSdk.Prime.Allocations;
  using CoinbaseSdk.Prime.Client;
  using CoinbaseSdk.Prime.Orders;
  using CoinbaseSdk.Prime.PaymentMethods;
  using CoinbaseSdk.Prime.Portfolios;
  using CoinbaseSdk.Prime.Transactions;
  using CoinbaseSdk.Prime.Wallets;
  using Xunit;
  using OnchainSvc = CoinbaseSdk.Prime.OnchainAddressBook;

  public sealed class PrimeIntegrationFixture : IAsyncLifetime
  {
    public PrimeIntegrationFixture()
    {
      this.Ids = new DiscoveredIds();
    }

    public DiscoveredIds Ids { get; }

    public bool HasCredentials { get; private set; }

    public CoinbasePrimeClient Client { get; private set; } = null!;

    public Exception? BootstrapException { get; private set; }

    public async Task InitializeAsync()
    {
      this.HasCredentials = this.HasAllCredentialEnvVars();
      if (this.IntegrationRequired() && !this.HasCredentials)
      {
        throw new InvalidOperationException("PRIME_INTEGRATION_REQUIRED is set but PRIME_ACCESS_KEY, PRIME_PASSPHRASE, and/or PRIME_SIGNING_KEY are missing.");
      }

      if (!this.HasCredentials)
      {
        return;
      }

      try
      {
        this.Client = CoinbasePrimeClient.FromEnv();
        await this.DiscoverAsync().ConfigureAwait(false);
        this.WriteDiscoverySummary();
      }
      catch (Exception ex) when (ex is not InvalidOperationException)
      {
        this.BootstrapException = ex;
        Console.Error.WriteLine("[PrimeIntegration] bootstrap error: {0}", ex.Message);
      }
    }

    public Task DisposeAsync() => Task.CompletedTask;

    private bool IntegrationRequired() => PrimeIntegrationEnv.IsTruthy(PrimeIntegrationEnv.Get("PRIME_INTEGRATION_REQUIRED"));

    private bool HasAllCredentialEnvVars()
    {
      return !string.IsNullOrWhiteSpace(PrimeIntegrationEnv.Get("PRIME_ACCESS_KEY")) &&
             !string.IsNullOrWhiteSpace(PrimeIntegrationEnv.Get("PRIME_PASSPHRASE")) &&
             !string.IsNullOrWhiteSpace(PrimeIntegrationEnv.Get("PRIME_SIGNING_KEY"));
    }

    private void WriteDiscoverySummary()
    {
      if (!this.HasCredentials)
      {
        return;
      }

      var parts = new[]
      {
        $"portfolio={PrimeIntegrationEnv.DescribeSource(PrimeIntegrationEnv.Get("PRIME_PORTFOLIO_ID"), this.Ids.PortfolioId != null ? "ok" : "missing")}",
        $"entity={PrimeIntegrationEnv.DescribeSource(PrimeIntegrationEnv.Get("PRIME_ENTITY_ID"), this.Ids.EntityId != null ? "ok" : "missing")}",
        $"wallet={PrimeIntegrationEnv.DescribeSource(PrimeIntegrationEnv.Get("PRIME_WALLET_ID"), this.Ids.WalletId != null ? "ok" : "missing")}",
        $"order={PrimeIntegrationEnv.DescribeSource(PrimeIntegrationEnv.Get("PRIME_ORDER_ID"), this.Ids.OrderId != null ? "ok" : "missing")}",
        $"activity={PrimeIntegrationEnv.DescribeSource(PrimeIntegrationEnv.Get("PRIME_ACTIVITY_ID"), this.Ids.ActivityId != null ? "ok" : "missing")}",
        $"transaction={PrimeIntegrationEnv.DescribeSource(PrimeIntegrationEnv.Get("PRIME_TRANSACTION_ID"), this.Ids.TransactionId != null ? "ok" : "missing")}",
        $"allocation={PrimeIntegrationEnv.DescribeSource(PrimeIntegrationEnv.Get("PRIME_ALLOCATION_ID"), this.Ids.AllocationId != null ? "ok" : "missing")}",
        $"addressBook={PrimeIntegrationEnv.DescribeSource(PrimeIntegrationEnv.Get("PRIME_ADDRESS_BOOK_ID"), this.Ids.AddressBookEntryId != null ? "ok" : "missing")}",
        $"paymentMethod={PrimeIntegrationEnv.DescribeSource(PrimeIntegrationEnv.Get("PRIME_PAYMENT_METHOD_ID"), this.Ids.PaymentMethodId != null ? "ok" : "missing")}",
        $"onchainGroup={PrimeIntegrationEnv.DescribeSource(PrimeIntegrationEnv.Get("PRIME_ONCHAIN_ADDRESS_GROUP_ID"), this.Ids.OnchainAddressGroupId != null ? "ok" : "missing")}",
        $"clientNetting={PrimeIntegrationEnv.DescribeSource(PrimeIntegrationEnv.Get("PRIME_CLIENT_NETTING_ID"), this.Ids.ClientNettingId != null ? "ok" : "missing")}",
      };
      Console.WriteLine("[PrimeIntegration] discovery " + string.Join(", ", parts));
    }

    private async Task DiscoverAsync()
    {
      var portfolios = new PortfoliosService(this.Client);
      var walletEnv = PrimeIntegrationEnv.Get("PRIME_WALLET_ID");
      var orderEnv = PrimeIntegrationEnv.Get("PRIME_ORDER_ID");
      var activityEnv = PrimeIntegrationEnv.Get("PRIME_ACTIVITY_ID");
      var txEnv = PrimeIntegrationEnv.Get("PRIME_TRANSACTION_ID");
      var allocEnv = PrimeIntegrationEnv.Get("PRIME_ALLOCATION_ID");
      var addrEnv = PrimeIntegrationEnv.Get("PRIME_ADDRESS_BOOK_ID");
      var pmEnv = PrimeIntegrationEnv.Get("PRIME_PAYMENT_METHOD_ID");
      var oaEnv = PrimeIntegrationEnv.Get("PRIME_ONCHAIN_ADDRESS_GROUP_ID");
      var nettingEnv = PrimeIntegrationEnv.Get("PRIME_CLIENT_NETTING_ID");

      var pEnv = PrimeIntegrationEnv.Get("PRIME_PORTFOLIO_ID");
      if (!string.IsNullOrWhiteSpace(pEnv))
      {
        this.Ids.PortfolioId = pEnv;
      }
      else
      {
        var list = await IntegrationRetry
          .ExecuteWithRetryAsync(() => portfolios.ListPortfoliosAsync())
          .ConfigureAwait(false);
        this.Ids.PortfolioId = list.Portfolios?.FirstOrDefault()?.Id;
        if (string.IsNullOrEmpty(this.Ids.PortfolioId))
        {
          throw new InvalidOperationException("ListPortfolios returned no portfolio (set PRIME_PORTFOLIO_ID).");
        }
      }

      var eEnv = PrimeIntegrationEnv.Get("PRIME_ENTITY_ID");
      if (!string.IsNullOrWhiteSpace(eEnv))
      {
        this.Ids.EntityId = eEnv;
      }
      else
      {
        var gp = await IntegrationRetry
          .ExecuteWithRetryAsync(() => portfolios.GetPortfolioAsync(
            new GetPortfolioRequest.GetPortfolioRequestBuilder()
              .WithPortfolioId(this.Ids.PortfolioId!)
              .Build()))
          .ConfigureAwait(false);
        this.Ids.EntityId = gp.Portfolio?.EntityId;
      }

      if (!string.IsNullOrEmpty(walletEnv))
      {
        this.Ids.WalletId = walletEnv;
      }
      else
      {
        var ws = new WalletsService(this.Client);
        var w = await IntegrationRetry
          .ExecuteWithRetryAsync(() => ws.ListWalletsAsync(
            new ListWalletsRequest.ListWalletsRequestBuilder()
              .WithPortfolioId(this.Ids.PortfolioId!)
              .WithType("VAULT")
              .WithLimit(10)
              .Build()))
          .ConfigureAwait(false);
        this.Ids.WalletId = w.Wallets?.FirstOrDefault()?.Id;
      }

      if (!string.IsNullOrEmpty(orderEnv))
      {
        this.Ids.OrderId = orderEnv;
      }
      else
      {
        var os = new OrdersService(this.Client);
        var o = await IntegrationRetry
          .ExecuteWithRetryAsync(() => os.ListPortfolioOrdersAsync(
            new ListPortfolioOrdersRequest.ListPortfolioOrdersRequestBuilder()
              .WithPortfolioId(this.Ids.PortfolioId!)
              .WithLimit(5)
              .Build()))
          .ConfigureAwait(false);
        this.Ids.OrderId = o.Orders?.FirstOrDefault()?.Id;
      }

      if (!string.IsNullOrEmpty(activityEnv))
      {
        this.Ids.ActivityId = activityEnv;
      }
      else
      {
        var asvc = new ActivitiesService(this.Client);
        var a = await IntegrationRetry
          .ExecuteWithRetryAsync(() => asvc.ListActivitiesAsync(
            new ListActivitiesRequest.ListActivitiesRequestBuilder()
              .WithPortfolioId(this.Ids.PortfolioId!)
              .WithLimit(5)
              .Build()))
          .ConfigureAwait(false);
        this.Ids.ActivityId = a.Activities?.FirstOrDefault()?.Id;
      }

      if (!string.IsNullOrEmpty(txEnv))
      {
        this.Ids.TransactionId = txEnv;
      }
      else
      {
        var ts = new TransactionsService(this.Client);
        var t = await IntegrationRetry
          .ExecuteWithRetryAsync(() => ts.ListPortfolioTransactionsAsync(
            new ListPortfolioTransactionsRequest.ListPortfolioTransactionsRequestBuilder()
              .WithPortfolioId(this.Ids.PortfolioId!)
              .WithLimit(5)
              .Build()))
          .ConfigureAwait(false);
        this.Ids.TransactionId = t.Transactions?.FirstOrDefault()?.Id;
      }

      if (!string.IsNullOrEmpty(allocEnv))
      {
        this.Ids.AllocationId = allocEnv;
      }
      else
      {
        var al = new AllocationsService(this.Client);
        var r = await IntegrationRetry
          .ExecuteWithRetryAsync(() => al.ListPortfolioAllocationsAsync(
            new ListPortfolioAllocationsRequest.ListPortfolioAllocationsRequestBuilder()
              .WithPortfolioId(this.Ids.PortfolioId!)
              .WithLimit(10)
              .Build()))
          .ConfigureAwait(false);
        var first = r.Allocations?.FirstOrDefault();
        this.Ids.AllocationId = first?.RootId;
        if (string.IsNullOrEmpty(this.Ids.ClientNettingId) && !string.IsNullOrEmpty(first?.NettingId))
        {
          this.Ids.ClientNettingId = first.NettingId;
        }
      }

      if (!string.IsNullOrEmpty(addrEnv))
      {
        this.Ids.AddressBookEntryId = addrEnv;
      }
      else
      {
        var ab = new AddressBookService(this.Client);
        var ar = await IntegrationRetry
          .ExecuteWithRetryAsync(() => ab.ListAddressBookEntriesAsync(
            new ListAddressBookEntriesRequest.ListAddressBookEntriesRequestBuilder()
              .WithPortfolioId(this.Ids.PortfolioId!)
              .WithLimit(10)
              .Build()))
          .ConfigureAwait(false);
        this.Ids.AddressBookEntryId = ar.Addresses?.FirstOrDefault()?.Id;
      }

      if (!string.IsNullOrEmpty(pmEnv))
      {
        this.Ids.PaymentMethodId = pmEnv;
      }
      else if (!string.IsNullOrEmpty(this.Ids.EntityId))
      {
        var pms = new PaymentMethodsService(this.Client);
        var pr = await IntegrationRetry
          .ExecuteWithRetryAsync(() => pms.ListEntityPaymentMethodsAsync(
            new ListEntityPaymentMethodsRequest.ListEntityPaymentMethodsRequestBuilder()
              .WithEntityId(this.Ids.EntityId)
              .Build()))
          .ConfigureAwait(false);
        this.Ids.PaymentMethodId = pr.PaymentMethods?.FirstOrDefault()?.Id;
      }

      if (!string.IsNullOrEmpty(oaEnv))
      {
        this.Ids.OnchainAddressGroupId = oaEnv;
      }
      else
      {
        var oac = new OnchainSvc.OnchainAddressBookService(this.Client);
        var or = await IntegrationRetry
          .ExecuteWithRetryAsync(() => oac.ListOnchainAddressGroupsAsync(
            new OnchainSvc.ListOnchainAddressGroupsRequest.ListOnchainAddressGroupsRequestBuilder()
              .WithPortfolioId(this.Ids.PortfolioId!)
              .Build()))
          .ConfigureAwait(false);
        this.Ids.OnchainAddressGroupId = or.AddressGroups?.FirstOrDefault()?.Id;
      }

      if (!string.IsNullOrEmpty(nettingEnv))
      {
        this.Ids.ClientNettingId = nettingEnv;
      }

      if (!string.IsNullOrEmpty(PrimeIntegrationEnv.Get("PRIME_ASSET_SYMBOL")))
      {
        this.Ids.AssetSymbol = PrimeIntegrationEnv.Get("PRIME_ASSET_SYMBOL")!;
      }

      if (!string.IsNullOrEmpty(PrimeIntegrationEnv.Get("PRIME_PRODUCT_ID")))
      {
        this.Ids.ProductId = PrimeIntegrationEnv.Get("PRIME_PRODUCT_ID")!;
      }
    }
  }
}
