using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NpgsqlTypes;

namespace Plateaumed.EHR.Invoices.Dtos;

[JsonConverter(typeof(StringEnumConverter))]
public enum FilterType
{
    [PgName("Patient_Seen"), EnumMember(Value = "PatientSeen")]
    PatientSeen,
    [PgName("Amount_Paid"), EnumMember(Value = "AmountPaid")]
    AmountPaid,
    [PgName("Wallet_ToUp"), EnumMember(Value = "WalletTopUp")]
    WalletTopUp,
    [PgName("Service_On_Credit"), EnumMember(Value = "ServiceOnCredit")]
    ServiceOnCredit,
    [PgName("Outstanding_Amount"), EnumMember(Value = "OutstandingAmount")]
    OutstandingAmount,
}