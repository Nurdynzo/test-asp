using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Invoices.Abstraction;
using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.PatientWallet.Dtos.WalletRefund;

namespace Plateaumed.EHR.Invoices.Query;

public class GetCancelRequestForApprovalQueryHandler : IGetCancelRequestForApprovalQueryHandler
{
    private readonly IRepository<InvoiceCancelRequest,long> _invoiceCancelRequestRepository;

    public GetCancelRequestForApprovalQueryHandler(IRepository<InvoiceCancelRequest, long> invoiceCancelRequestRepository)
    {
        _invoiceCancelRequestRepository = invoiceCancelRequestRepository;
    }

    public async Task<List<GetInvoiceForCancelQueryResponse>> Handle(long patientId, long facilityId)
    {
        var query = await _invoiceCancelRequestRepository.GetAll()
            .Include(x => x.Invoice)
            .Include(x => x.InvoiceItem)
            .Where(x => x.PatientId == patientId
                        && x.FacilityId == facilityId
                        && x.Status == InvoiceCancelStatus.Pending)
            .GroupBy(x => x.InvoiceId)
            .Select(x => new GetInvoiceForCancelQueryResponse
            {
                InvoiceNo = x.FirstOrDefault().Invoice.InvoiceId,
                CreatedDate = x.FirstOrDefault().Invoice.CreationTime,
                InvoiceItems = x.Select(i => new CancelInvoiceItemsQueryResponse
                {
                    Id = i.InvoiceItem.Id,
                    SubTotal = new MoneyDto
                    {
                        Amount = i.InvoiceItem.SubTotal.Amount,
                        Currency = i.InvoiceItem.SubTotal.Currency
                    },
                    ItemName = i.InvoiceItem.Name
                }).ToArray()
            }).ToListAsync();
        return query;
    }
}