using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Investigations.Dto;

namespace Plateaumed.EHR.Investigations.Handlers;

public class GetInvestigationRequestsQueryHandler : IGetInvestigationRequestsQueryHandler
{
    private readonly IRepository<InvestigationRequest, long> _investigationRequestRepository;

    public GetInvestigationRequestsQueryHandler(IRepository<InvestigationRequest, long> investigationRequestRepository)
    {
        _investigationRequestRepository = investigationRequestRepository;
    }

    public async Task<List<GetInvestigationRequestsResponse>> Handle(GetInvestigationRequestsRequest request)
    {
        return await _investigationRequestRepository.GetAll()
            .Include(x => x.Investigation)
            .Where(x => x.PatientId == request.PatientId)
            .WhereIf(!string.IsNullOrWhiteSpace(request.Type), x => x.Investigation.Type == request.Type)
            .WhereIf(request.ProcedureId.HasValue, v => v.ProcedureId == request.ProcedureId.Value)
            .Select(x => new GetInvestigationRequestsResponse
            {
                Id = x.Id,
                InvestigationId = x.InvestigationId,
                Name = x.Investigation.Name,
                Type = x.Investigation.Type,
                Specimen = x.Investigation.Specimen,
                SpecificOrganism = x.Investigation.SpecificOrganism,
                Urgent = x.Urgent,
                WithContrast = x.WithContrast,
                CreationTime = x.CreationTime,
                ProcedureId = x.ProcedureId
            })
            .OrderByDescending(x => x.CreationTime)
            .ToListAsync();
    }
}