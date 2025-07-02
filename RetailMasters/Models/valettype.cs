using System.ComponentModel.DataAnnotations.Schema;

namespace RetailMasters.Models;

public partial class Valettype
{
    [Column("valettypeid")]
    public int Valettypeid { get; set; }

    [Column("valettypename")]
    public string Valettypename { get; set; } = null!;

    public virtual ICollection<Valetprice> Valetprices { get; set; } = new List<Valetprice>();
}
