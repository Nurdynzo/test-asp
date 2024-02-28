using Plateaumed.EHR.Misc;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.Organizations.Dtos
{
    public class CreateOrEditOrganizationUnitTimeDto : EntityDto<long?>
    {

        public DaysOfTheWeek DayOfTheWeek { get; set; }

        public DateTime? OpeningTime { get; set; }

        public DateTime? ClosingTime { get; set; }

        public bool IsActive { get; set; }

        public long? OrganizationUnitId { get; set; }

    }
}