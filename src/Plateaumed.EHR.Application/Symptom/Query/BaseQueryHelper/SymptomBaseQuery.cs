using System.Linq;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Symptom.Abstractions;

namespace Plateaumed.EHR.Symptom.Query.BaseQueryHelper;

public class SymptomBaseQuery : IBaseQuery
{
    private readonly IRepository<AllInputs.Symptom, long> _symptomRepository; 
    
    public SymptomBaseQuery(IRepository<AllInputs.Symptom, long> symptomRepository)
    {
        _symptomRepository = symptomRepository;
    }
    
    public IQueryable<AllInputs.Symptom> GetPatientSymptomsBaseQuery(long patientId, int? tenantId, long? encounterId = null)
    {
        var query = (from a in _symptomRepository.GetAll().IgnoreQueryFilters()
            where a.PatientId == patientId && a.TenantId == tenantId
            select a);
        if(encounterId != null )
        {
            query = query.Where(a=>a.EncounterId == encounterId);
        }
        return query;
    }
}
