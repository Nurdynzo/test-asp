using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Invoices.Abstraction;
using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Utility;
using Plateaumed.EHR.ValueObjects;

namespace Plateaumed.EHR.Invoices.Query;

public class GetPaymentLandingListHeader : IGetPaymentLandingListHeader
{
    private readonly IRepository<Invoice,long> _invoiceRepository;
    private readonly IRepository<InvoiceItem,long> _invoiceItemRepository;
    private readonly IRepository<PaymentActivityLog,long> _paymentActivityLogRepository;

    public GetPaymentLandingListHeader(
        IRepository<Invoice, long> invoiceRepository, 
        IRepository<InvoiceItem, long> invoiceItemRepository,
        IRepository<PaymentActivityLog, long> paymentActivityLog)
    {
        _invoiceRepository = invoiceRepository;
        _invoiceItemRepository = invoiceItemRepository;
        _paymentActivityLogRepository = paymentActivityLog;
    }
    
    public async Task<GetPatientTotalSummaryQueryResponse> Handle( long facilityId)
    {
        var query = await (from p in _invoiceRepository.GetAll().AsNoTracking()
            join i in _invoiceItemRepository.GetAll().AsNoTracking() on p.Id equals i.InvoiceId
            where i.FacilityId == facilityId
            select new
            {
                i.SubTotal,
                i.OutstandingAmount,
                i.AmountPaid,
                i.DiscountPercentage,
                i.Status,
                p.PatientId
            }).ToListAsync();
        
        var topUpQuery = await _paymentActivityLogRepository.GetAll().AsNoTracking()
            .Where(x=>x.TransactionAction == TransactionAction.FundWallet)
            .Select(x=>x.ToUpAmount ?? new Money())
            .ToListAsync();
        
        var totalMoney = query.Select(x=>x.SubTotal ?? new Money())
            .Sum(x => x)?.ToMoneyDto();
        var totalTopUp = topUpQuery.Sum(x => x)?.ToMoneyDto();
        var totalPaid = query.Select(x=> x.AmountPaid ?? new Money())
            .Sum(x => x)?.ToMoneyDto();
        var totalOutstanding = query.Select(x=>x.OutstandingAmount ?? new Money())
            .Sum(x => x)?.ToMoneyDto();
        
        return new GetPatientTotalSummaryQueryResponse
        {
            TotalAmount = totalMoney,
            ToTalPaid = totalPaid,
            TotalOutstanding = totalOutstanding,
            TotalTopUp = totalTopUp,
            ItemsCounts = query.GroupBy(x=>x.PatientId).Count(),
            IsDiscountApplied = query.Any(x=>x.DiscountPercentage>0),
            IsReliefApplied = query.Any(x =>x.Status == InvoiceItemStatus.ReliefApplied)
            
        };


    }
    
    
}