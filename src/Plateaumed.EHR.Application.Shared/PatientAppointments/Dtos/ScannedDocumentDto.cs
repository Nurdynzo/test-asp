namespace Plateaumed.EHR.PatientAppointments.Dtos;

public record ScannedDocumentDto
{
    public long Id { get; set; }
    public bool? IsApproved { get; set; }
    public long? ReviewerId { get; set; }
}