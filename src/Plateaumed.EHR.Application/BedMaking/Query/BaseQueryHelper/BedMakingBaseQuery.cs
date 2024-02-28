using System.Linq;
using Abp.Domain.Repositories;
using Plateaumed.EHR.BedMaking.Abstractions;

namespace Plateaumed.EHR.BedMaking.Query.BaseQueryHelper;

public class BedMakingBaseQuery : IBaseQuery
{
    private readonly IRepository<AllInputs.BedMaking, long> _bedMakingRepository; 
    
    public BedMakingBaseQuery(IRepository<AllInputs.BedMaking, long> bedMakingRepository)
    {
        _bedMakingRepository = bedMakingRepository;
    }
    
    public IQueryable<AllInputs.BedMaking> GetPatientBedMakingBaseQuery(int patientId, int? tenantId, bool isDeleted = false)
    {
        var query = (from a in _bedMakingRepository.GetAll()
            where a.PatientId == patientId && a.IsDeleted == isDeleted
            select a);
        
        return query;
    }
}