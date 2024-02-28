using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.PatientProfile.Dto;
using Plateaumed.EHR.Patients.Dtos;

namespace Plateaumed.EHR.PatientProfile.Abstraction;

public interface IGetPatientPastMedicalHistoryQueryHandler : ITransientDependency
{
    Task<GetPatientPastMedicalConditionQueryResponse> Handle(long patientId);
}