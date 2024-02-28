using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.WardEmergencies.Dto;

namespace Plateaumed.EHR.WardEmergencies.Handlers;

public class GetPatientInterventionsQueryHandler : IGetPatientInterventionsQueryHandler
{
    private readonly IRepository<PatientIntervention, long> _patientInterventionRepository;
    private readonly IRepository<User,long> _userRepository;

    public GetPatientInterventionsQueryHandler(
        IRepository<PatientIntervention, long> patientInterventionRepository,
        IRepository<User, long> userRepository)
    {
        _patientInterventionRepository = patientInterventionRepository;
        _userRepository = userRepository;
    }

    public async Task<List<GetPatientInterventionsResponse>> Handle(GetPatientInterventionsRequest request)
    {
        var interventions = await (from i in _patientInterventionRepository.GetAll().IgnoreQueryFilters()
            .Include(x => x.Event)
            .Include(x => x.InterventionEvents)
            .ThenInclude(x => x.NursingIntervention)
            .Where(x => x.PatientId == request.PatientId)
            join u in _userRepository.GetAll() on i.DeleterUserId equals u.Id into user
            from u in user.DefaultIfEmpty()
            select new {i,u})
            .ToListAsync();

        return interventions
            .OrderByDescending(v => v.i.CreationTime)
            .Select(x => new GetPatientInterventionsResponse
            {
                Id = x.i.Id,
                Time = x.i.CreationTime,
                Event = x.i.Event?.Event ?? x.i.EventText,
                Interventions = x.i.InterventionEvents.Select(y => y.InterventionText)
                    .Concat(x.i.InterventionEvents.Select(y => y?.NursingIntervention?.Name))
                    .Where(z => !string.IsNullOrWhiteSpace(z)).ToList(),
                DeletedUser = x.u != null ? x.u.DisplayName : "",
                IsDeleted = x.i.IsDeleted

            }).ToList();

    }
}
