using Abp.Configuration;

namespace Plateaumed.EHR.Timing.Dto
{
    public class GetTimezonesInput
    {
        public SettingScopes DefaultTimezoneScope { get; set; }
    }
}
