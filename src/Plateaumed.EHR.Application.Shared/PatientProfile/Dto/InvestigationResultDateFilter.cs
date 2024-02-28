using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace Plateaumed.EHR.PatientProfile.Dto
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum InvestigationResultDateFilter
    {
        Today,
        LastSevenDays,
        LastFourteenDays,
        LastThirtyDays,
        LastNinetyDays,
        LastOneYear,
    }
}