using System;

namespace Plateaumed.EHR.PatientAppointments.Dtos;

public record ReferralDocumentDto
{
    public long Id { get; set; }
    public string ReferringHealthCareProvider { get; set; }
    public string DiagnosisSummary { get; set; }

    //File
    public Guid? ReferralDocument { get; set; } 
}