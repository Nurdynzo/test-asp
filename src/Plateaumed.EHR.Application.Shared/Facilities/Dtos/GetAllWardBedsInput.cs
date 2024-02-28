using Abp.Application.Services.Dto;
using System;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class GetAllWardBedsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public int? IsActiveFilter { get; set; }

        public string BedTypeNameFilter { get; set; }

        public string WardNameFilter { get; set; }
    }
}
