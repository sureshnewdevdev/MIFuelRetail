using System.ComponentModel.DataAnnotations.Schema;

namespace RetailMasters.Models;

public partial class Pricechange
{
    [Column("changeid")]
    public int Changeid { get; set; }

    [Column("fueltypeid")]
    public int Fueltypeid { get; set; }

    [Column("oldprice")]
    public decimal? Oldprice { get; set; }

    [Column("newprice")]
    public decimal? Newprice { get; set; }

    [Column("changedate")]
    public DateTime? Changedate { get; set; }

    public virtual Fueltype Fueltype { get; set; } = null!;
}
