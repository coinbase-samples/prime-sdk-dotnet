namespace Coinbase.Prime.AddressBook
{
  using Coinbase.Prime.Common;
  using Newtonsoft.Json;
  public class GetPortfolioAddressBookRequest : BaseListRequest
  {
    [JsonProperty("currency_symbol")]
    public string? CurrencySymbol { get; set; }
    public string? Search { get; set; }
  }
}