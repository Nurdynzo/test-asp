namespace Plateaumed.EHR.Patients.Dtos
{
    public class GetPatientDetailsInput
    {
        public long PatientId { get; set; }

        public long? EncounterId { get; set; }
    }
}