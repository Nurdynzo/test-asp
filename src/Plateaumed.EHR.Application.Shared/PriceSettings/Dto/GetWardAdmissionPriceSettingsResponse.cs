using System;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Invoices;
namespace Plateaumed.EHR.PriceSettings.Dto
{
    public class GetWardAdmissionPriceSettingsResponse : EntityDto<long>
    {
        public int DefaultInitialPeriodValue { get; set; }
        public PriceTimeFrequency DefaultInitialPeriodUnit { get; set; }
        public PriceTimeFrequency DefaultContinuePeriodUnit { get; set; }
        public int DefaultContinuePeriodValue { get; set; }
        public TimeOnly AdmissionChargeTime { get; set; }
        public decimal FollowUpVisitPercentage { get; set; }
        public long FacilityId { get; set; }
    }
}
