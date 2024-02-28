using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NpgsqlTypes;

namespace Plateaumed.EHR.Invoices.Dtos;

[JsonConverter(typeof(StringEnumConverter))]
public enum PatientSeenFilter
{
    [PgName("Today"), EnumMember(Value = "Today")]
    Today,
    [PgName("ThisWeek"), EnumMember(Value = "ThisWeek")]
    ThisWeek,
    [PgName("ThisMonth"), EnumMember(Value = "ThisMonth")]
    ThisMonth,
    [PgName("ThisYear"), EnumMember(Value = "ThisYear")]
    ThisYear
}