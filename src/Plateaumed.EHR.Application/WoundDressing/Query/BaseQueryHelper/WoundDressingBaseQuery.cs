using System.Linq;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Plateaumed.EHR.WoundDressing.Abstractions;

namespace Plateaumed.EHR.WoundDressing.Query.BaseQueryHelper;

public class WoundDressingBaseQuery : IBaseQuery
{
    private readonly IRepository<AllInputs.WoundDressing, long> _woundDressingRepository;

    public WoundDressingBaseQuery(IRepository<AllInputs.WoundDressing, long> woundDressingRepository)
    {
        _woundDressingRepository = woundDressingRepository;
    }

    public IQueryable<AllInputs.WoundDressing> GetPatientWoundDressingBaseQuery(int patientId, bool isDeleted = false)
    {
        return _woundDressingRepository.GetAll()
            .Where(a => a.PatientId == patientId && a.IsDeleted == isDeleted);
    }
}