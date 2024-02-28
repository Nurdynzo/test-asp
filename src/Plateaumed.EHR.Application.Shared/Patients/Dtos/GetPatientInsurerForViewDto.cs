namespace Plateaumed.EHR.Patients.Dtos
{
    public class GetPatientInsurerForViewDto
    {
        public PatientInsurerDto PatientInsurer { get; set; }

        public string InsuranceProviderName { get; set; }
    }
}
