using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.ReviewAndSaves.Dtos;

namespace Plateaumed.EHR.ReviewAndSaves.Abstraction;

public interface IDoctorsSummaryQueryHandler : ITransientDependency
{
    Task<DoctorSummaryDto> Handle(long doctorUserId, bool isOnBehalfOf, PatientEncounter encounter);
}
