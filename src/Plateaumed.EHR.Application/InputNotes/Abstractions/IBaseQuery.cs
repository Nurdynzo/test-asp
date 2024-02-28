using System.Linq;
using Abp.Dependency;
using Plateaumed.EHR.Procedures;

namespace Plateaumed.EHR.InputNotes.Abstractions;

public interface IBaseQuery : ITransientDependency
{
    IQueryable<AllInputs.InputNotes> GetPatientInputNotesBaseQuery(
        long patientId,
        bool isDeleted = false,
        ProcedureEntryType? entryType = null,
        long encounterId = 0);
    IQueryable<AllInputs.InputNotes> GetPatientInputNotesByEncounter(long patientId, long encounterId, int tenantId, bool isDeleted = false);
}
