using System;
using System.Collections.Generic;

namespace RetailMasters.Models;

public partial class fueltype
{
    public int fueltypeid { get; set; }

    public string fueltypename { get; set; } = null!;

    public string? description { get; set; }

    public virtual ICollection<pricechange> pricechanges { get; set; } = new List<pricechange>();
}
