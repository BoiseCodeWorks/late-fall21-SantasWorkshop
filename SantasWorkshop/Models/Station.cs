using System.ComponentModel.DataAnnotations;

namespace SantasWorkshop.Models
{
  public class Station
  {
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string CreatorId { get; set; }
  }

  public class StationAccountViewModel : Station
  {
    public int AccountStationId { get; set; }
  }
}