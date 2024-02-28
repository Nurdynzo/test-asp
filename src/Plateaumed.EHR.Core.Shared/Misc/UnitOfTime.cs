using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NpgsqlTypes;

namespace Plateaumed.EHR.Misc
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum UnitOfTime
    {
        [PgName("Day"), EnumMember(Value = "Day")]
        Day,

        [PgName("Week"), EnumMember(Value = "Week")]
        Week,

        [PgName("Month"), EnumMember(Value = "Month")]
        Month,

        [PgName("Year"), EnumMember(Value = "Year")]
        Year
    }
}