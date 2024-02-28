using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.PatientWallet;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Invoices.Abstraction;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Utility;

namespace Plateaumed.EHR.Invoices.Query;

public class GetPaymentExpandableQueryHandler : IGetPaymentExpandableQueryHandler
{
    private readonly IRepository<PaymentActivityLog,long> _paymentActivityLogRepository;
    private readonly IRepository<InvoiceItem,long> _invoiceItemRepository;
    private readonly IRepository<Invoice,long> _invoiceRepository;
    private readonly IRepository<WalletHistory,long> _walletHistoryRepository;
    private readonly IRepository<PatientAppointment,long> _patientAppointmentRepository;
    private readonly IRepository<User,long> _userRepository;

    public GetPaymentExpandableQueryHandler(
        IRepository<PaymentActivityLog, long> paymentActivityLogRepository, 
        IRepository<InvoiceItem, long> invoiceItemRepository,
        IRepository<Invoice, long> invoiceRepository,
        IRepository<WalletHistory, long> walletHistoryRepository,
        IRepository<PatientAppointment, long> patientAppointmentRepository, 
        IRepository<User, long> userRepository)
    {
        _paymentActivityLogRepository = paymentActivityLogRepository;
        _invoiceItemRepository = invoiceItemRepository;
        _invoiceRepository = invoiceRepository;
        _walletHistoryRepository = walletHistoryRepository;
        _patientAppointmentRepository = patientAppointmentRepository;
        _userRepository = userRepository;
    }

    public async Task<PagedResultDto<GetPaymentExpandableQueryResponse>> Handle(GetPaymentExpandableQueryRequest request,long facilityId)
    {
        var query = (
                from a in _paymentActivityLogRepository.GetAll().AsNoTracking()
                join i in _invoiceRepository.GetAll() on (a != null ? a.InvoiceId : 0) equals i.Id into ii
                from i in ii.DefaultIfEmpty()
                join it in _invoiceItemRepository.GetAll() on (a != null ? a.InvoiceItemId : 0) equals it.Id into itt
                from it in itt.DefaultIfEmpty()
                join w in _walletHistoryRepository.GetAll() on (a != null ? a.PatientId : 0) equals w.PatientId into ww
                from w in ww.DefaultIfEmpty()
                join ap in _patientAppointmentRepository.GetAll() on (i != null ? i.PatientAppointmentId:0) equals ap.Id into app
                from ap in app.DefaultIfEmpty()
                join u in _userRepository.GetAll() on (a != null ? a.CreatorUserId:0) equals u.Id into uu
                from u in uu.DefaultIfEmpty()
                where a != null &&  a.FacilityId == facilityId && a.PatientId == request.PatientId
                orderby a.CreationTime descending
                select new GetPaymentExpandableQueryResponse
                {
                    AmountPaid   = a != null && a.AmountPaid != null ? a.AmountPaid.ToMoneyDto(): null,
                    AppointmentStatus = ap != null ? ap.Status.ToString(): string.Empty,
                    InvoiceItemName = it != null ? it.Name : string.Empty,
                    InvoiceItemDateTime = it != null ? it.CreationTime : null,
                    OutstandingAmount = it != null && it.OutstandingAmount != null ? it.OutstandingAmount.ToMoneyDto() : null,
                    ActualInvoiceAmount = it != null && it.SubTotal != null ? it.SubTotal.ToMoneyDto() : null,
                    PaymentType = a != null ? a.TransactionAction.ToString(): string.Empty,
                    InvoiceNo = i != null ? i.InvoiceId : string.Empty,
                    LastPaidDateTime = it != null ? it.LastModificationTime : null,
                    TopUpMoney = a != null && a.ToUpAmount != null ? a.ToUpAmount.ToMoneyDto() : null,
                    EditorInfo = u != null ? $"{u.Title} {u.FullName}" : string.Empty,
                    EditedAmount = a != null && a.EditAmount != null ? a.EditAmount.ToMoneyDto() : null
                }

            );
        query = query.Skip(request.SkipCount).Take(request.MaxResultCount);
        var count = await query.CountAsync();
        var items = await  query.ToListAsync();
        return new PagedResultDto<GetPaymentExpandableQueryResponse>(count, items);
    }
}