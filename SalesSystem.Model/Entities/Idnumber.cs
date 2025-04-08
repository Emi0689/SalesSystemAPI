using System;
using System.Collections.Generic;

namespace SalesSystem.Model.Entities;

public partial class IdNumber
{
    public int IdIdNumber { get; set; }

    public int LastNumber { get; set; }

    public DateTime? Timestamp { get; set; }
}
