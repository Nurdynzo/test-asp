using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.PatientWallet.Abstractions;
using Plateaumed.EHR.PatientWallet.Dtos.WalletRefund;

namespace Plateaumed.EHR.PatientWallet.Query;

public class GetInvoicesForRefundQueryHandler : IGetInvoicesForRefundQueryHandler
{
    private readonly IRepository<Invoice,long> _invoiceRepository;

    public GetInvoicesForRefundQueryHandler(IRepository<Invoice, long> invoiceRepository)
    {
        _invoiceRepository = invoiceRepository;
    }

    public async Task<List<GetInvoicesForRefundQueryResponse>> Handle(GetInvoicesForRefundQueryRequest request)
    {
        var query = _invoiceRepository
            .GetAll()
            .Where(x=>x.PatientId == request.PatientId && x.PaymentStatus == PaymentStatus.Paid);

        query = request switch
        {
            { Filter: WalletRefundFilter.Today } => query.Where(x => x.CreationTime == DateTime.Today),
            { Filter: WalletRefundFilter.Yesterday } => query.Where(x => x.CreationTime >= DateTime.Today.AddDays(-1)),
            { Filter: WalletRefundFilter.ThisWeek } => query.Where(x => x.CreationTime >= DateTime.Today.AddDays(-7)),
            { Filter: WalletRefundFilter.LastWeek } => query.Where(x => x.CreationTime >= DateTime.Today.AddDays(-14)),
            { Filter: WalletRefundFilter.ThisMonth } => query.Where(x => x.CreationTime >= DateTime.Today.AddDays(-30)),
            { Filter: WalletRefundFilter.LastMonth } => query.Where(x => x.CreationTime >= DateTime.Today.AddDays(-60)),
            { Filter: WalletRefundFilter.ThisYear } => query.Where(x => x.CreationTime >= DateTime.Today.AddDays(-365)),
            { Filter: WalletRefundFilter.LastYear } => query.Where(x => x.CreationTime >= DateTime.Today.AddDays(-730)),
            { CustomDate: not null } => query.Where(x => x.CreationTime >= request.CustomDate ),
            _ => query
        };
        var result = query.Select(x => new GetInvoicesForRefundQueryResponse()
        {
            Id = x.Id,
            InvoiceNo = x.InvoiceId

        });
        return await result.ToListAsync();
    }
}