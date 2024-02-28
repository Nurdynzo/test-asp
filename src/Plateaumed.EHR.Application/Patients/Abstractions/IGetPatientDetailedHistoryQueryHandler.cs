using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Patients.Dtos;

namespace Plateaumed.EHR.Patients.Abstractions;

/// <summary>
/// Query Family History
/// </summary>
public interface IGetPatientDetailedHistoryQueryHandler: ITransientDependency
{
    /// <summary>
    /// Handler for GetPatientDetailedHistory
    /// </summary>
    /// <param name="patientId"></param>
    /// <param name="facilityId"></param>
    /// <returns></returns>
    Task<PatientDetailsQueryResponse> Handle(long patientId, long facilityId);
}