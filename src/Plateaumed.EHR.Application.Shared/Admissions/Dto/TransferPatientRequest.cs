using Plateaumed.EHR.Misc;

namespace Plateaumed.EHR.Admissions.Dto;

public class TransferPatientRequest
{
    public long EncounterId { get; set; }
    public long PatientId { get; set; }
    public long WardId { get; set; }
    public long? WardBedId { set; get; }
    public PatientStabilityStatus Status { get; set; }
}