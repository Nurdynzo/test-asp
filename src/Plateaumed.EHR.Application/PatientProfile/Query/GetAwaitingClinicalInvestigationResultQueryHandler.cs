using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Investigations;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Organizations;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;
namespace Plateaumed.EHR.PatientProfile.Query
{
    public class GetAwaitingClinicalInvestigationResultQueryHandler : IGetAwaitingClinicalInvestigationResultQueryHandler
    {
        private readonly IRepository<InvestigationRequest, long> _investigationRequestRepository;
        private readonly IRepository<Investigation, long> _investigationRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<OrganizationUnitExtended, long> _organizationUnitRepository;

        public GetAwaitingClinicalInvestigationResultQueryHandler(IRepository<InvestigationRequest, long> investigationRequestRepository, IRepository<User, long> userRepository, IRepository<OrganizationUnitExtended, long> organizationUnitRepository, IRepository<Investigation, long> investigationRepository)
        {
            _investigationRequestRepository = investigationRequestRepository;
            _userRepository = userRepository;
            _organizationUnitRepository = organizationUnitRepository;
            _investigationRepository = investigationRepository;
        }
        public async Task<List<AwaitClinicalInvestigationResultResponse>> Handle(GetClinicalInvestigationQuery request)
        {
            var query = from r in _investigationRequestRepository.GetAll()
                        join i in _investigationRepository.GetAll() on r.InvestigationId equals i.Id
                        join u in _userRepository.GetAll() on r.CreatorUserId equals u.Id
                        from ou in _organizationUnitRepository.GetAll()
                            .Where(x =>
                                u.OrganizationUnits.Select(s => s.Id)
                                    .Contains(x.Id)).DefaultIfEmpty()
                        where r.PatientId == request.PatientId &&
                              ou.Type == OrganizationUnitType.Clinic
                              && r.InvestigationStatus == InvestigationStatus.ReportReady
                              && (string.IsNullOrEmpty(request.CategoryFilter) || i.Type.Equals(request.CategoryFilter))
                        select new AwaitClinicalInvestigationResultResponse
                        {
                            Name = i.Name,
                            Date = r.CreationTime,
                            Physician = u.FullName,
                            Clinic = ou.DisplayName
                        };
            query = request switch
            {
                { DateFilter: InvestigationResultDateFilter.Today } => query.Where(x => x.Date.Date == DateTime.UtcNow.Date),
                { DateFilter: InvestigationResultDateFilter.LastSevenDays } => query.Where(x => x.Date.Date >= DateTime.UtcNow.AddDays(-7)),
                { DateFilter: InvestigationResultDateFilter.LastFourteenDays } => query.Where(x => x.Date.Date >= DateTime.UtcNow.AddDays(-14)),
                { DateFilter: InvestigationResultDateFilter.LastThirtyDays } => query.Where(x => x.Date.Date >= DateTime.UtcNow.AddDays(-30)),
                { DateFilter: InvestigationResultDateFilter.LastNinetyDays } => query.Where(x => x.Date.Date >= DateTime.UtcNow.AddDays(-90)),
                { DateFilter: InvestigationResultDateFilter.LastOneYear } => query.Where(x => x.Date.Date >= DateTime.UtcNow.AddDays(-365)),
                _ => query
            };
            return await query.ToListAsync().ConfigureAwait(false);
        }

    }
}
