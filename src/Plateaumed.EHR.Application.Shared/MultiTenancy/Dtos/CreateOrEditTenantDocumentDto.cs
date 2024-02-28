using Plateaumed.EHR.MultiTenancy;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.MultiTenancy.Dtos
{
    public class CreateOrEditTenantDocumentDto : EntityDto<long?>
    {
        public TenantDocumentType Type { get; set; }

        public Guid? Document { get; set; }

        public string DocumentToken { get; set; }

        [StringLength(
            TenantDocumentConsts.MaxFileNameLength,
            MinimumLength = TenantDocumentConsts.MinFileNameLength
        )]
        public string FileName { get; set; }
    }
}
