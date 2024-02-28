using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Invoices.Abstraction;
using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Patients;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Invoices.Query
{
    public class GetEditedInvoiceQueryHandler : IGetEditedInvoiceQueryHandler
    {
        private readonly IRepository<PaymentActivityLog, long> _logRepository;
        private readonly IRepository<Patient, long> _patientRepository;
        private readonly IRepository<PatientCodeMapping,long> _patientCodeMappingRepository;
        private readonly IRepository<Invoice, long> _invoiceRepository;

        public GetEditedInvoiceQueryHandler(
            IRepository<PaymentActivityLog, long> logRepository,
            IRepository<Patient, long> patientRepository, 
            IRepository<PatientCodeMapping, long> patientCodeMappingRepository, 
            IRepository<Invoice, long> invoiceRepository)
        {
            _logRepository = logRepository;
            _patientRepository = patientRepository;
            _patientCodeMappingRepository = patientCodeMappingRepository;
            _invoiceRepository = invoiceRepository;
        }

        public async Task<PagedResultDto<GetEditedInvoiceResponseDto>> Handle(GetEditedInvoiceRequestDto request, long facilityId)
        {
            var filter = string.IsNullOrWhiteSpace(request.SearchText) ? string.Empty : request.SearchText.ToLower();
            
            var query = (from i in _logRepository.GetAll()
                            join p in _patientRepository.GetAll() on i.PatientId equals p.Id
                            join pcm in _patientCodeMappingRepository.GetAll() on p.Id equals pcm.PatientId
                            join w in _invoiceRepository.GetAll() on i.InvoiceId equals w.Id
                            where pcm.FacilityId == facilityId &&
                                  i.FacilityId == facilityId &&
                                  i.TransactionAction == TransactionAction.EditInvoice
                            group i by new
                            {
                                i.InvoiceId,
                                p.FirstName,
                                i.InvoiceNo,
                                p.LastName,
                                i.CreationTime,
                                pcm.PatientCode,
                                p.DateOfBirth,
                                p.GenderType,
                                w.InvoiceSource
                                
                            } into groupedInvoices
                            select new GetEditedInvoiceResponseDto
                            {
                                InvoiceId = groupedInvoices.Key.InvoiceId.GetValueOrDefault(),
                                PatientName = $"{groupedInvoices.Key.FirstName} {groupedInvoices.Key.LastName}",
                                DateOfBirth = groupedInvoices.Key.DateOfBirth,
                                Gender = groupedInvoices.Key.GenderType.ToString(),
                                InvoiceNumber = groupedInvoices.Key.InvoiceNo,
                                PatientCode = groupedInvoices.Key.PatientCode,
                                Ward = "N/A",
                                TotalAmountBeforeEdit = new MoneyDto
                                {
                                    Amount = groupedInvoices.Sum(x => x.ActualAmount.Amount),
                                    Currency = groupedInvoices.Select(x => x.ActualAmount.Currency).ToArray()[0]
                                },
                                TotalEditedInvoiceAmount = new MoneyDto
                                {
                                    Amount = groupedInvoices.Sum(x => x.EditAmount.Amount),
                                    Currency = groupedInvoices.Select(x => x.EditAmount.Currency).ToArray()[0]
                                },
                                TotalAmountPaid = new MoneyDto
                                {
                                    Amount = groupedInvoices.Sum(x => x.AmountPaid.Amount),
                                    Currency = groupedInvoices.Select(x => x.AmountPaid.Currency).ToArray()[0]
                                },
                                TotalOutstanding = new MoneyDto
                                {
                                    Amount = groupedInvoices.Sum(x => x.OutStandingAmount.Amount),
                                    Currency = groupedInvoices.Select(x => x.OutStandingAmount.Currency).ToArray()[0]
                                },
                                InvoiceSource = groupedInvoices.Key.InvoiceSource,
                                CreationTime = groupedInvoices.Key.CreationTime
                            });
            query = request switch
            {
                {SearchText: not null} => query.Where(x => 
                    x.PatientName.ToLower().Contains(filter) || 
                    x.PatientCode.ToLower().Contains(filter) || 
                    x.InvoiceNumber.ToLower().Contains(filter) ||
                    x.Ward.ToLower().Contains(filter)),
                { FilterDate: PatientSeenFilter.Today } => query.Where(x => x.CreationTime.Date == DateTime.Now.Date),
                { FilterDate: PatientSeenFilter.ThisWeek } => query.Where(x => x.CreationTime.Date >= DateTime.Now.Date.AddDays(-7)),
                { FilterDate: PatientSeenFilter.ThisMonth } => query.Where(x => x.CreationTime.Date >= DateTime.Now.Date.AddMonths(-1)),
                { FilterDate: PatientSeenFilter.ThisYear } => query.Where(x => x.CreationTime.Date >= DateTime.Now.Date.AddYears(-1)),
                { InvoiceSource: not null } => query.Where(x => x.InvoiceSource == request.InvoiceSource),
                { StartDate: not null, EndTime: not null } => query.Where(x => x.CreationTime >= request.StartDate && x.CreationTime <= request.EndTime),
                _ => query
            };
            query = query.Skip(request.SkipCount).Take(request.MaxResultCount);

            var count = await query.CountAsync();
            var result = await query.ToListAsync();


            return new PagedResultDto<GetEditedInvoiceResponseDto>
            {
                TotalCount = count,
                Items = result,
            };
            
        }
    }
}
