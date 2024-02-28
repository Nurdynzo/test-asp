using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class GetFacilityGroupForEditOutput
    {
        public CreateOrEditFacilityGroupDto FacilityGroup { get; set; }
    }
}
