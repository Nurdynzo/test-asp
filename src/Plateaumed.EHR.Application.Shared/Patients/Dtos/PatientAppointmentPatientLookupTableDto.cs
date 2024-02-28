using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.Patients.Dtos
{
    public class PatientAppointmentPatientLookupTableDto
    {
        public long Id { get; set; }

        public string DisplayName { get; set; }
    }
}