using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PhysicalExaminations.Dto;

namespace Plateaumed.EHR.PhysicalExaminations.Handlers;

public class GetPhysicalExaminationHeadersQueryHandler : IGetPhysicalExaminationHeadersQueryHandler
{
    private readonly IRepository<PhysicalExamination, long> _repository;

    public GetPhysicalExaminationHeadersQueryHandler(IRepository<PhysicalExamination, long> repository)
    {
        _repository = repository;
    }

    public async Task<GetPhysicalExaminationHeadersResponse> Handle(GetPhysicalExaminationHeadersRequest request)
    {
        var headers = await _repository.GetAll()
            .Where(x => x.PhysicalExaminationTypeId == request.PhysicalExaminationTypeId)
            .Select(x => x.Header)
            .Distinct()
            .ToListAsync();

        return new GetPhysicalExaminationHeadersResponse
        {
            Headers = headers
        };
    }
}