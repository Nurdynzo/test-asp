using System.Linq;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.Runtime.Session;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.InputNotes.Abstractions;
using Plateaumed.EHR.Procedures;

namespace Plateaumed.EHR.InputNotes.Query.BaseQueryHelper;

public class InputNotesBaseQuery : IBaseQuery
{
    private readonly IRepository<AllInputs.InputNotes, long> _inputNotesRepository;
    private readonly IAbpSession _abpSession;

    public InputNotesBaseQuery(
        IRepository<AllInputs.InputNotes, long> inputNotesRepository,
        IAbpSession abpSession)
    {
        _inputNotesRepository = inputNotesRepository;
        _abpSession = abpSession;
    }

    public IQueryable<AllInputs.InputNotes> GetPatientInputNotesBaseQuery(
        long patientId,
        bool isDeleted = false,
        ProcedureEntryType? entryType = null,
        long encounterId = 0)
    {
        return _inputNotesRepository.GetAll()
            .IgnoreQueryFilters()
            .Where(a =>
                a.PatientId == patientId &&
                a.TenantId == _abpSession.TenantId &&
                a.IsDeleted == isDeleted)
            .WhereIf(entryType.HasValue, a =>a.EntryType == entryType)
            .WhereIf(encounterId > 0, a=>a.EncounterId == encounterId);
    }
    public IQueryable<AllInputs.InputNotes> GetPatientInputNotesByEncounter(long patientId, long encounterId, int tenantId, bool isDeleted = false)
    {
        return _inputNotesRepository.GetAll()
            .IgnoreQueryFilters()
            .Where(a =>
                a.EncounterId == encounterId &&
                a.PatientId == patientId &&
                a.TenantId == tenantId &&
                a.IsDeleted == isDeleted);
    }
}
