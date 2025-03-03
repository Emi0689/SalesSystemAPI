using System;
using System.Collections.Generic;

namespace SalesSystem.Model.Entities;

public partial class Menu
{
    public int IdMenu { get; set; }

    public string? Name { get; set; }

    public string? Icon { get; set; }

    public string? Url { get; set; }

    public virtual ICollection<MenuRol> MenuRols { get; set; } = new List<MenuRol>();
}
