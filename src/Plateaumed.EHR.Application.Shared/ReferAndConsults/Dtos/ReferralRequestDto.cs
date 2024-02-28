namespace Plateaumed.EHR.ReferAndConsults.Dtos;

public class ReferralRequestDto
{
    public long EncounterId { get; set; }
    public string ReceivingHospital { get; set; }
    public string ReceivingUnit { get; set; }
    public string ReceivingConsultant { get; set; }
}
