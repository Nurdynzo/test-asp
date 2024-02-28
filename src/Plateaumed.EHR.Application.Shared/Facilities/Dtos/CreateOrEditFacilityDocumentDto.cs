using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class CreateOrEditFacilityDocumentDto : EntityDto<long?>
    {
        [StringLength(FacilityDocumentConsts.MaxFileNameLength, MinimumLength = FacilityDocumentConsts.MinFileNameLength)]
        public string FileName { get; set; }

        [Required]
        [StringLength(FacilityDocumentConsts.MaxDocumentTypeLength, MinimumLength = FacilityDocumentConsts.MinDocumentTypeLength)]
        public string DocumentType { get; set; }

        public Guid? Document { get; set; }

        public string DocumentToken { get; set; }

        public long? FacilityGroupId { get; set; }

        public long? FacilityId { get; set; }
    }
}
