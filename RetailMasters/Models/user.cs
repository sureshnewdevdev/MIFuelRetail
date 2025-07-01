using System;
using System.Collections.Generic;

namespace RetailMasters.Models;

public partial class user
{
    public int userid { get; set; }

    public string username { get; set; } = null!;

    public string passwordhash { get; set; } = null!;

    public int? roleid { get; set; }

    public string? email { get; set; }

    public bool? isactive { get; set; }

    public virtual ICollection<message> messagereceiverusers { get; set; } = new List<message>();

    public virtual ICollection<message> messagesenderusers { get; set; } = new List<message>();

    public virtual role? role { get; set; }
}
