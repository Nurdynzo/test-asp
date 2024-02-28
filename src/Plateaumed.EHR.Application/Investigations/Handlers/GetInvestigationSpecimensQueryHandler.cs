using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Investigations.Dto;

namespace Plateaumed.EHR.Investigations.Handlers;

public class GetInvestigationSpecimensQueryHandler : IGetInvestigationSpecimensQueryHandler
{
    private readonly IRepository<Investigation, long> _repository;

    public GetInvestigationSpecimensQueryHandler(IRepository<Investigation, long> repository)
    {
        _repository = repository;
    }

    public async Task<GetInvestigationSpecimensResponse> Handle(GetInvestigationSpecimensRequest request)
    {
        var specimens = await _repository.GetAll()
            .WhereIf(!string.IsNullOrWhiteSpace(request.Type), x => x.Type.ToLower() == request.Type.ToLower())
            .Select(x => x.Specimen)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Distinct()
            .ToListAsync();

        return new GetInvestigationSpecimensResponse
        {
            Specimens = specimens
        };
    }
}