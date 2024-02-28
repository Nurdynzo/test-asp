using Abp.Dependency;
using Plateaumed.EHR.PatientProfile.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Abstraction
{
    public interface IGetCigaretteAndTobaccoHistoryQueryHandler : ITransientDependency
    {
        Task<List<GetCigaretteHistoryResponseDto>> Handle(long patientId);
    }
}
