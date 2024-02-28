using Abp.Application.Services.Dto;
using System;

namespace Plateaumed.EHR.Staff.Dtos
{
    public class GetAllStaffCodeTemplatesInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
