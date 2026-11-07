using System;
using System.Collections.Generic;

namespace ProjectKino.Models;

public partial class Специализация
{
    public int КодСпециализации { get; set; }

    public string Наименование { get; set; } = null!;

    public virtual ICollection<Сотрудник> Сотрудникs { get; set; } = new List<Сотрудник>();
}
