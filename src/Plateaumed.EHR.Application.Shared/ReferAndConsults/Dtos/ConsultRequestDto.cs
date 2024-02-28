namespace Plateaumed.EHR.ReferAndConsults.Dtos;

public class ConsultRequestDto
{
    public long EncounterId { get; set; }
    public string ReceivingUnit { get; set; }
    public string ReceivingConsultant { get; set; }
}
