using Abp.Application.Services.Dto;
using System;

namespace Plateaumed.EHR.PatientProfile.Dto
{
    public class GetPatientImplantResponseDto : EntityDto<long>
    {
        public long PatientId { get; set; }
        public string Name { get; set; }
        public long? SnomedId { get; set; }
        public bool IsIntact { get; set; }
        public bool HasComplications { get; set; }
        public string Note { get; set; }
        public DateTime DateInserted { get; set; }
        public DateTime DateRemoved { get; set; }
        public long CreatorUserId { get; set; }
    }
}
