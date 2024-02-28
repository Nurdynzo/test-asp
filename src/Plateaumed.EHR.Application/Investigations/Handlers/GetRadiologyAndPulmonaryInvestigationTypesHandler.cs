using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Investigations.Abstractions;
using Plateaumed.EHR.Investigations.Dto;

namespace Plateaumed.EHR.Investigations.Handlers
{
    public class GetRadiologyAndPulmonaryInvestigationTypesHandler : IGetRadiologyAndPulmonaryInvestigationTypesHandler
    {
        private readonly IRepository<Investigation, long> _investigationRepository;       

        public GetRadiologyAndPulmonaryInvestigationTypesHandler(IRepository<Investigation, long> investigationRepository)
        {
            _investigationRepository = investigationRepository;
        }

        public async Task<List<RadiologyAndPulmonaryInvestigationDto>> Handle(GetElectroRadPulmInvestigationResultDto request)
        {
            var filter = string.IsNullOrWhiteSpace(request.Category) ? "" : request.Category.ToLower();
            var name = string.IsNullOrWhiteSpace(request.Name) ? null : request.Name.ToLower();
            var type = string.IsNullOrWhiteSpace(request.Type) ? null : request.Type.ToLower();

            return await _investigationRepository.GetAll()
                        .Where(x => x.Type.ToLower() == InvestigationTypes.RadiologyAndPulm.ToLower())
                        .Include(x => x.RadiologyAndPulmonary)
                        .WhereIf(name != null, x=>x.Name.ToLower().Contains(name))
                        .WhereIf(type != null, x=>x.Type.ToLower().Contains(type))
                        .Select(x => new RadiologyAndPulmonaryInvestigationDto
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Type = x.Type,
                            InvestigationComponents = x.RadiologyAndPulmonary.Select(r => new RadiologyAndPulmonaryInvestigationsDto
                            {
                                Name = r.Name,
                                Category = r.Category,
                                Id = r.Id,
                                InvestigationId = r.InvestigationId
                            }).Where(x => x.Category.ToLower().Contains(filter)).ToList()
                        }).Where(x => x.InvestigationComponents.Count > 0)
                        .ToListAsync();

        }       
    }
}

