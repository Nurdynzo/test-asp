using Abp.Application.Services.Dto;
using System;

namespace Plateaumed.EHR.Patients.Dtos
{
    public class GetAllPatientAppointmentsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string TitleFilter { get; set; }

        public int? MaxDurationFilter { get; set; }
        public int? MinDurationFilter { get; set; }

        public DateTime? MaxStartTimeFilter { get; set; }
        public DateTime? MinStartTimeFilter { get; set; }

        public int? IsRepeatFilter { get; set; }

        public string NotesFilter { get; set; }

        public int? StatusFilter { get; set; }

        public int? TypeFilter { get; set; }

        public string PatientPatientCodeFilter { get; set; }

        public string PatientReferralReferringHospitalFilter { get; set; }

        public string StaffMemberStaffCodeFilter { get; set; }

        public string OrganizationUnitDisplayNameFilter { get; set; }

        public string OrganizationUnitDisplayName2Filter { get; set; }

    }
}