using System.ComponentModel.DataAnnotations.Schema;

namespace RetailMasters.Models;

public partial class Fueltype
{
    [Column("fueltypeid")]
    public int Fueltypeid { get; set; }

    [Column("fueltypename")]
    public string Fueltypename { get; set; } = null!;

    [Column("description")]
    public string? Description { get; set; }

    public virtual ICollection<Fuelprice> Fuelprices { get; set; } = new List<Fuelprice>();
    public virtual ICollection<Pricechange> Pricechanges { get; set; } = new List<Pricechange>();
    public virtual ICollection<Tank> Tanks { get; set; } = new List<Tank>();
}
