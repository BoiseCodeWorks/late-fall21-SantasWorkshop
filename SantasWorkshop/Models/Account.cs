namespace SantasWorkshop.Models
{
  public class Account
  {
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Picture { get; set; }
  }

  // ViewModels are everything the core object is (account) and inherits from that object
  // however it is extended to keep extra info for the relationship
  public class AccountStationViewModel : Account
  {
    public int AccountStationId { get; set; }
  }


}