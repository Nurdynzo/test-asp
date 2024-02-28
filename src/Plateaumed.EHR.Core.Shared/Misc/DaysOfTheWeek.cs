using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NpgsqlTypes;
using System.Runtime.Serialization;

namespace Plateaumed.EHR.Misc
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DaysOfTheWeek
    {
        [PgName("Sunday"), EnumMember(Value = "Sunday")]
        Sunday,

        [PgName("Monday"), EnumMember(Value = "Monday")]
        Monday,

        [PgName("Tuesday"), EnumMember(Value = "Tuesday")]
        Tuesday,

        [PgName("Wednesday"), EnumMember(Value = "Wednesday")]
        Wednesday,

        [PgName("Thursday"), EnumMember(Value = "Thursday")]
        Thursday,

        [PgName("Friday"), EnumMember(Value = "Friday")]
        Friday,

        [PgName("Saturday"), EnumMember(Value = "Saturday")]
        Saturday,
    }
}
