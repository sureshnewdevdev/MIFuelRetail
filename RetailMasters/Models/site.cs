using System;
using System.Collections.Generic;

namespace RetailMasters.Models;

public partial class site
{
    public int siteid { get; set; }

    public string sitecode { get; set; } = null!;

    public string description { get; set; } = null!;

    public int? supplierid { get; set; }

    public string? shiftstatus { get; set; }

    public bool? isactive { get; set; }

    public virtual supplier? supplier { get; set; }

    public virtual ICollection<valetprice> valetprices { get; set; } = new List<valetprice>();
}
