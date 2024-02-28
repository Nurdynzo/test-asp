using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.InputNotes.Abstractions;
using Plateaumed.EHR.InputNotes.Dtos;

namespace Plateaumed.EHR.InputNotes.Query.BaseQueryHelper;

public class GetPatientInputNotesSummaryQueryHandler : IGetPatientInputNotesSummaryQueryHandler
{
    private readonly IBaseQuery _baseQuery;
    private readonly IRepository<User,long> _userRepository;
    
    public GetPatientInputNotesSummaryQueryHandler(
        IBaseQuery baseQuery,
        IRepository<User, long> userRepository)
    {
        _baseQuery = baseQuery;
        _userRepository = userRepository;
    }
    
    public async Task<List<InputNotesSummaryForReturnDto>> Handle(GetInputNotesDto input)
    {
        var query = await (
                from i in _baseQuery
                    .GetPatientInputNotesBaseQuery(
                    input.PatientId,
                    entryType:input.EntryType,
                    encounterId:input.EncounterId)
                    .WhereIf(input.ProcedureId.HasValue , a => a.ProcedureId == input.ProcedureId)
                join u in _userRepository.GetAll() on i.DeleterUserId equals u.Id into user
                from u in user.DefaultIfEmpty()
                orderby i.CreationTime descending
                select new InputNotesSummaryForReturnDto
                {
                    Id = i.Id,
                    CreationTime = i.CreationTime,
                    IsDeleted = i.IsDeleted,
                    Description = i.Description,
                    DeletionTime = i.DeletionTime,
                    DeletedUser = u != null ? u.DisplayName : "",
                    EntryType = i.EntryType,
                    ProcedureId = i.ProcedureId
                })
            .ToListAsync();
        return query;

    }
}
