using System;
using System.Collections.Generic;

namespace RetailMasters.Models;

public partial class message
{
    public int messageid { get; set; }

    public int senderuserid { get; set; }

    public int receiveruserid { get; set; }

    public string? subject { get; set; }

    public string? body { get; set; }

    public DateTime? sentat { get; set; }

    public virtual user receiveruser { get; set; } = null!;

    public virtual user senderuser { get; set; } = null!;
}
