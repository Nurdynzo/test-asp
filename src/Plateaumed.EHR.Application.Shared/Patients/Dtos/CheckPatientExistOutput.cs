using System;
using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.Patients.Dtos
{
    public class CheckPatientExistOutput : EntityDto<long>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string PatientCode { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}
