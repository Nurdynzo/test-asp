using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.MultiTenancy.Dtos
{
    public class GetTenantDocumentForEditOutput
    {
        public CreateOrEditTenantDocumentDto TenantDocument { get; set; }

        public string DocumentFileName { get; set; }
    }
}
