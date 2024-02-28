using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NpgsqlTypes;

namespace Plateaumed.EHR.PatientWallet.Dtos.WalletRefund;

[JsonConverter(typeof(StringEnumConverter))]
public enum WalletRefundFilter
{
    [PgName("Today"), EnumMember(Value = "Today")]
    Today,
    [PgName("Yesterday"), EnumMember(Value = "Yesterday")]
    Yesterday,
    [PgName("ThisWeek"), EnumMember(Value = "ThisWeek")]
    ThisWeek,
    [PgName("LastWeek"), EnumMember(Value = "LastWeek")]
    LastWeek,
    [PgName("ThisMonth"), EnumMember(Value = "ThisMonth")]
    ThisMonth,
    [PgName("LastMonth"), EnumMember(Value = "LastMonth")]
    LastMonth,
    [PgName("ThisYear"), EnumMember(Value = "ThisYear")]
    ThisYear,
    [PgName("LastYear"), EnumMember(Value = "LastYear")]
    LastYear
    
}