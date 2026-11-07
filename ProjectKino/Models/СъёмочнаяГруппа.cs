using System;
using System.Collections.Generic;

namespace ProjectKino.Models;

public partial class СъёмочнаяГруппа
{
    public int КодСъёмочнойГруппы { get; set; }

    public int ПроектГруппы { get; set; }

    public virtual ICollection<ГрафикСъёмок> ГрафикСъёмокs { get; set; } = new List<ГрафикСъёмок>();

    public virtual Проект ПроектГруппыNavigation { get; set; } = null!;

    public virtual ICollection<СоставСъёмочнойГруппы> СоставСъёмочнойГруппыs { get; set; } = new List<СоставСъёмочнойГруппы>();
}
