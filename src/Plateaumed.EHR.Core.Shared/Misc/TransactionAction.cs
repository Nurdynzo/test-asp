using System.Runtime.Serialization;
using NpgsqlTypes;

namespace Plateaumed.EHR.Misc
{
    public enum TransactionAction
    {
        [PgName("FundWallet"), EnumMember(Value = "FundWallet")]
        FundWallet,
        [PgName("PaidInvoice"), EnumMember(Value = "PaidInvoice")]
        PaidInvoice,
        [PgName("CreateInvoice"), EnumMember(Value = "CreateInvoice")]
        CreateInvoice,
        [PgName("CreateProforma"), EnumMember(Value = "CreateProforma")]
        CreateProforma,
        [PgName("ApproveWalletFunding"), EnumMember(Value = "ApproveWalletFunding")]
        ApproveWalletFunding,
        [PgName("RejectWalletFunding"), EnumMember(Value = "RejectWalletFunding")]
        RejectWalletFunding,
        [PgName("CancelledInvoiceItem"), EnumMember(Value = "CancelledInvoiceItem")]
        CancelledInvoiceItem,
        [PgName("ClearInvoice"), EnumMember(Value = "ClearInvoice")]
        ClearInvoice,
        [PgName("EditInvoice"), EnumMember(Value = "EditInvoice")]
        EditInvoice,
        [PgName("RefundAmount"), EnumMember(Value = "RefundAmount")]
        RefundAmount,
        [PgName("ApproveRefund"), EnumMember(Value = "ApproveRefund")]
        ApproveRefund,
        [PgName("RejectRefund"), EnumMember(Value = "RejectRefund")]
        RejectRefund,
        [PgName("RequestWalletFunding"), EnumMember(Value = "RequestWalletFunding")]
        RequestWalletFunding,
        [PgName("PaidInvoiceItem"), EnumMember(Value = "PaidInvoiceItem")]
        PaidInvoiceItem,
        [PgName("DebtRelief"), EnumMember(Value = "DebtRelief")]
        DebtRelief
    }
}