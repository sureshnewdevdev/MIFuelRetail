using System;
using System.Collections.Generic;

namespace RetailMasters.Models;

public partial class User
{
    public int Userid { get; set; }

    public string Username { get; set; } = null!;

    public string Passwordhash { get; set; } = null!;

    public int? Roleid { get; set; }

    public string? Email { get; set; }

    public bool? Isactive { get; set; }

    public virtual ICollection<Message> MessageReceiverusers { get; set; } = new List<Message>();

    public virtual ICollection<Message> MessageSenderusers { get; set; } = new List<Message>();

    public virtual Role? Role { get; set; }
}
