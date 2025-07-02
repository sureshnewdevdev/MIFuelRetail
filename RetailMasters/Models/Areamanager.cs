using System;
using System.Collections.Generic;

namespace RetailMasters.Models;

public partial class Areamanager
{
    public int Areamanagerid { get; set; }

    public string Name { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }
}
