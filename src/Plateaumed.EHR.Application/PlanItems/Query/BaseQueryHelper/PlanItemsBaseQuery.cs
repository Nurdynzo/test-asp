using System.Linq;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.Runtime.Session;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PlanItems.Abstractions;

namespace Plateaumed.EHR.PlanItems.Query.BaseQueryHelper;

public class PlanItemsBaseQuery : IBaseQuery
{
    private readonly IRepository<AllInputs.PlanItems, long> _planItemsRepository;
    private readonly IAbpSession _abpSession;
    
    public PlanItemsBaseQuery(IRepository<AllInputs.PlanItems, long> planItemsRepository,
        IAbpSession abpSession)
    {
        _planItemsRepository = planItemsRepository;
        _abpSession = abpSession;
    }
    
    public IQueryable<AllInputs.PlanItems> GetPatientPlanItemsBaseQuery(int patientId, long? procedureId = null, bool? isDeleted = null)
    {
        return _planItemsRepository.GetAll()
            .IgnoreQueryFilters()
            .Where(a => a.PatientId == patientId && (isDeleted == null || a.IsDeleted == isDeleted) && a.TenantId == _abpSession.TenantId)
            .WhereIf(procedureId.HasValue, a => a.ProcedureId == procedureId);
    }
}
