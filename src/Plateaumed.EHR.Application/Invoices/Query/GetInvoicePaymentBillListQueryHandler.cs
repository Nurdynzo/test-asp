using System;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Invoices.Dtos;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Invoices.Abstraction;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Utility;

namespace Plateaumed.EHR.Invoices.Query;

/// <inheritdoc />
public class GetInvoicePaymentBillListQueryHandler : IGetInvoicePaymentBillListQueryHandler
{
    private readonly IRepository<Invoice,long> _invoiceRepository;
    private readonly IRepository<InvoiceItem, long> _invoiceItemRepository;
    private readonly IRepository<User , long> _userRepository;
    private readonly IRepository<PatientAppointment, long> _patientAppointmentRepository;

    /// <summary>
    /// Constructor for GetInvoicePaymentBillListQueryHandler
    /// </summary>
    /// <param name="invoiceRepository"></param>
    /// <param name="invoiceItemRepository"></param>
    /// <param name="userRepository"></param>
    /// <param name="patientAppointmentRepository"></param>
    public GetInvoicePaymentBillListQueryHandler(IRepository<Invoice, long> invoiceRepository, 
        IRepository<InvoiceItem, long> invoiceItemRepository, 
        IRepository<User, long> userRepository,
        IRepository<PatientAppointment, long> patientAppointmentRepository)
    {
        _invoiceRepository = invoiceRepository;
        _invoiceItemRepository = invoiceItemRepository;
        _userRepository = userRepository;
        _patientAppointmentRepository = patientAppointmentRepository;
        
    }

    /// <inheritdoc />
    public async Task<InvoiceReceiptQueryResponse> Handle(GetInvoicePaymentBillListQueryRequest request)
    {
        var query = (from i in _invoiceRepository.GetAll().AsNoTracking()
                join u in _userRepository.GetAll() on i.CreatorUserId equals u.Id into uu
                from u in uu.DefaultIfEmpty()
                join a in _patientAppointmentRepository.GetAll() on i.PatientAppointmentId equals a.Id
                where i.PatientId == request.PatientId
                orderby i.CreationTime descending
                select new GetMostRecentBillResponse
                {
                    Id = i.Id,
                    IssuedOn = i.CreationTime,
                    PaymentStatus = i.PaymentStatus.ToString(),
                    IssuedBy = u != null ? u.FullName : "",
                    TotalAmount = i.TotalAmount.ToMoneyDto(),
                    InvoiceNo = i.InvoiceId,
                    Notes = $"Created for {a.Title} appointment on {a.CreationTime:D}",
                    Items = _invoiceItemRepository.GetAll().Where(x => x.InvoiceId == i.Id)
                        .Select(x => new MostRecentBillItems
                        {
                            Name = x.Name,
                            Quantity = x.Quantity,
                            SubTotal = x.SubTotal.ToMoneyDto(),
                            AmountPaid = x.AmountPaid.ToMoneyDto(),
                            OutstandingAmount = x.OutstandingAmount.ToMoneyDto(),
                            Notes = x.Notes
                        }).ToList()
                }

            ).Take(request.MaxResultCount).Skip(request.SkipCount);
       
       
        var result = await query.ToListAsync();
        var invoiceList =  result.Where(x=> (PaymentStatus)Enum.Parse(typeof(PaymentStatus),x.PaymentStatus) is PaymentStatus.Unpaid ||  x.Items.Any(a => a.AmountPaid.Amount == 0));
        var receiptList = result.Where(x => (PaymentStatus)Enum.Parse(typeof(PaymentStatus),x.PaymentStatus) is PaymentStatus.Paid || x.Items.Any(a => a.AmountPaid.Amount > 0)); 
        var invoiceReceiptList = new InvoiceReceiptQueryResponse
        {
            InvoiceItems = invoiceList.ToList(),
            ReceiptItems = receiptList.ToList()
        };
        return invoiceReceiptList;
    }
}
