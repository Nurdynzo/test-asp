using System;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Misc;
namespace Plateaumed.EHR.PatientProfile.Dto
{
    public class GetPatientMenstrualFrequencyResponse: EntityDto<long>
    {
        public DateTime LastDayOfPeriod { get; set; }

        public int AveragePeriodDuration { get; set; }

        public bool IsPeriodPredictable { get; set; }

        public int AverageCycleLength { get; set; }

        public UnitOfTime AverageCycleLengthUnit { get; set; }

        public string RequestedTest { get; set; }

        public string Notes { get; set; }

        public long PatientId { get; set; }
    }
}