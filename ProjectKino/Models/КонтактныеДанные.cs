using System;
using System.Collections.Generic;

namespace ProjectKino.Models;

public partial class КонтактныеДанные
{
    public int КодКонтактныхДанных { get; set; }

    public string Телефон { get; set; } = null!;

    public string Почта { get; set; } = null!;

    public string Логин { get; set; } = null!;

    public string Пароль { get; set; } = null!;

    public virtual ICollection<Сотрудник> Сотрудникs { get; set; } = new List<Сотрудник>();
}
