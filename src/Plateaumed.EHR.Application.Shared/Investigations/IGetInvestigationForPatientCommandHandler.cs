using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Investigations.Dto;

namespace Plateaumed.EHR.Investigations
{
    public interface IGetInvestigationForPatientCommandHandler: ITransientDependency
    {
        Task<Dictionary<long, GetInvestigationForPatientResponse>> GetInvestigationForPatient(GetPatientInvestigationRequest command);
	}
}