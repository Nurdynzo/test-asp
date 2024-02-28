using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.ReviewAndSaves.Dtos;

namespace Plateaumed.EHR.ReviewAndSaves.Abstraction;

public interface INursingNoteQueryHandler : ITransientDependency
{
    Task<NursingNoteDto> Handle(long staffUserId, PatientEncounter encounter);
}