using System;
using System.Collections.Generic;

namespace ProjectKino.Models;

public partial class СоставСъёмочнойГруппы
{
    public int КодСоставаСъёмочнойГруппы { get; set; }

    public int Сотрудник { get; set; }

    public int СёмочнаяГруппа { get; set; }

    public string? Роль { get; set; }

    public virtual Сотрудник СотрудникNavigation { get; set; } = null!;

    public virtual СъёмочнаяГруппа СёмочнаяГруппаNavigation { get; set; } = null!;
}
