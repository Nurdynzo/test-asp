using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class GetFacilityInsurersForEditOutput
    {
        public List<CreateOrEditFacilityInsurerDto> FacilityInsurers { get; set; }

        public string FacilityGroupName { get; set; }

        public string FacilityName { get; set; }

        //public string InsuranceProviderName { get; set; }

    }
}