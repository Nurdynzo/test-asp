using Plateaumed.EHR.MultiTenancy;

using System;
using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.MultiTenancy.Dtos
{
    public class TenantDocumentDto : EntityDto<long>
    {
        public TenantDocumentType Type { get; set; }

        public Guid? Document { get; set; }

        public string DocumentFileName { get; set; }

        public string FileName { get; set; }
    }
}
