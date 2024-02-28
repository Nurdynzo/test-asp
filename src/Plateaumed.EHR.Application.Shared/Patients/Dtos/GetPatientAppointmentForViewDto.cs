namespace Plateaumed.EHR.Patients.Dtos
{
    public class GetPatientAppointmentForViewDto
    {
        public PatientAppointmentDto PatientAppointment { get; set; }

        public string PatientPatientCode { get; set; }

        public string PatientReferralReferringHospital { get; set; }

        public string StaffMemberStaffCode { get; set; }

        public string OrganizationUnitDisplayName { get; set; }

        public string OrganizationUnitDisplayName2 { get; set; }

    }
}