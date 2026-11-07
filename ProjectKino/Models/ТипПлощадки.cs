using System;
using System.Collections.Generic;

namespace ProjectKino.Models;

public partial class ТипПлощадки
{
    public int КодТипаПлощаки { get; set; }

    public string Наименование { get; set; } = null!;

    public virtual ICollection<СъёмочнаяПлощадка> СъёмочнаяПлощадкаs { get; set; } = new List<СъёмочнаяПлощадка>();
}
