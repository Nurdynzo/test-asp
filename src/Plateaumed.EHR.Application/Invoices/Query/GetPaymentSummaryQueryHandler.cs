using System;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Plateaumed.EHR.Invoices.Dtos;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Application.Services.Dto;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Invoices.Abstraction;
using Plateaumed.EHR.Patients;

namespace Plateaumed.EHR.Invoices.Query;

/// <inheritdoc />
public class GetPaymentSummaryQueryHandler : IGetPaymentSummaryQueryHandler
{
    private readonly IRepository<Invoice,long> _invoiceRepository;
    private readonly IRepository<InvoiceItem, long> _invoiceItemRepository;
    private readonly IRepository<PatientAppointment,long> _patientAppointmentRepository;
    private readonly IRepository<PaymentActivityLog,long> _paymentActivityLogRepository;

    /// <summary>
    /// Constructor for GetPaymentSummaryQueryHandler
    /// </summary>
    /// <param name="invoiceRepository"></param>
    /// <param name="invoiceItemRepository"></param>
    /// <param name="patientAppointmentRepository"></param>
    /// <param name="paymentActivityLogRepository"></param>
    public GetPaymentSummaryQueryHandler(IRepository<Invoice, long> invoiceRepository,
        IRepository<InvoiceItem, long> invoiceItemRepository, 
        IRepository<PatientAppointment, long> patientAppointmentRepository, 
        IRepository<PaymentActivityLog, long> paymentActivityLogRepository)
    {
        _invoiceItemRepository = invoiceItemRepository;
        _patientAppointmentRepository = patientAppointmentRepository;
        _paymentActivityLogRepository = paymentActivityLogRepository;
        _invoiceRepository = invoiceRepository;
    }

    /// <inheritdoc />
    public async Task<PagedResultDto<GetPaymentSummaryQueryResponse>> Handle(GetPaymentSummaryQueryRequest request)
    {
        string filter = string.IsNullOrEmpty(request.Filter) ? String.Empty : request.Filter.ToLower();

        var query = (from a in _paymentActivityLogRepository.GetAll()
                join i in _invoiceRepository.GetAll() on a.InvoiceId equals i.Id into ii
                from i in ii.DefaultIfEmpty()
                join p in _patientAppointmentRepository.GetAll() on i.PatientAppointmentId equals p.Id into ap
                from p in ap.DefaultIfEmpty()
                join l in _invoiceItemRepository.GetAll() on a.InvoiceItemId equals l.Id into it
                from l in it.DefaultIfEmpty()
                where i.PatientId == request.PatientId
                orderby a.CreationTime descending
                select new GetPaymentSummaryQueryResponse
                {
                    Id = a.Id,
                    Items = l != null ? l.Name : string.Empty,
                    InvoiceNo = i != null ? i.InvoiceId : string.Empty,
                    Amount = new MoneyDto
                    {
                        Amount = a.ActualAmount != null ? a.ActualAmount.Amount : 0,
                        Currency = a.ActualAmount != null ? a.ActualAmount.Currency : string.Empty
                    },
                    PaymentType = i != null ? i.PaymentType : null,
                    AmountPaid = new MoneyDto
                    {
                        Amount = a.AmountPaid != null ? a.AmountPaid.Amount : 0,
                        Currency = a.AmountPaid != null ? a.AmountPaid.Currency : string.Empty
                    },
                    OutstandingAmount = new MoneyDto
                    {
                        Amount = a.OutStandingAmount != null ? a.OutStandingAmount.Amount : 0,
                        Currency = a.OutStandingAmount != null ? a.OutStandingAmount.Currency : string.Empty
                    },
                    ToUpAmount = new MoneyDto
                    {
                        Amount = a.ToUpAmount != null ? a.ToUpAmount.Amount : 0,
                        Currency = a.ToUpAmount != null ? a.ToUpAmount.Currency : string.Empty
                    },
                    CreatedDate = a != null ? a.CreationTime : null,
                    AppointmentStatus = p != null ? p.Status : null,
                    PaymentDate = i != null ? i.TimeOfInvoicePaid : null,
                    ItemStatus = l != null ? l.Status : null,
                    ActionStatus =  a != null ? a.TransactionAction: null
                }
            );
            // filter
            query = request switch
            {
                { Amount: > 0 } => query.Where(x =>  x.Amount.Amount == request.Amount),
                { AmountPaid: > 0 } => query.Where(i =>  i.AmountPaid.Amount == request.AmountPaid),
                { OutStandingAmount: > 0 } => query.Where(i =>  i.OutstandingAmount.Amount == request.OutStandingAmount),
                { PaymentType: not null } => query.Where(i =>  i.PaymentType == request.PaymentType),
                { DateFilter: PatientSeenFilter.Today } => query.Where(x=> x.CreatedDate != null  && x.CreatedDate.Value.Date == DateTime.Now.Date),
                { DateFilter: PatientSeenFilter.ThisWeek } => query.Where(x=>x.CreatedDate != null && x.CreatedDate.Value.Date == DateTime.Now.Date.AddDays(-7)),
                { DateFilter: PatientSeenFilter.ThisMonth } => query.Where(x=>x.CreatedDate != null && x.CreatedDate.Value.Date == DateTime.Now.Date.AddDays(-30)),
                { DateFilter: PatientSeenFilter.ThisYear } => query.Where(x=>x.CreatedDate != null && x.CreatedDate.Value.Date == DateTime.Now.Date.AddDays(-365)),
                _ => query
            };
            
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(x => x != null && (x.InvoiceNo.Contains(filter) || x.Items.Contains(filter)));
            }

            query = query.Skip(request.SkipCount).Take(request.MaxResultCount).OrderBy(request.Sorting ?? "CreatedDate desc");
            var count = await query.CountAsync();
            var result = await query.ToListAsync();

            return new PagedResultDto<GetPaymentSummaryQueryResponse>(count, result);

    }
}