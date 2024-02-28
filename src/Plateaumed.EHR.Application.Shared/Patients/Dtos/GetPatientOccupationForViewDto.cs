namespace Plateaumed.EHR.Patients.Dtos
{
    public class GetPatientOccupationForViewDto
    {
        public PatientOccupationDto PatientOccupation { get; set; }

        public string PatientOccupationCategoryName { get; set; }
    }
}
