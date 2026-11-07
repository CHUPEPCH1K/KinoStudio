using System;
using System.Collections.Generic;

namespace ProjectKino.Models;

public partial class Прокат
{
    public int КодПроката { get; set; }

    public DateOnly ДатаВыхода { get; set; }

    public int КоличествоКинотеатров { get; set; }

    public decimal Сборы { get; set; }

    public decimal РейтингЗрителей { get; set; }

    public int Проект { get; set; }

    public virtual Проект ПроектNavigation { get; set; } = null!;
}
