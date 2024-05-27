using System;
using System.Collections.Generic;

namespace Pr5SP.Models;

public partial class Pavilion
{
    public int PavilionId { get; set; }

    public string NameOfPavilion { get; set; } = null!;

    public int ResponsibleEmployee { get; set; }

    public virtual ICollection<Exhibit> Exhibits { get; set; } = new List<Exhibit>();

    public virtual ResponsibleEmployee ResponsibleEmployeeNavigation { get; set; } = null!;
}
