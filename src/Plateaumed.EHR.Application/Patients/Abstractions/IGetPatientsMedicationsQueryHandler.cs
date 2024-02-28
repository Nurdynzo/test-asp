using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Patients.Dtos;

namespace Plateaumed.EHR.Patients.Abstractions
{
    public interface IGetPatientsMedicationsQueryHandler: ITransientDependency
    {
        Task<GetPatientsMedicationsResponse> Handle(long patientId, long facilityId);
	}
}

