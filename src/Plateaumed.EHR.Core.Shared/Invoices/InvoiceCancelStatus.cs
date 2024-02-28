using System.Runtime.Serialization;
using NpgsqlTypes;

namespace Plateaumed.EHR.Invoices
{
    public enum InvoiceCancelStatus
    {
        [PgName("Pending"), EnumMember(Value = "Pending")]
        Pending,

        [PgName("Approved"), EnumMember(Value = "Approved")]
        Approved,

        [PgName("Rejected"), EnumMember(Value = "Rejected")]
        Rejected,
    }
}