using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Plateaumed.EHR.Investigations;
using Plateaumed.EHR.PatientProfile.Dto;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile.Abstraction;
namespace Plateaumed.EHR.PatientProfile.Query
{
    public class GetClinicalInvestigationQueryHandler : IGetClinicalInvestigationQueryHandler
    {
        private readonly IRepository<InvestigationResult, long>  _investigationResultRepository;
        public GetClinicalInvestigationQueryHandler(IRepository<InvestigationResult, long> investigationResultRepository)
        {
            _investigationResultRepository = investigationResultRepository;
        }
        public async Task<List<ClinicalInvestigationResultResponse>> Handle(GetClinicalInvestigationQuery request)
        {
            var query = (from i in _investigationResultRepository.GetAll()
                             .Include(x => x.InvestigationComponentResults)
                         where i.PatientId == request.PatientId
                         select new ClinicalInvestigationResultResponse
                         {
                             Date = i.CreationTime,
                             Name = i.Name,
                             Note = i.Notes,
                             Conclusion = i.Conclusion,
                             FacilityId = i.PatientEncounter.FacilityId,
                             ResultComponent = i.InvestigationComponentResults
                                 .Where(f =>
                                     string.IsNullOrEmpty(request.CategoryFilter) || f.Category.Equals(request.CategoryFilter)
                                     && (string.IsNullOrEmpty(request.TestFilter) || f.Name.Equals(request.TestFilter)))
                                 .Select(x => new ResultComponentResponse
                                 {
                                     Category = x.Category,
                                     Name = x.Name,
                                     NumericResult = x.NumericResult,
                                     RangeMax = x.RangeMax,
                                     RangeMin = x.RangeMin,
                                     Reference = x.Reference,
                                     Result = x.Result,
                                     Unit = x.Unit
                                 }).ToList()

                         }
                );
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
