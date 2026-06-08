using System;
using System.Collections.Generic;

namespace CareBridge.EFCoreDemo.Models.Generated;

public partial class VwBillingClaim
{
    public int InsuranceId { get; set; }

    public int PatientId { get; set; }

    public string Payer { get; set; } = null!;

    public DateOnly EffectiveDate { get; set; }

    public DateOnly? ExpiryDate { get; set; }
}
