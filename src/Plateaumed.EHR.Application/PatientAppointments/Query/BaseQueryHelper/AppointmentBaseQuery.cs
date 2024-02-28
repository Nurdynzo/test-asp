using System.Collections.Generic;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Organizations;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Staff;

namespace Plateaumed.EHR.PatientAppointments.Query.BaseQueryHelper
{
    /// <summary>
    /// AppointmentBaseQuery class
    /// </summary>
    public class AppointmentBaseQuery
    {
        public PatientAppointment Appointment { init; get; }
        public Patient Patient { init; get; }
        public PatientCodeMapping PatientCodeMapping { init; get; }
        public OrganizationUnitExtended AttendingClinic { init; get; }
        public OrganizationUnitExtended ReferringClinic { init; get; }
        public StaffMember AttendingPhysician { init; get; }
        public PatientReferralDocument ReferralDocument { init; get; }
        public User StaffUser { get; init; }
        
        public PatientScanDocument PatientScanDocument { init; get; }
        public IEnumerable<string> Roles { init; get; }
    }
}
