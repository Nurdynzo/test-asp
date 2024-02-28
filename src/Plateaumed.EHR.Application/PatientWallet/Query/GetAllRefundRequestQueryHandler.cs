using System;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.PatientWallet.Dtos.WalletRefund;
using System.Linq;
using Abp.Application.Services.Dto;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.PatientWallet.Abstractions;

namespace Plateaumed.EHR.PatientWallet.Query
{
    public class GetAllRefundRequestQueryHandler : IGetAllRefundRequestQueryHandler
    {
        private readonly IRepository<InvoiceRefundRequest, long> _invoiceRefundRequestRepository;
        private readonly IRepository<Patient,long> _patientRepository;
        private readonly IRepository<Invoice, long> _invoiceRepository;
        private readonly IRepository<PatientCodeMapping,long> _patientCodeMappingRepository;
        public GetAllRefundRequestQueryHandler(
            IRepository<InvoiceRefundRequest, long> invoiceRefundRequestRepository, 
            IRepository<Patient, long> patientRepository, 
            IRepository<Invoice, long> invoiceRepository, 
            IRepository<PatientCodeMapping, long> patientCodeMappingRepository)
        {
            _invoiceRefundRequestRepository = invoiceRefundRequestRepository;
            _patientRepository = patientRepository;
            _invoiceRepository = invoiceRepository;
            _patientCodeMappingRepository = patientCodeMappingRepository;
        }
        public async Task<PagedResultDto<RefundRequestListQueryResponse>> Handle(
            RefundRequestListQueryRequest request,
            long facilityId)
        {
            var query = (from r in _invoiceRefundRequestRepository.GetAll()
                         join p in _patientRepository.GetAll() on r.PatientId equals p.Id
                         join c in _patientCodeMappingRepository.GetAll() on p.Id equals c.PatientId
                         join i in _invoiceRepository.GetAll() on r.InvoiceId equals i.Id
                         where r.FacilityId == facilityId
                         group new
                         { r, i } by new
                         {
                             r.PatientId,
                             p.FirstName,
                             p.LastName,
                             p.DateOfBirth,
                             p.GenderType,
                             c.PatientCode,
                             i.InvoiceSource
                         }
                         into grouped
                         select new RefundRequestListQueryResponse
                         {
                             PatientCode = grouped.Key.PatientCode,
                             Dob = grouped.Key.DateOfBirth,
                             Gender = grouped.Key.GenderType.ToString(),
                             PatientName = $"{grouped.Key.FirstName} {grouped.Key.LastName}",
                             CreationDate = grouped.First().r.CreationTime,
                             TotalReceiptAmount = new MoneyDto()
                             {
                                 Amount = grouped.Sum(x => x.i.AmountPaid != null ?
                                     x.i.TotalAmount.Amount: 0),
                                 Currency = grouped.First().i.AmountPaid.Currency
                             },
                             TotalRefundAmount = new MoneyDto()
                             {
                                 Amount = grouped.Sum(x => x.i.TotalAmount != null ?
                                     x.i.TotalAmount.Amount: 0),
                                 Currency = grouped.First().i.TotalAmount.Currency
                             },
                             InvoiceSource = grouped.Key.InvoiceSource,
                             IsApprove = grouped.Any(x => x.r.Status == InvoiceRefundStatus.Approved),
                             IsReject = grouped.Any(x => x.r.Status == InvoiceRefundStatus.Rejected),
                             IsPendingApproval = grouped.Any(x => x.r.Status == InvoiceRefundStatus.Pending),
                         });
            //filter 
            query = request switch
            {
                { InvoiceSource: not null } => query.Where(x => x.InvoiceSource == request.InvoiceSource),
                { DateFilter: PatientSeenFilter.Today } => query.Where(x => x.CreationDate.Date == DateTime.Now.Date),
                { DateFilter: PatientSeenFilter.ThisWeek } => query.Where(x => x.CreationDate.Date >= DateTime.Now.Date.AddDays(-7)),
                { DateFilter: PatientSeenFilter.ThisMonth } => query.Where(x => x.CreationDate.Date >= DateTime.Now.Date.AddDays(-30)),
                { DateFilter: PatientSeenFilter.ThisYear } => query.Where(x => x.CreationDate.Date >= DateTime.Now.Date.AddDays(-365)),
                _ => query
            };
            
            
                
            var  queryResult = await query.Skip(request.SkipCount).Take(request.MaxResultCount).ToListAsync();
            var count = await query.CountAsync();
            return new PagedResultDto<RefundRequestListQueryResponse>(count,queryResult);
           
        }
    }
}