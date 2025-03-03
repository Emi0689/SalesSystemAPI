using System;
using System.Collections.Generic;

namespace SalesSystem.Model.Entities;

public partial class Idnumber
{
    public int IdIdnumber { get; set; }

    public int LastNumber { get; set; }

    public DateTime? Timestamp { get; set; }
}
