using System;
using System.Collections.Generic;

namespace Pr5SP.Models;

public partial class Exhibit
{
    public int ExhibitId { get; set; }

    public string NameOfExhibit { get; set; } = null!;

    public int TypeOfExhibit { get; set; }

    public int CountryId { get; set; }

    public DateOnly DateOfArrival { get; set; }

    public int PavilionId { get; set; }

    public DateOnly DiscoveryDate { get; set; }

    public virtual Country Country { get; set; } = null!;

    public virtual Pavilion Pavilion { get; set; } = null!;

    public virtual TypeOfExhibit TypeOfExhibitNavigation { get; set; } = null!;
}
