namespace Coinbase.Prime.AddressBook
{
  using Coinbase.Prime.Common;
  public class GetPortfolioAddressBookResponse
  {
    public AddressBookEntry[] Addresses { get; set; } = [];
    public Pagination? Pagination { get; set; }
  }
}