using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NpgsqlTypes;
using System.Runtime.Serialization;

namespace Plateaumed.EHR.NextAppointments
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DateTypes
    {
        [PgName("Day"), EnumMember(Value = "Day")]
        Day,

        [PgName("Week"), EnumMember(Value = "Week")]
        Week,

        [PgName("Month"), EnumMember(Value = "Month")]
        Month,

        [PgName("Date"), EnumMember(Value = "Date")]
        Date,
    }
}
