using Abp.Dependency;
using Plateaumed.EHR.PatientProfile.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Abstraction
{
    public interface IGetPatientReviewOfSystemsDataQueryHandler : ITransientDependency
    {
        Task<List<GetPatientReviewOfSystemsDataResponseDto>> Handle(long patientId);
    }
}
