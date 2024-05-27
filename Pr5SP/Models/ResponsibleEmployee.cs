using System;
using System.Collections.Generic;

namespace Pr5SP.Models;

public partial class ResponsibleEmployee
{
    public int ResponsibleEmployeeId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Patronymic { get; set; } = null!;

    public string TelephoneNumber { get; set; } = null!;

    public virtual ICollection<Pavilion> Pavilions { get; set; } = new List<Pavilion>();
}
