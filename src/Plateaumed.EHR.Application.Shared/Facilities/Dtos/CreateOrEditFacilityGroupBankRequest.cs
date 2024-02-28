using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Plateaumed.EHR.Misc;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class CreateOrEditFacilityGroupBankRequest : EntityDto<long?>
    {
        public List<CreateOrEditBankRequest> ChildFacilities { get; set; }
    }
}
