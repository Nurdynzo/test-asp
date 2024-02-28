using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Invoices.Abstraction;
using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.Invoices.Dtos.PatientInvoicesAndWalletTransactionsDtos;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Patients;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Invoices.Query
{
    public class GetPatientInvoicesAndWalletTransactionsQueryHandler : IGetPatientInvoicesAndWalletTransactionsQueryHandler
    {

        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<Invoice, long> _invoiceRepository;
        private readonly IRepository<InvoiceItem, long> _invoiceItemRepository;
        private readonly IRepository<PatientAppointment, long> _patientAppointmentRepository;
        private readonly IRepository<PaymentActivityLog, long> _paymentActivityLogRepository;

        /// <summary>
        /// Constructor for GetPatientsWithUnpaidInvoiceSummaryQueryHandler
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="invoiceRepository"></param>
        /// <param name="invoiceItemRepository"></param>
        /// <param name="paymentActivityLogRepository"></param>
        /// <param name="patientAppointmentRepository"></param>
        public GetPatientInvoicesAndWalletTransactionsQueryHandler(
            IRepository<User, long> userRepository,
            IRepository<Invoice, long> invoiceRepository,
            IRepository<InvoiceItem, long> invoiceItemRepository,
            IRepository<PaymentActivityLog, long> paymentActivityLogRepository,
            IRepository<PatientAppointment, long> patientAppointmentRepository)
        {
            _userRepository = userRepository;
            _invoiceRepository = invoiceRepository;
            _invoiceItemRepository = invoiceItemRepository;
            _paymentActivityLogRepository = paymentActivityLogRepository;
            _patientAppointmentRepository = patientAppointmentRepository;
        }

        public async Task<PagedResultDto<PatientInvoicesAndWalletTransactionsResponse>> Handle(PatientInvoicesAndWalletTransactionsRequest request, long facilityId)
        {

            var query = GetQuery(request, facilityId);

            // Skip && Take
            query = query.Skip(request.SkipCount).Take(request.MaxResultCount);

            var count = await query.CountAsync();
            var result = await query.ToListAsync();


            return new PagedResultDto<PatientInvoicesAndWalletTransactionsResponse>() 
            { 
                TotalCount = count,
                Items = result,
            };
        }


        private IQueryable<PatientInvoicesAndWalletTransactionsResponse> GetQuery(PatientInvoicesAndWalletTransactionsRequest request, long facilityId) {

            var query = (from al in _paymentActivityLogRepository.GetAll()
                         join u in _userRepository.GetAll() on al.CreatorUserId equals u.Id
                         join i in _invoiceRepository.GetAll() on (al != null ? al.InvoiceId : 0) equals i.Id into ii
                         from i in ii.DefaultIfEmpty()
                         join a in _patientAppointmentRepository.GetAll() on (i != null ? i.PatientAppointmentId : 0) equals a.Id into pa
                         from a in pa.DefaultIfEmpty()
                         join it in _invoiceItemRepository.GetAll() on (al != null ? al.InvoiceItemId : 0) equals it.Id into iit
                         from it in iit.DefaultIfEmpty()
                         where al != null && al.PatientId == request.PatientId &&
                         al.TransactionAction != TransactionAction.PaidInvoice && al.FacilityId == facilityId
                         orderby al.CreationTime descending
                         select new PatientInvoicesAndWalletTransactionsResponse
                         {
                             ModifiedBy = u != null && u.Title != null && u.Surname != null ? $"{u.Title} {u.Surname}" : string.Empty,
                             ModifiedAt = al != null ? al.CreationTime : null,
                             InvoiceId = i != null ? i.Id : null,
                             InvoiceItemName = it != null ? it.Name : string.Empty,
                             InvoiceNo = i != null ? i.InvoiceId : string.Empty,
                             CreatedAt = al != null ? al.CreationTime : null,

                             // TODO(POD2): This is subject to change
                             GeneralStatus = a != null ? a.Status.ToString() : string.Empty,
                             InvoiceItemStatus = it != null ? it.Status.ToString() : string.Empty,
                             Type = al != null ? al.TransactionAction.ToString() : string.Empty,
                             EditedAmount = new MoneyDto
                             {
                                 Amount = al != null && al.EditAmount != null ? al.EditAmount.Amount : 0,
                                 Currency = al != null && al.EditAmount != null ? al.EditAmount.Currency : string.Empty
                             },
                             InvoiceAmount = new MoneyDto
                             {
                                 Amount = al != null && al.AmountPaid != null ? al.AmountPaid.Amount : 0,
                                 Currency = al != null && al.AmountPaid != null ? al.AmountPaid.Currency : string.Empty,
                             },
                             TopUpAmount = new MoneyDto
                             {
                                 Amount = al != null && al.ToUpAmount != null ? al.ToUpAmount.Amount : 0,
                                 Currency = al != null && al.ToUpAmount != null ? al.ToUpAmount.Currency : string.Empty
                             },
                         });

            return query.AsNoTracking();
        }
    }
}
