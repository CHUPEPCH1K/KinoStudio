using System;
using System.Collections.Generic;

namespace ProjectKino.Models;

public partial class Сотрудник
{
    public int ТабельныйНомер { get; set; }

    public string Фамилия { get; set; } = null!;

    public string Имя { get; set; } = null!;

    public int Специализация { get; set; }

    public int КонтактныеДанные { get; set; }

    public int Ставка { get; set; }

    public string? Фото { get; set; }

    public virtual КонтактныеДанные КонтактныеДанныеNavigation { get; set; } = null!;

    public virtual ICollection<СоставСъёмочнойГруппы> СоставСъёмочнойГруппыs { get; set; } = new List<СоставСъёмочнойГруппы>();

    public virtual Специализация СпециализацияNavigation { get; set; } = null!;
}
