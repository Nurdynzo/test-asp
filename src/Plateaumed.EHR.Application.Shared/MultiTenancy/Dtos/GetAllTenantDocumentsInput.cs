using Abp.Application.Services.Dto;
using System;

namespace Plateaumed.EHR.MultiTenancy.Dtos
{
    public class GetAllTenantDocumentsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
