using System;
using System.Collections.Generic;

namespace RetailMasters.Models;

public partial class Tank
{
    public int Tankid { get; set; }

    public string Tankcode { get; set; } = null!;

    public decimal? Capacity { get; set; }

    public int? Fueltypeid { get; set; }

    public int Siteid { get; set; }

    public virtual Fueltype? Fueltype { get; set; }

    public virtual ICollection<Nozzle> Nozzles { get; set; } = new List<Nozzle>();

    public virtual Site Site { get; set; } = null!;
}
