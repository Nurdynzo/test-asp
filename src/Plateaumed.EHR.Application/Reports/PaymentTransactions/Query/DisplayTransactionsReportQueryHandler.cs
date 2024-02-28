using Abp.Domain.Repositories;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Invoices.Dtos.UnpaidInvoicesDtos;
using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Reports.PaymentTransactions.Abstractions;

namespace Plateaumed.EHR.Reports.PaymentTransactions.Query
{
    public class DisplayTransactionsReportQueryHandler : IDisplayTransactionsReportQueryHandler
    {
        private readonly IRepository<PaymentActivityLog, long> _paymentActivityRepository;
        private readonly IRepository<Patient, long> _patientRepository;
        private readonly IRepository<Facility, long> _facilityRepository;
        public DisplayTransactionsReportQueryHandler(IRepository<PaymentActivityLog, long> paymentActivityRepository,
            IRepository<Patient, long> patientRepository,
            IRepository<Facility, long> facilityRepository)
        {
            _paymentActivityRepository = paymentActivityRepository;
            _patientRepository = patientRepository;
            _facilityRepository = facilityRepository;
        }

        public async Task<List<DisplayPaymentTransactionsResponseDto>> Handle(DownloadPaymentActivityRequest request, long facilityId)
        {
            var query = from pa in _paymentActivityRepository.GetAll()
                        join p in _patientRepository.GetAll() on pa.PatientId equals p.Id
                        where pa.FacilityId == facilityId && pa.TransactionAction == TransactionAction.PaidInvoice &&
                        pa.CreationTime >= request.StartTime && pa.CreationTime <= request.EndTime
                        select new DisplayPaymentTransactionsResponseDto
                        {
                            PatientId = p.IdentificationCode,
                            PatientName = p.FullName,
                            Age = $"{GetAge(p.DateOfBirth)} yrs",
                            Gender = p.GenderType.ToString(),
                            ServiceCenter = pa.Facility.Name,
                            InvoiceTotal = pa.Invoice.TotalAmount.Amount,
                            AmountPaid = pa.AmountPaid.Amount,
                            OutStandingAmount = pa.OutStandingAmount.Amount,
                            WalletTopUpTotal = pa.ToUpAmount.Amount,
                            InvoiceNo = pa.InvoiceNo,
                            TransactionAction = pa.TransactionAction.ToString(),
                            TransactionType = pa.TransactionType.ToString(),
                            TransactionDate = pa.CreationTime
                        };
            query = query.Take(request.SkipCount).Skip(request.MaxResultCount);
            var items = await query.ToListAsync();
            var tenantName = _facilityRepository.SingleAsync(x => x.Id == facilityId).Result.Name;
            items.Select(x => x.TenantName = tenantName);
            return items;
        }

        private static int GetAge(DateTime dob)
        {
            DateTime dateTimeToday = DateTime.Today;
            TimeSpan difference = dateTimeToday.Subtract(dob);
            var firstDay = new DateTime(1, 1, 1);
            int totalYears = (firstDay + difference).Year - 1;
            return totalYears;
        }
    }
}
