using Plateaumed.EHR.Authorization.Users;
using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.Patients.Dtos
{
    public class PatientRelationDto : EntityDto<long>
    {
        public Relationship Relationship { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public TitleType Title { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public bool IsGuardian { get; set; }

        public long PatientId { get; set; }
    }
}
