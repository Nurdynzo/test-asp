using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NpgsqlTypes;
namespace Plateaumed.EHR.Invoices
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PriceTimeFrequency
    {
        [PgName("Day"), EnumMember(Value = "Day")]
        Day,
        [PgName("Week"), EnumMember(Value = "Week")]
        Week,
        [PgName("Visit"), EnumMember(Value = "Visit")]
        Visit,
    }
}
