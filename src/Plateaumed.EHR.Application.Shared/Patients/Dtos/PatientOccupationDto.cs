using System;
using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.Patients.Dtos
{
    public class PatientOccupationDto : EntityDto<long>
    {
        public long OccupationId { get; set; }

        public virtual DateTime? StartDate { get; set; }

        public virtual DateTime? EndDate { get; set; }

        public virtual bool IsCurrent { get; set; }

        public string Location { get; set; }

        public string Notes { get; set; }
    }
}
