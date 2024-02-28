using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NpgsqlTypes;
namespace Plateaumed.EHR.Misc
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PricingCategory
    {
        [PgName("Consultation"), EnumMember(Value = "Consultation")]
        Consultation,
        [PgName("Ward_Admission"), EnumMember(Value = "Ward Admission")]
        WardAdmission,
        [PgName("Procedure"), EnumMember(Value = "Procedure")]
        Procedure,
        [PgName("Laboratory"), EnumMember(Value = "Laboratory")]
        Laboratory,
        [PgName("Others"), EnumMember(Value = "Others")]
        Others
    }
}