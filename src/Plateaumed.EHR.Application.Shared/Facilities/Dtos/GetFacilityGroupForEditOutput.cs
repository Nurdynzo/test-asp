using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class GetFacilityGroupPatientDetailsForEditOutput
    {
        public CreateOrEditFacilityGroupPatientCodeTemplateDto FacilityGroup { get; set; }
    }
}
