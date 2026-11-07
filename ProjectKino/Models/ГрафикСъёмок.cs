using System;
using System.Collections.Generic;

namespace ProjectKino.Models;

public partial class ГрафикСъёмок
{
    public int КодГрафикаСъёмок { get; set; }

    public DateOnly Дата { get; set; }

    public TimeOnly Время { get; set; }

    public int Площадка { get; set; }

    public string Сцена { get; set; } = null!;

    public int СъёмочнаяГруппа { get; set; }

    public virtual СъёмочнаяПлощадкаПроекта ПлощадкаNavigation { get; set; } = null!;

    public virtual ICollection<Проект> Проектs { get; set; } = new List<Проект>();

    public virtual СъёмочнаяГруппа СъёмочнаяГруппаNavigation { get; set; } = null!;
}
