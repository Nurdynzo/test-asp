using Abp.Application.Services.Dto;
using Plateaumed.EHR.Misc;

namespace Plateaumed.EHR.PatientProfile.Dto
{
    public class GetAlcoholHistoryResponseDto : EntityDto<long>
    {
        public long PatientId { get; set; }
        public int Frequency { get; set; }
        public UnitOfTime Interval { get; set; }
        public string TypeOfAlcohol { get; set; }
        public string MaximumUnitTaken { get; set; }
        public string MaximumAmountOfUnits { get; set; }
        public string Note { get; set; }
        public bool DetailsOfAlcoholIntakeNotKnown { get; set; }
        public bool DoesNotTakeAlcohol { get; set; }
    }
}
