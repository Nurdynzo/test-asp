using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.Patients;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Invoices.Abstraction;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Utility;
namespace Plateaumed.EHR.Invoices.Query
{
    public class GetEditedInvoiceForDownloadQueryHandler : IGetEditedInvoiceForDownloadQueryHandler
    {
        private readonly IRepository<PaymentActivityLog,long> _paymentActivityLogRepository;
        private readonly IRepository<Patient,long> _patientRepository;
        private readonly IRepository<Invoice,long> _invoiceRepository;
        private readonly IRepository<InvoiceItem,long> _invoiceItemRepository;
        private readonly IRepository<PatientCodeMapping,long> _patientCodeMappingRepository;
        public GetEditedInvoiceForDownloadQueryHandler(
            IRepository<PaymentActivityLog, long> paymentActivityLogRepository, 
            IRepository<Patient, long> patientRepository, 
            IRepository<Invoice, long> invoiceRepository, 
            IRepository<InvoiceItem, long> invoiceItemRepository, 
            IRepository<PatientCodeMapping, long> patientCodeMappingRepository)
        {
            _paymentActivityLogRepository = paymentActivityLogRepository;
            _patientRepository = patientRepository;
            _invoiceRepository = invoiceRepository;
            _invoiceItemRepository = invoiceItemRepository;
            _patientCodeMappingRepository = patientCodeMappingRepository;
        }
        public async Task<List<GetEditedInvoiceForDownloadResponse>> Handle(GetEditedInvoiceForDownloadRequest request, long facilityId)
        {
            var query = (from a in _paymentActivityLogRepository.GetAll().AsNoTracking()
                         join i in _invoiceRepository.GetAll() on a.InvoiceId equals i.Id
                         join it in _invoiceItemRepository.GetAll() on i.Id equals it.InvoiceId
                         join p in _patientRepository.GetAll() on i.PatientId equals p.Id
                         join pc in _patientCodeMappingRepository.GetAll() on p.Id equals pc.PatientId
                         where a.FacilityId == facilityId && 
                               pc.FacilityId == facilityId && 
                               a.TransactionAction == TransactionAction.EditInvoice
                         select new GetEditedInvoiceForDownloadResponse
                         {
                             Dob = p.DateOfBirth,
                             Gender = p.GenderType,
                             Id = a.Id,
                             Total = i.TotalAmount.ToMoneyDto(),
                             ActualAmount = a.ActualAmount.ToMoneyDto(),
                             PatientCode = pc.PatientCode,
                             FullName = $"{p.FirstName} {p.LastName}",
                             EditedAmount = a.EditAmount.ToMoneyDto(),
                             InvoiceNo = i.InvoiceId,
                             ItemName = it.Name,
                             OutStanding = it.OutstandingAmount.ToMoneyDto(),
                             CreatedDate = a.CreationTime,
                             ServiceCentre = "--" //TODO,
                             
                         }
                );

            query = request switch
            {
                { Filter: PatientSeenFilter.Today } => query.Where(x => x.CreatedDate.Date == DateTime.Now.Date),
                { Filter: PatientSeenFilter.ThisWeek } => query.Where(x => x.CreatedDate.Date >= DateTime.Today.AddDays(-7) && x.CreatedDate.Date <= DateTime.Today),
                { Filter: PatientSeenFilter.ThisMonth } => query.Where(x => x.CreatedDate.Date >= DateTime.Today.AddMonths(-1) && x.CreatedDate.Date <= DateTime.Today),
                { Filter: PatientSeenFilter.ThisYear } => query.Where(x => x.CreatedDate.Date >= DateTime.Today.AddYears(-1) && x.CreatedDate.Date <= DateTime.Today),
                _ => query
            };
            return await query.ToListAsync();


        }
    }
}