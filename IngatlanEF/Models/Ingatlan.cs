using System;
using System.Collections.Generic;

namespace IngatlanEF.Models;

public partial class Ingatlan
{
    public int Id { get; set; }

    public string Település { get; set; } = null!;

    public string Cim { get; set; } = null!;

    public int Ár { get; set; }

    public string Tipus { get; set; } = null!;

    public string KepNev { get; set; } = null!;

    public int UgyintezoId { get; set; }

    public virtual Ugyintezo Ugyintezo { get; set; } = null!;
}
