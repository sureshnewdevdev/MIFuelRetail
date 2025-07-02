using System;
using System.Collections.Generic;

namespace RetailMasters.Models;

public partial class Pump
{
    public int Pumpid { get; set; }

    public string Pumpcode { get; set; } = null!;

    public string? Description { get; set; }

    public int Siteid { get; set; }

    public virtual ICollection<Nozzle> Nozzles { get; set; } = new List<Nozzle>();

    public virtual Site Site { get; set; } = null!;
}
