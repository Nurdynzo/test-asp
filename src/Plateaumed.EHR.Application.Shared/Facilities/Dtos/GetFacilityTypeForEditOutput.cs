using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class GetFacilityTypeForEditOutput
    {
        public CreateOrEditFacilityTypeDto FacilityType { get; set; }
    }
}
