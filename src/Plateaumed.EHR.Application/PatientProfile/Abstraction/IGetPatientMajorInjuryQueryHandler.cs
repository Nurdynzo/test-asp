using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.PatientProfile.Dto;
using Plateaumed.EHR.Patients.Dtos;

namespace Plateaumed.EHR.PatientProfile.Abstraction;

public interface IGetPatientMajorInjuryQueryHandler: ITransientDependency
{
    Task<List<GetPatientMajorInjuryResponse>> Handle(long patientId);
}