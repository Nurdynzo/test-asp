namespace Plateaumed.EHR.Patients.Dtos
{
    public class AllPatientInClinicRequest
    {
        public string SearchText { get; set; }
        public ViewAllPatientInClinicSortFilter? SortFilter{ get; set; }
    }
}
