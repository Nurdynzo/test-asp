using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.PatientWallet;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Invoices.Abstraction;
using Plateaumed.EHR.Misc;

namespace Plateaumed.EHR.Invoices.Query;

public class PaymentLandingListQueryHandler : IPaymentLandingListQueryHandler
{
    private readonly IRepository<Invoice,long> _invoiceRepository;
    private readonly IRepository<Patient,long> _patientRepository;
    private readonly IRepository<InvoiceItem, long> _invoiceItemRepository;
    private readonly IRepository<PatientCodeMapping,long> _patientCodeMappingRepository;
    private readonly IRepository<Wallet,long> _walletRepository;
    private readonly IRepository<WalletHistory,long> _walletHistoryRepository;
    private readonly IRepository<PatientAppointment,long> _patientAppointmentRepository;

    public PaymentLandingListQueryHandler(
        IRepository<Invoice, long> invoiceRepository,
        IRepository<Patient, long> patientRepository,
        IRepository<InvoiceItem, long> invoiceItemRepository,
        IRepository<PatientCodeMapping, long> patientCodeMappingRepository,
        IRepository<Wallet, long> walletRepository, 
        IRepository<WalletHistory, long> walletHistoryRepository,
        IRepository<PatientAppointment, long> patientAppointmentRepository)
    {
        _invoiceRepository = invoiceRepository;
        _patientRepository = patientRepository;
        _invoiceItemRepository = invoiceItemRepository;
        _patientCodeMappingRepository = patientCodeMappingRepository;
        _walletRepository = walletRepository;
        _walletHistoryRepository = walletHistoryRepository;
        _patientAppointmentRepository = patientAppointmentRepository;
    }

    public async Task<PagedResultDto<GetPaymentLadingListQueryResponse>> Handle(
        PaymentLandingListFilterRequest request,
        long facilityId)
    {
        var filterString = !string.IsNullOrEmpty(request.Filter) ? request.Filter.ToLower() : string.Empty;
        var query =
            from p in _patientRepository.GetAll().AsNoTracking()
            join i in _invoiceRepository.GetAll() on p.Id equals i.PatientId
            join pi in _invoiceItemRepository.GetAll() on i.Id equals pi.InvoiceId
            join a in _patientAppointmentRepository.GetAll() on p.Id equals a.PatientId
            join pcm in _patientCodeMappingRepository.GetAll() on p.Id equals pcm.PatientId
            join w in _walletRepository.GetAll() on p.Id equals w.PatientId
            join wh in _walletHistoryRepository.GetAll() on w.Id equals wh.WalletId into whg
            from wh in whg.DefaultIfEmpty()
            where pcm.FacilityId == facilityId
            orderby i.CreationTime descending
            group new { i, p, pi, wh, a } by new
            {
                i.PatientId,
                pcm.FacilityId,
                pcm.PatientCode,
                p.DateOfBirth,
                p.GenderType,
                p.FirstName,
                p.MiddleName,
                p.LastName,
                p.EmailAddress,
                w.Balance.Amount,
                w.Balance.Currency,
                i.InvoiceSource,
                
            }
            into g
            select new GetPaymentLadingListQueryResponse
            {
                PatientId = g.Key.PatientId,
                FirstName = g.Key.FirstName,
                LastName = g.Key.LastName,
                MiddleName = g.Key.MiddleName,
                PatientCode = g.Key.PatientCode,
                DateOfBirth = g.Key.DateOfBirth,
                Gender = g.Key.GenderType.ToString(),
                WalletBalance = new MoneyDto { Amount = g.Key.Amount, Currency = g.Key.Currency },
                // Ward = p.Ward, Todo join to encounter  table to get ward information
                AmountPaid = new MoneyDto
                {
                    Amount =
                        g.Select(x => x.pi)
                            .Distinct()
                            .Where(x => x.Status == InvoiceItemStatus.Paid)
                            .Sum(x => x.AmountPaid.Amount),
                    Currency = g.First().pi.AmountPaid.Currency
                },
                OutstandingAmount = new MoneyDto
                {
                    Amount = g.Select(x => x.pi)
                        .Distinct()
                        .Where(x => x.Status == InvoiceItemStatus.Unpaid)
                        .Sum(x => x.OutstandingAmount.Amount),
                    Currency = g.First().pi.OutstandingAmount.Currency
                },
                ActualInvoiceAmount = new MoneyDto
                {
                    Amount = g.Select(x => x.pi).Distinct().Sum(x => x.SubTotal.Amount),
                    Currency = g.First().pi.SubTotal.Currency
                },
                WalletTopUpAmount = new MoneyDto
                {
                    Amount = g
                        .Where(x =>
                            x.wh != null && x.wh.Status == TransactionStatus.Approved)
                        .Select(x => x.wh)
                        .Distinct()
                        .Sum(x => x.Amount.Amount),
                    Currency = g.First().wh.Amount.Currency
                },
                LastPaymentDate = g
                    .Select(x => x.pi)
                    .Where(x => x.Status == InvoiceItemStatus.Paid)
                    .OrderByDescending(x => x.LastModificationTime)
                    .FirstOrDefault().LastModificationTime.GetValueOrDefault(),
                AppointmentStatus = g.Select(x => x.a)
                    .OrderByDescending(x => x.LastModificationTime)
                    .FirstOrDefault().Status.ToString(),
                HasPendingWalletRequest = g.Any(x => x.pi.Status == InvoiceItemStatus.AwaitingApproval),
                InvoiceSource = g.Key.InvoiceSource,
                AllVisits = new List<PaymentAllInputResponse>(), // todo get all visits
                InvoiceItemDate = g.Max(x => x.pi.CreationTime),
                AppointmentDate = g.Max(x => x.a.CreationTime),
                ToUpDate = g.Where(x=>x.wh.Status == TransactionStatus.Approved).Max(x => x.wh.LastModificationTime),
                IsServiceOnCredit = g.Any(x=>x.i.IsServiceOnCredit),
                PaymentStatus = g.First().i.PaymentStatus,
                EmailAddress = g.Key.EmailAddress,
                TimeOfInvoicePaid = g.Max(x=>x.i.TimeOfInvoicePaid)

            };
       
        // filter
       
        query = request switch
        {
            { Filter: not null } => query.Where(x
                => !string.IsNullOrEmpty(x.FirstName) && x.FirstName.ToLower().Contains(filterString) ||
                   !string.IsNullOrEmpty(x.LastName) && x.LastName.ToLower().Contains(filterString) ||
                   !string.IsNullOrEmpty(x.MiddleName) && x.MiddleName.ToLower().Contains(filterString) || 
                   !string.IsNullOrEmpty(x.PatientCode) && x.PatientCode.ToLower().Contains(filterString)
            ),
            // by filter type
            { FilterType: FilterType.PatientSeen, PatientSeenFilter: PatientSeenFilter.Today } => 
                query.Where(x => 
                    x.AppointmentDate != null &&
                    x.AppointmentDate.Value.Date == DateTime.Now.Date),
            { FilterType: FilterType.PatientSeen, PatientSeenFilter: PatientSeenFilter.ThisWeek } => 
                query.Where(x=>
                    x.AppointmentDate != null && x.AppointmentDate.Value.Date >= DateTime.Now.AddDays(-7).Date),
            { FilterType: FilterType.PatientSeen, PatientSeenFilter: PatientSeenFilter.ThisMonth } => 
                query.Where(x =>
                    x.AppointmentDate != null && x.AppointmentDate.Value.Date >= DateTime.Now.AddDays(-30).Date),
            { FilterType: FilterType.PatientSeen, PatientSeenFilter: PatientSeenFilter.ThisYear } =>
                query.Where(x => 
                    x.AppointmentDate != null && x.AppointmentDate.Value.Date >= DateTime.Now.AddDays(-365).Date),
            { FilterType: FilterType.PatientSeen,  CustomStartDateFilter: not null, CustomEndDateFilter: not null } => 
                query.Where(x =>
                    x.AppointmentDate != null && 
                    (x.AppointmentDate >= request.CustomStartDateFilter.Value && x.AppointmentDate <= request.CustomEndDateFilter.Value)),
           
            { FilterType: FilterType.AmountPaid, PatientSeenFilter: PatientSeenFilter.Today } => 
                query.Where(x => 
                    x.TimeOfInvoicePaid != null &&
                    x.TimeOfInvoicePaid.Value.Date == DateTime.Now.Date),
            { FilterType: FilterType.AmountPaid, PatientSeenFilter: PatientSeenFilter.ThisWeek } => 
                query.Where(x=>
                    x.TimeOfInvoicePaid != null &&
                    x.TimeOfInvoicePaid.Value.Date >= DateTime.Now.AddDays(-7).Date),
            { FilterType: FilterType.AmountPaid, PatientSeenFilter: PatientSeenFilter.ThisMonth } => 
                query.Where(x =>
                    x.TimeOfInvoicePaid != null &&
                    x.TimeOfInvoicePaid.Value.Date >= DateTime.Now.AddDays(-30).Date),
            { FilterType: FilterType.AmountPaid, PatientSeenFilter: PatientSeenFilter.ThisYear } =>
                query.Where(x => 
                    x.TimeOfInvoicePaid != null &&
                    x.TimeOfInvoicePaid.Value.Date >= DateTime.Now.AddDays(-365).Date),
            { FilterType: FilterType.AmountPaid,  CustomStartDateFilter: not null, CustomEndDateFilter: not null } => 
                query.Where(x => 
                    x.TimeOfInvoicePaid != null &&
                    x.TimeOfInvoicePaid.Value.Date >= request.CustomStartDateFilter.Value && x.LastPaymentDate <= request.CustomEndDateFilter.Value),
            { FilterType: FilterType.WalletTopUp, PatientSeenFilter: PatientSeenFilter.Today } => 
                query.Where(x => 
                    x.ToUpDate != null &&
                    x.ToUpDate.Value.Date == DateTime.Now.Date),
            { FilterType: FilterType.WalletTopUp, PatientSeenFilter: PatientSeenFilter.ThisWeek } => 
                query.Where(x=>
                    x.ToUpDate != null  &&
                    x.ToUpDate.Value.Date >= DateTime.Now.AddDays(-7).Date),
            { FilterType: FilterType.WalletTopUp, PatientSeenFilter: PatientSeenFilter.ThisMonth } => 
                query.Where(x =>
                    x.ToUpDate != null   
                    && x.ToUpDate.Value.Date >= DateTime.Now.AddDays(-30).Date),
            { FilterType: FilterType.WalletTopUp, PatientSeenFilter: PatientSeenFilter.ThisYear } =>
                query.Where(x => 
                    x.ToUpDate != null  &&
                    x.ToUpDate.Value.Date >= DateTime.Now.AddDays(-365).Date),
            { FilterType: FilterType.WalletTopUp,  CustomStartDateFilter: not null, CustomEndDateFilter: not null } => 
                query.Where(x =>
                    x.ToUpDate != null  &&
                    (x.ToUpDate >= request.CustomStartDateFilter.Value && x.ToUpDate <= request.CustomEndDateFilter.Value)),
            { FilterType: FilterType.ServiceOnCredit, PatientSeenFilter: PatientSeenFilter.Today } => 
                query.Where(x => 
                    x.IsServiceOnCredit &&
                    x.InvoiceItemDate.Date == DateTime.Now.Date),
            { FilterType: FilterType.ServiceOnCredit, PatientSeenFilter: PatientSeenFilter.ThisWeek } => 
                query.Where(x=>
                    x.IsServiceOnCredit  &&
                    x.InvoiceItemDate.Date >= DateTime.Now.AddDays(-7).Date),
            { FilterType: FilterType.ServiceOnCredit, PatientSeenFilter: PatientSeenFilter.ThisMonth } => 
                query.Where(x =>
                    x.IsServiceOnCredit  &&
                    x.InvoiceItemDate.Date >= DateTime.Now.AddDays(-30).Date),
            { FilterType: FilterType.ServiceOnCredit, PatientSeenFilter: PatientSeenFilter.ThisYear } =>
                query.Where(x => 
                    x.IsServiceOnCredit  &&
                    x.InvoiceItemDate.Date >= DateTime.Now.AddDays(-365).Date),
            { FilterType: FilterType.ServiceOnCredit,  CustomStartDateFilter: not null, CustomEndDateFilter: not null } => 
                query.Where(x =>
                    x.IsServiceOnCredit   &&
                    (x.InvoiceItemDate >= request.CustomStartDateFilter.Value && x.InvoiceItemDate <= request.CustomEndDateFilter.Value)),
            { FilterType: FilterType.OutstandingAmount, PatientSeenFilter: PatientSeenFilter.Today } => 
                query.Where(x => 
                    x.InvoiceItemDate.Date == DateTime.Now.Date),
            { FilterType: FilterType.OutstandingAmount, PatientSeenFilter: PatientSeenFilter.ThisWeek } => 
                query.Where(x=>
                    x.InvoiceItemDate.Date >= DateTime.Now.AddDays(-7).Date),
            { FilterType: FilterType.OutstandingAmount, PatientSeenFilter: PatientSeenFilter.ThisMonth } => 
                query.Where(x =>
                    x.InvoiceItemDate.Date >= DateTime.Now.AddDays(-30).Date),
            { FilterType: FilterType.OutstandingAmount, PatientSeenFilter: PatientSeenFilter.ThisYear } =>
                query.Where(x => 
                    x.InvoiceItemDate.Date >= DateTime.Now.AddDays(-365).Date),
            { FilterType: FilterType.OutstandingAmount,  CustomStartDateFilter: not null, CustomEndDateFilter: not null } => 
                query.Where(x =>
                    (x.InvoiceItemDate >= request.CustomStartDateFilter.Value && x.InvoiceItemDate <= request.CustomEndDateFilter.Value)),
            
            { InvoiceSource: InvoiceSource.Lab } => query.Where(x => x.InvoiceSource == InvoiceSource.Lab),
            { InvoiceSource: InvoiceSource.Pharmacy } => query.Where(x => x.InvoiceSource == InvoiceSource.Pharmacy),
            { InvoiceSource: InvoiceSource.AccidentAndEmergency } => query.Where(x =>
                x.InvoiceSource == InvoiceSource.AccidentAndEmergency),
            { InvoiceSource: InvoiceSource.OutPatient } => query.Where(x =>
                x.InvoiceSource == InvoiceSource.OutPatient),
            { InvoiceSource: InvoiceSource.InPatient } =>
                query.Where(x => x.InvoiceSource == InvoiceSource.InPatient),
            { InvoiceSource: InvoiceSource.Others } => query.Where(x => x.InvoiceSource == InvoiceSource.Others),
            _ => query
        
        };
        
        var resultQuery  =  query.Skip(request.SkipCount).Take(request.MaxResultCount);
        var count =   await query.CountAsync();
        var queryResult = await resultQuery
            .OrderByDescending(x=>x.InvoiceItemDate).ToListAsync();
       
        
        var result = new PagedResultDto<GetPaymentLadingListQueryResponse>(count, queryResult);
        return result;
    

    }
}