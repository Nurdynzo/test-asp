using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class GetFacilityGroupBankDetailsForEditOutput
    {
        public CreateOrEditFacilityGroupBankRequest FacilityGroup { get; set; }
    }
}
