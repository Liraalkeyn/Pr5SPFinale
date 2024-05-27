using System;
using System.Collections.Generic;

namespace Pr5SP.Models;

public partial class AuditViewModel
{
    public int ResponsibleEmployeeId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Patronymic { get; set; }

    public string TelephoneNumber { get; set; }
    
    public Guid Genuuid { get; set; }

}
