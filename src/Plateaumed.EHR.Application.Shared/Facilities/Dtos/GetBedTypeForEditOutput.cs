using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class GetBedTypeForEditOutput
    {
        public CreateOrEditBedTypeDto BedType { get; set; }
    }
}
