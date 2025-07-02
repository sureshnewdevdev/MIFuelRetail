using System;
using System.Collections.Generic;

namespace RetailMasters.Models;

public partial class Fueltype
{
    public int Fueltypeid { get; set; }

    public string Fueltypename { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Fuelprice> Fuelprices { get; set; } = new List<Fuelprice>();

    public virtual ICollection<Pricechange> Pricechanges { get; set; } = new List<Pricechange>();

    public virtual ICollection<Tank> Tanks { get; set; } = new List<Tank>();
}
