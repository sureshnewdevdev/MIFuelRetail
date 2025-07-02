using System;
using System.Collections.Generic;

namespace RetailMasters.Models;

public partial class Message
{
    public int Messageid { get; set; }

    public int Senderuserid { get; set; }

    public int Receiveruserid { get; set; }

    public string? Subject { get; set; }

    public string? Body { get; set; }

    public DateTime? Sentat { get; set; }

    public virtual User Receiveruser { get; set; } = null!;

    public virtual User Senderuser { get; set; } = null!;
}
