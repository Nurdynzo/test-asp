using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.PlanItems.Abstractions;
using Plateaumed.EHR.PlanItems.Dtos;

namespace Plateaumed.EHR.PlanItems.Query.BaseQueryHelper;

public class GetPatientPlanItemsSummaryQueryHandler : IGetPatientPlanItemsSummaryQueryHandler
{
    private readonly IBaseQuery _baseQuery;
    private readonly IRepository<User, long> _userRepository;
    
    public GetPatientPlanItemsSummaryQueryHandler(
        IBaseQuery baseQuery,
        IRepository<User, long> userRepository)
    {
        _baseQuery = baseQuery;
        _userRepository = userRepository;
    }
    
    public async Task<List<PlanItemsSummaryForReturnDto>> Handle(int patientId, int? tenantId, long? procedureId)
    {
        var query = await ( from p in _baseQuery
            .GetPatientPlanItemsBaseQuery(patientId, procedureId)
            join u in _userRepository.GetAll() on p.DeleterUserId equals u.Id into user
            from u in user.DefaultIfEmpty()
                    orderby p.Id descending
                    select new PlanItemsSummaryForReturnDto
                    {
                        Id = p.Id,
                        Description = p.Description,
                        CreationTime = p.CreationTime,
                        DeletionTime = p.DeletionTime,
                        IsDeleted = p.IsDeleted,
                        ProcedureId = p.ProcedureId,
                        ProcedureEntryType = p.ProcedureEntryType != null ?p.ProcedureEntryType.ToString():"",
                        DeletedUser = u == null ? "" : u.DisplayName

                    })
            .ToListAsync();
        return query;
    }
}
