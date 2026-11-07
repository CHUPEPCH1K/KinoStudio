using System;
using System.Collections.Generic;

namespace ProjectKino.Models;

public partial class СъёмочнаяПлощадкаПроекта
{
    public int КодСъёмочнойПлощадкиПроекта { get; set; }

    public int СъёмочнаяПлощадка { get; set; }

    public int Проект { get; set; }

    public virtual ICollection<ГрафикСъёмок> ГрафикСъёмокs { get; set; } = new List<ГрафикСъёмок>();

    public virtual Проект ПроектNavigation { get; set; } = null!;

    public virtual ICollection<Проект> Проектs { get; set; } = new List<Проект>();

    public virtual СъёмочнаяПлощадка СъёмочнаяПлощадкаNavigation { get; set; } = null!;
}
