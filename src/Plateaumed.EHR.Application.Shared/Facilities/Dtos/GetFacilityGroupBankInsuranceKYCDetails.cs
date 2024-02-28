using Abp.Application.Services.Dto;
using System.Collections.Generic;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class GetFacilityGroupBankInsuranceKYCDetails : EntityDto<long?>
    {
        public List<GetFacilityBankInsuranceKYCDetails> ChildFacilities { get; set; }
    }
}
