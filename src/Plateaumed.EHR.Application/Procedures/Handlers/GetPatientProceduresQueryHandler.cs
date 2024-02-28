using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Plateaumed.EHR.Procedures.Abstractions;
using Plateaumed.EHR.Procedures.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Abp.Runtime.Session;
using Abp.UI;
using Newtonsoft.Json;
using Plateaumed.EHR.Authorization.Users;

namespace Plateaumed.EHR.Procedures.Handlers;

public class GetPatientProceduresQueryHandler : IGetPatientProceduresQueryHandler
{
    private readonly IRepository<Procedure, long> _procedureRepository;
    private readonly IRepository<SpecializedProcedure, long> _specializedProcedureRepository;  
    private readonly IRepository<ScheduleProcedure, long> _scheduledProcedureRepository;
    private readonly IAbpSession _abpSession;
    private readonly IRepository<User, long> _userRepository;
    
    public GetPatientProceduresQueryHandler(
        IRepository<Procedure, long> procedureRepository,
        IRepository<SpecializedProcedure, long> specializedProcedureRepository,
        IRepository<ScheduleProcedure, long> scheduledProcedureRepository,
        IAbpSession abpSession,
        IRepository<User, long> userRepository)
    {
        _procedureRepository = procedureRepository;
        _specializedProcedureRepository = specializedProcedureRepository;
        _scheduledProcedureRepository = scheduledProcedureRepository;
        _abpSession = abpSession;
        _userRepository = userRepository;
    }
    
    public async Task<List<PatientProcedureResponseDto>> Handle(long patientId, string procedureType, long? parentProcedureId)
    {
        var tenantId = _abpSession.TenantId;
        var canParse = Enum.TryParse(procedureType, out ProcedureType type);
        if (!canParse)
        {
            throw new UserFriendlyException("Invalid procedure type");
        }
        var query = from p in _procedureRepository.GetAll().IgnoreQueryFilters()
                    join u in _userRepository.GetAll() on p.DeleterUserId equals u.Id into user
                    from u in user.DefaultIfEmpty()
                    where p.PatientId == patientId &&
                          p.ProcedureType == type &&
                          (!parentProcedureId.HasValue || p.ParentProcedureId == parentProcedureId) &&
                          p.TenantId == tenantId
                    orderby p.Id descending
                    select new PatientProcedureResponseDto
                    {
                        Id = p.Id,
                        PatientId = p.PatientId,
                        ProcedureType = p.ProcedureType.ToString(),
                        Note = p.Note,
                        CreationTime = p.CreationTime,
                        IsDeleted = p.IsDeleted,
                        SnowmedId = p.SnowmedId,
                        SelectedProcedures = p.SelectedProcedures != null ? JsonConvert.DeserializeObject<List<SelectedProcedureDto>>(p.SelectedProcedures) : new List<SelectedProcedureDto>(),
                        ProcedureEntryType = p.ProcedureEntryType.ToString(),
                        DeletedUser = u != null ? u.DisplayName : "",
                        ScheduledProcedures = _scheduledProcedureRepository.GetAll().Include(x => x.Rooms)
                            .IgnoreQueryFilters()
                            .Where(x => x.TenantId == tenantId && x.ProcedureId == p.Id)
                            .Select(s => new ScheduledProcedureResponseDto
                            {
                                Id = s.Id,
                                CreationTime = s.CreationTime,
                                SnowmedId = s.SnowmedId,
                                ProcedureId = s.ProcedureId,
                                Duration = s.Duration,
                                Time = s.Time,
                                ProcedureName = s.ProcedureName,
                                ProposedDate = s.ProposedDate,
                                RequireAnaesthetist = s.RequireAnaesthetist,
                                RoomId = s.RoomId,
                                RoomName = s.Rooms.Name,
                                AnaesthetistUserId = s.CreatorUserId,
                                IsProcedureInSameSession = s.IsProcedureInSameSession,
                                IsDeleted = s.IsDeleted
                            }).ToList(),
                        SpecializedProcedures = _specializedProcedureRepository.GetAll().Include(x => x.Rooms)
                            .IgnoreQueryFilters()
                            .Where(x => x.TenantId == tenantId && x.ProcedureId == p.Id)
                            .Select(s => new SpecializedProcedureResponseDto
                            {
                                Id = s.Id,
                                CreationTime = s.CreationTime,
                                SnowmedId = s.SnowmedId,
                                ProcedureId = s.ProcedureId,
                                Duration = s.Duration,
                                Time = s.Time,
                                ProcedureName = s.ProcedureName,
                                ProposedDate = s.ProposedDate,
                                RequireAnaesthetist = s.RequireAnaesthetist,
                                RoomId = s.RoomId,
                                RoomName = s.Rooms.Name,
                                AnaesthetistUserId = s.CreatorUserId,
                                IsProcedureInSameSession = s.IsProcedureInSameSession,
                                IsDeleted = s.IsDeleted,

                            }).ToList()

                    };
        return await query.ToListAsync();



    }
}
