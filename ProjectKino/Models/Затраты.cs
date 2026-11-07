using System;
using System.Collections.Generic;

namespace ProjectKino.Models;

public partial class Затраты
{
    public int КодЗатраты { get; set; }

    public decimal? Зарплаты { get; set; }

    public decimal? Аренда { get; set; }

    public decimal? Оборудование { get; set; }

    public decimal? Постобработка { get; set; }

    public DateOnly? Дата { get; set; }

    public virtual ICollection<Проект> Проектs { get; set; } = new List<Проект>();
}
