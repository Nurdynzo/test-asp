using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using NpgsqlTypes;

namespace Plateaumed.EHR.Patients
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AppointmentRepeatType
    {
        [PgName("Daily"), EnumMember(Value = "Daily")]
        Daily,

        [PgName("Weekly"), EnumMember(Value = "Weekly")]
        Weekly,

        [PgName("Weekends"), EnumMember(Value = "Weekends")]
        Weekends,

        [PgName("Weekdays"), EnumMember(Value = "Weekdays")]
        Weekdays,

        [PgName("Monthly"), EnumMember(Value = "Monthly")]
        Monthly,

        [PgName("Annually"), EnumMember(Value = "Annually")]
        Annually,

        [PgName("Custom"), EnumMember(Value = "Custom")]
        Custom
    }
}
