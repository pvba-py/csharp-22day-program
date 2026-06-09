using System;
using System.Collections.Generic;

namespace CareBridge.PerformanceLab.Models;

public partial class ViewAnalytic
{
    public string AgeBand { get; set; } = null!;

    public string EncounterType { get; set; } = null!;

    public int DepartmentId { get; set; }
}
