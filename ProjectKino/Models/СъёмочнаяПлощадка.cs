using System;
using System.Collections.Generic;

namespace ProjectKino.Models;

public partial class СъёмочнаяПлощадка
{
    public int КодСъёмочнойПлощадки { get; set; }

    public string Адрес { get; set; } = null!;

    public int ТипПлощадки { get; set; }

    public int СтоимостьАрендыВДень { get; set; }

    public int Вместимость { get; set; }

    public virtual ICollection<СъёмочнаяПлощадкаПроекта> СъёмочнаяПлощадкаПроектаs { get; set; } = new List<СъёмочнаяПлощадкаПроекта>();

    public virtual ТипПлощадки ТипПлощадкиNavigation { get; set; } = null!;
}
