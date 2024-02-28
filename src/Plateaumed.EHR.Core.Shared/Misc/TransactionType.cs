using System.Runtime.Serialization;
using NpgsqlTypes;

namespace Plateaumed.EHR.Misc
{
    public enum TransactionType
    {
        [PgName("Credit"), EnumMember(Value = "Credit")]
        Credit,
        [PgName("Debit"), EnumMember(Value = "Debit")]
        Debit,
        [PgName("Other"), EnumMember(Value = "Other")]
        Other
        
    }
}