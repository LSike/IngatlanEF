using System;
using System.Collections.Generic;

namespace IngatlanEF.Models;

public partial class Ugyintezo
{
    public int Id { get; set; }

    public string Nev { get; set; } = null!;

    public string TelefonSzám { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<Ingatlan> Ingatlans { get; set; } = new List<Ingatlan>();
}
