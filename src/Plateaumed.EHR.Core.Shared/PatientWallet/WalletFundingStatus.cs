using NpgsqlTypes;
using System.Runtime.Serialization;

namespace Plateaumed.EHR.PatientWallet
{
    public enum WalletFundingStatus
    {
        [PgName("Pending"), EnumMember(Value = "Pending")]
        Pending,

        [PgName("Approved"), EnumMember(Value = "Approved")]
        Approved,

        [PgName("Rejected"), EnumMember(Value = "Rejected")]
        Rejected,
    }
}
