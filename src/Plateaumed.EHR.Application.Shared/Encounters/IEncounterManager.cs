using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Encounters.Dto;
using Plateaumed.EHR.Misc;

namespace Plateaumed.EHR.Encounters;

public interface IEncounterManager : ITransientDependency
{
    Task AdmitPatient(CreateAdmissionEncounterRequest request);

    Task CheckEncounterExists(long encounterId);

    Task RequestTransferPatient(long encounterId, long wardId, long? wardBedId, PatientStabilityStatus status);

    Task CompleteTransferPatient(long encounterId);

    Task RequestDischargePatient(long encounterId);

    Task CompleteDischargePatient(long encounterId);

    Task MarkAsDeceased(long encounterId);

    Task CreateAppointmentEncounter(CreateAppointmentEncounterRequest request);
}