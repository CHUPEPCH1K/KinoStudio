using System;
using System.Collections.Generic;

namespace ProjectKino.Models;

public partial class Статус
{
    public int КодСтатуса { get; set; }

    public string Наименование { get; set; } = null!;

    public virtual ICollection<Проект> Проектs { get; set; } = new List<Проект>();
}
