using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.Organizations.Dtos
{
    public class GetOrganizationUnitTimeForEditOutput
    {
        public CreateOrEditOrganizationUnitTimeDto OrganizationUnitTime { get; set; }

        public string OrganizationUnitDisplayName { get; set; }

    }
}