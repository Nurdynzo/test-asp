using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.PatientProfile.Dto;

namespace Plateaumed.EHR.PatientProfile.Abstraction;

public interface IGetChronicDiseaseSuggestionQueryHandler : ITransientDependency
{
    Task<List<GetChronicDiseaseSuggestionQueryResponse>> Handle();
}