using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Invoices.Abstraction;
using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.Invoices.Dtos.PatientWithUnpaidInvoiceItemsDtos;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.PatientWallet;
using System;
using System.Linq;
using System.Threading.Tasks;
using Plateaumed.EHR.Invoices.Dtos.PatientWithInvoiceItemsDtos;
using Plateaumed.EHR.Misc;

namespace Plateaumed.EHR.Invoices.Query
{
    /// <inheritdoc/>
    public class GetPatientsWithInvoiceItemsQueryHandler : IGetPatientsWithInvoiceItemsQueryHandler
    {
        private readonly IRepository<Patient, long> _patientRepository;
        private readonly IRepository<PatientCodeMapping, long> _patientCodeMappingRepository;
        private readonly IRepository<Invoice, long> _invoiceRepository;
        private readonly IRepository<InvoiceItem, long> _invoiceItemRepository;
        private readonly IRepository<Wallet, long> _walletRepository;
        private readonly IRepository<PatientAppointment,long> _patientAppointmentRepository;


        /// <param name="patientRepository"></param>
        /// <param name="patientCodeMappingRepository"></param>
        /// <param name="invoiceRepository"></param>
        /// <param name="invoiceItemRepository"></param>
        /// <param name="walletRepository"></param>
        /// <param name="patientAppointmentRepository"></param>
        public GetPatientsWithInvoiceItemsQueryHandler(
            IRepository<Patient, long> patientRepository,
            IRepository<PatientCodeMapping, long> patientCodeMappingRepository,
            IRepository<Invoice, long> invoiceRepository,
            IRepository<InvoiceItem, long> invoiceItemRepository,
            IRepository<Wallet, long> walletRepository,
            IRepository<PatientAppointment, long> patientAppointmentRepository)
        {

            _patientRepository = patientRepository;
            _patientCodeMappingRepository = patientCodeMappingRepository;
            _invoiceRepository = invoiceRepository;
            _invoiceItemRepository = invoiceItemRepository;
            _walletRepository = walletRepository;
            _patientAppointmentRepository = patientAppointmentRepository;
        }

        /// <inheritdoc/>
        public async Task<PagedResultDto<PatientsWithInvoiceItemsResponse>> Handle(PatientsWithInvoiceItemsRequest request, long facilityId)
        {
            var resultQuery = GetPatientWithUnpaidInvoiceItemQueryResult(request, facilityId);


            // Apply Skip and Take
            var pagedQuery = resultQuery.Skip(request.SkipCount).Take(request.MaxResultCount);

            // Execute the query and retrieve the results
            var results = await pagedQuery.ToListAsync();
            var count = await pagedQuery.CountAsync();

            return new PagedResultDto<PatientsWithInvoiceItemsResponse>
            {
                TotalCount = count,
                Items = results,
            };
        }


        private IQueryable<PatientsWithInvoiceItemsResponse> GetPatientWithUnpaidInvoiceItemQueryResult(PatientsWithInvoiceItemsRequest request, long facilityId) {

            var today = DateTime.UtcNow.Date;
            var filterString = !string.IsNullOrEmpty(request.Filter) ? request.Filter.ToLower() : string.Empty;

            var query = (from i in _invoiceRepository.GetAll()
                         join a in _patientAppointmentRepository.GetAll() on i.PatientAppointmentId equals a.Id
                         join p in _patientRepository.GetAll() on i.PatientId equals p.Id
                         join pw in _walletRepository.GetAll() on p.Id equals pw.PatientId into pg
                         from pw in pg.DefaultIfEmpty()
                         join pcm in _patientCodeMappingRepository.GetAll() on p.Id equals pcm.Id
                         join it in _invoiceItemRepository.GetAll() on i.Id equals it.InvoiceId
                         where i.FacilityId == facilityId
                         orderby i.CreationTime descending
                         select new { i, it, pcm, pw, p , a});

            // Apply Filtering
            query = request switch
            {
                { Filter: not null } => query.Where(x
                    => x.p.FirstName.ToLower().Contains(filterString) ||
                       x.p.LastName.ToLower().Contains(filterString) ||
                       x.pcm.PatientCode.ToLower().Contains(filterString) ||
                       x.p.GenderType.ToString().ToLower().Contains(filterString) ||
                       x.p.EmailAddress.ToLower().Contains(filterString)
                ),
                { StartDate: not null, EndDate: not null } => query.Where(x => x.i.CreationTime.Date >= request.StartDate.Value.Date && x.i.CreationTime.Date <= request.EndDate.Value.Date),
                { StartDate: not null } => query.Where(x => x.i.CreationTime.Date == request.StartDate.Value.Date),
                { EndDate: not null } => query.Where(x => x.i.CreationTime.Date >= today && x.i.CreationTime.Date <= request.EndDate.Value.Date),
                { PatientSeenFilter: PatientSeenFilter.Today } => query.Where(x => x.i.CreationTime.Date == DateTime.Today),
                { PatientSeenFilter: PatientSeenFilter.ThisWeek } => query.Where(x =>
                    x.a.StartTime.Date >= DateTimeOffset.UtcNow.AddDays(-7)),
                { PatientSeenFilter: PatientSeenFilter.ThisMonth } => query.Where(x =>
                    x.a.StartTime.Date >= DateTimeOffset.UtcNow.AddDays(-30)),
                { PatientSeenFilter: PatientSeenFilter.ThisYear } => query.Where(x =>
                    x.a.StartTime.Date >= DateTimeOffset.UtcNow.AddDays(-365)),
                { InvoiceSource: InvoiceSource.Lab } => query.Where(x => x.i.InvoiceSource == InvoiceSource.Lab),
                { InvoiceSource: InvoiceSource.Pharmacy } => query.Where(x => x.i.InvoiceSource == InvoiceSource.Pharmacy),
                { InvoiceSource: InvoiceSource.AccidentAndEmergency } => query.Where(x =>
                    x.i.InvoiceSource == InvoiceSource.AccidentAndEmergency),
                { InvoiceSource: InvoiceSource.OutPatient } => query.Where(x =>
                    x.i.InvoiceSource == InvoiceSource.OutPatient),
                { InvoiceSource: InvoiceSource.InPatient } =>
                    query.Where(x => x.i.InvoiceSource == InvoiceSource.InPatient),
                { InvoiceSource: InvoiceSource.Others } => query.Where(x => x.i.InvoiceSource == InvoiceSource.Others),
                _ => query
            };

            // Projection
            var resultQuery = (from p in query.AsNoTracking()
                              group p by
                                  new
                                  {
                                      p.p.Id,
                                      p.p.FirstName,
                                      p.p.LastName,
                                      p.p.DateOfBirth,
                                      p.p.GenderType,
                                      p.pcm.PatientCode
                                  } into  g
                              select new PatientsWithInvoiceItemsResponse
                              {
                                  PatientId = g.Key.Id,
                                  FirstName = g.Key.FirstName,
                                  LastName = g.Key.LastName,
                                  PatientCode = g.Key.PatientCode,
                                  DateOfBirth = g.Key.DateOfBirth,
                                  GenderType = g.Key.GenderType,
                                  HasPendingWalletFundingRequest = g.Any(x=> x.it.Status == InvoiceItemStatus.AwaitingApproval),
                                  WalletBalance = g.FirstOrDefault(x => x.pw != null).pw == null ? new MoneyDto() : new MoneyDto
                                  {
                                      Amount = g.FirstOrDefault().pw.Balance.Amount,
                                      Currency = g.FirstOrDefault().pw.Balance.Currency,
                                  },
                                  TotalOutstanding = new MoneyDto
                                  {
                                      Amount = g
                                     .Where(i => i.it.Status == InvoiceItemStatus.Unpaid)
                                     .Sum(x => x.it.SubTotal.Amount),
                                      Currency = g.First().it.SubTotal.Currency,
                                  },

                                  InvoiceItems = g
                                  .OrderByDescending(x => x.it.LastModificationTime)
                                  .Select(i => new InvoiceItemSummary
                                  {
                                      Id = i.it.Id,
                                      Name = i.it.Name,
                                      Status = i.it.Status.ToString(),
                                      SubTotal = new MoneyDto
                                      {
                                          Amount = i.it.SubTotal.Amount,
                                          Currency = i.it.SubTotal.Currency,
                                      },
                                  }),

                                  LastPaymentDate = g
                                  .Where(x => x.it.Status == InvoiceItemStatus.Paid)
                                  .OrderByDescending(x => x.it.LastModificationTime)
                                  .Select(x => x.it.LastModificationTime).FirstOrDefault(),
                              });


            return resultQuery;
        }
    }
}
