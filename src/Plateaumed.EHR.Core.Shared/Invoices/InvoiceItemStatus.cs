using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;
using NpgsqlTypes;

namespace Plateaumed.EHR.Invoices
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum InvoiceItemStatus
    {
        [PgName("Unpaid"), EnumMember(Value = "Unpaid")]
        Unpaid,
        [PgName("AwaitingApproval"), EnumMember(Value = "Awaiting approval")]
        AwaitingApproval,
        [PgName("Paid"), EnumMember(Value = "Paid")]
        Paid,
        [PgName("Cancelled"), EnumMember(Value = "Cancelled")]
        Cancelled,
        [PgName("Approved"), EnumMember(Value = "Approved")]
        Approved,
        [PgName("ReliefApplied"), EnumMember(Value = "ReliefApplied")]
        ReliefApplied,
        [PgName("Refunded"), EnumMember(Value = "Refunded")]
        Refunded,
        [PgName("AwaitCancellationApproval"), EnumMember(Value = "AwaitCancellationApproval")]
        AwaitCancellationApproval
    }
}