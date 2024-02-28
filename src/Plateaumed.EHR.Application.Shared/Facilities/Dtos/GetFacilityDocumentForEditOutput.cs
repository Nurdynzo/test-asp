using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class GetFacilityDocumentForEditOutput
    {
        public CreateOrEditFacilityDocumentDto FacilityDocument { get; set; }

        public string FacilityGroup { get; set; }

        public string Facility { get; set; }

        public string DocumentFileName { get; set; }
    }
}
