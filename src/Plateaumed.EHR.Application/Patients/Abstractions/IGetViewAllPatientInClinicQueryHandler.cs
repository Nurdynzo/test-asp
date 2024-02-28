using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Patients.Dtos;
namespace Plateaumed.EHR.Patients.Abstractions
{
    public interface IGetViewAllPatientInClinicQueryHandler : ITransientDependency
    {
        Task<List<AllPatientInClinicResponse>> Handle(AllPatientInClinicRequest request, long facilityId);
    }
}
