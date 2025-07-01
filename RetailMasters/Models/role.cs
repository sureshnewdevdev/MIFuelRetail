using System;
using System.Collections.Generic;

namespace RetailMasters.Models;

public partial class role
{
    public int roleid { get; set; }

    public string rolename { get; set; } = null!;

    public virtual ICollection<user> users { get; set; } = new List<user>();
}
