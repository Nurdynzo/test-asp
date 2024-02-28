using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class GetWardBedForEditOutput
    {
        public CreateOrEditWardBedDto WardBed { get; set; }

        public string BedTypeName { get; set; }

        public string WardName { get; set; }
    }
}
