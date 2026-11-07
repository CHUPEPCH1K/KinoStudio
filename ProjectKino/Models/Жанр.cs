using System;
using System.Collections.Generic;

namespace ProjectKino.Models;

public partial class Жанр
{
    public int КодЖанра { get; set; }

    public string Наименование { get; set; } = null!;

    public virtual ICollection<Проект> Проектs { get; set; } = new List<Проект>();
}
