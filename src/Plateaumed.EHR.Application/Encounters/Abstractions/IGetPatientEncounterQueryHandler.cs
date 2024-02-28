using Abp.Dependency;
using Plateaumed.EHR.Facilities.Dtos;
using Plateaumed.EHR.Patients;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Encounters.Abstractions
{
    public interface IGetPatientEncounterQueryHandler : ITransientDependency
    {
        Task<PatientEncounter> Handle(long encounterId);
    }
}