

using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.Patients.Dtos
{
    public class NextOfKinDto : EntityDto<long>
    {

        public string FullName { get; set; }

        public Relationship Relationship { get; set; }

        public string PhoneNumber { get; set; }
    }
}
