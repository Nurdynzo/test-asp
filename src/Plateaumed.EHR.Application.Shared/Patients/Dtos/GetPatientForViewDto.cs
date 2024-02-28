namespace Plateaumed.EHR.Patients.Dtos
{
    public class GetPatientForViewDto
    {
        public PatientDto Patient { get; set; }

        public string CountryName { get; set; }

        public string PatientOccupationName { get; set; }

        public string PatientOccupationCategoryName { get; set; }
    }
}
