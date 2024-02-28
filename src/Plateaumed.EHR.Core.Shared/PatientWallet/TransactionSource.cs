using System.Runtime.Serialization;
using NpgsqlTypes;

namespace Plateaumed.EHR.PatientWallet
{
    public enum TransactionSource
    {
        [PgName("Direct"), EnumMember(Value = "Direct")]
        Direct,
        [PgName("Indirect"), EnumMember(Value = "Indirect")]
        Indirect
    }
}