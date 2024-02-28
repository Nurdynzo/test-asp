using System;
using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class FacilityDocumentDto : EntityDto<long>
    {
        public string FileName { get; set; }

        public string DocumentType { get; set; }

        public Guid? Document { get; set; }

        public string DocumentFileName { get; set; }

        public long? FacilityGroupId { get; set; }

        public long? FacilityId { get; set; }
    }
}
