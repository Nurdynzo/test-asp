using System.Runtime.Serialization;
using NpgsqlTypes;

namespace Plateaumed.EHR.Misc
{
    public enum InvoiceType
    {
        [PgName("InvoiceCreation"), EnumMember(Value = "InvoiceCreation")]
        InvoiceCreation,
        [PgName("Proforma"), EnumMember(Value = "Proforma")]
        Proforma,
        
    }
}