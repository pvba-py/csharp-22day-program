using System;
using System.Collections.Generic;

namespace CareBridge.EFCoreDemo.Models.Generated;

public partial class ViewClinical
{
    public int PatientId { get; set; }

    public string FullName { get; set; } = null!;

    public int EncounterId { get; set; }

    public string EncounterType { get; set; } = null!;

    public DateTime AdmitDate { get; set; }

    public DateTime? DischargeDate { get; set; }

    public string Description { get; set; } = null!;

    public DateTime DiagnosedOn { get; set; }
}
