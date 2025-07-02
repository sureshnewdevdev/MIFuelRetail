using System.ComponentModel.DataAnnotations.Schema;

namespace RetailMasters.Models;

public partial class Valetprice
{
    [Column("valetpriceid")]
    public int Valetpriceid { get; set; }

    [Column("siteid")]
    public int Siteid { get; set; }

    [Column("valettypeid")]
    public int Valettypeid { get; set; }

    [Column("price")]
    public decimal? Price { get; set; }

    [Column("effectivedate")]
    public DateOnly? Effectivedate { get; set; }

    public virtual Site Site { get; set; } = null!;
    public virtual Valettype Valettype { get; set; } = null!;
}
