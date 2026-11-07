using System;
using System.Collections.Generic;

namespace ProjectKino.Models;

public partial class Проект
{
    public int КодПроекта { get; set; }

    public string Название { get; set; } = null!;

    public int Жанр { get; set; }

    public decimal Бюджет { get; set; }

    public DateOnly ДатаНачалаСъёмок { get; set; }

    public DateOnly ДатаОкончанияСъёмок { get; set; }

    public int Статус { get; set; }

    public int ЦелеваяАудитория { get; set; }

    public int? Площадка { get; set; }

    public int? ГрафикСъёмок { get; set; }

    public int? Затраты { get; set; }

    public virtual ГрафикСъёмок? ГрафикСъёмокNavigation { get; set; }

    public virtual Жанр ЖанрNavigation { get; set; } = null!;

    public virtual Затраты? ЗатратыNavigation { get; set; }

    public virtual СъёмочнаяПлощадкаПроекта? ПлощадкаNavigation { get; set; }

    public virtual ICollection<Прокат> Прокатs { get; set; } = new List<Прокат>();

    public virtual Статус СтатусNavigation { get; set; } = null!;

    public virtual ICollection<СъёмочнаяГруппа> СъёмочнаяГруппаs { get; set; } = new List<СъёмочнаяГруппа>();

    public virtual ICollection<СъёмочнаяПлощадкаПроекта> СъёмочнаяПлощадкаПроектаs { get; set; } = new List<СъёмочнаяПлощадкаПроекта>();

    public virtual ЦелеваяАудитория ЦелеваяАудиторияNavigation { get; set; } = null!;
}
