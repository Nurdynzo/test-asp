using System.Threading.Tasks;
using Abp.Runtime.Session;
using Abp.UI;
using Plateaumed.EHR.IntakeOutputs.Abstractions;
using Plateaumed.EHR.IntakeOutputs.Dtos;

namespace Plateaumed.EHR.IntakeOutputs.Query.BaseQueryHelper;

public class GetOutputQueryHandler : IGetOutputQueryHandler
{
    private readonly IBaseQuery _baseQuery;

    public GetOutputQueryHandler(IBaseQuery baseQuery)
    {
        _baseQuery = baseQuery;
    }

    public async Task<PatientIntakeOutputDto> Handle(long patientId)
    {
        if (patientId == 0)
            throw new UserFriendlyException($"Patient Id is required.");

        var result = await _baseQuery.GetOutputSuggestions(patientId);
        return result;
    }
}
