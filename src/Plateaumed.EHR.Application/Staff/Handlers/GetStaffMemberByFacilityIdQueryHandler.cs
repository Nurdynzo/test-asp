using System.Threading.Tasks;
using Abp.Domain.Repositories;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Facilities.Dtos;
using System.Collections.Generic;
using Plateaumed.EHR.Authorization.Roles;
using Plateaumed.EHR.Staff.Abstractions;

namespace Plateaumed.EHR.Staff.Handlers;

public class GetStaffMemberByFacilityIdQueryHandler : IGetStaffMemberByFacilityIdQueryHandler
{
    private readonly IRepository<FacilityStaff, long> _facilityStaffRepository;
    private readonly IRepository<JobTitle, long> _jobTitleRepository;

    public GetStaffMemberByFacilityIdQueryHandler(
        IRepository<FacilityStaff, long> facilityStaffRepository,
        IRepository<JobTitle, long> jobTitleRepository)
    {
        _facilityStaffRepository = facilityStaffRepository;
        _jobTitleRepository = jobTitleRepository;
    }

    public async Task<List<FacilityStaffDto>> Handle(long facilityId)
    {
        var allFacilityStaffMembers = await _facilityStaffRepository.GetAll()
                       .Include(u => u.FacilityFk)
                       .Include(u => u.StaffMemberFk)
                       .ThenInclude(j => j.Jobs)
                       .Include(u => u.StaffMemberFk)
                       .ThenInclude(j => j.UserFk)
                       .Include(u => u.StaffMemberFk.Jobs)
                       .ThenInclude(j => j.JobLevel.JobTitleFk)
                       .IgnoreQueryFilters()
                       .Where(x => x.FacilityId == facilityId)
                       .ToListAsync();

        var results = allFacilityStaffMembers.Select(staff =>
        {
            return new FacilityStaffDto
            {
                Id = staff.Id,
                FacilityId = staff.FacilityId,
                StaffMemberId = staff.StaffMemberId,
                StaffCode = staff.StaffMemberFk?.StaffCode,
                Title = staff.StaffMemberFk?.UserFk?.Title,
                Name = staff.StaffMemberFk?.UserFk?.Name,
                Surname = staff.StaffMemberFk?.UserFk?.Surname,
                MiddleName = staff.StaffMemberFk?.UserFk?.MiddleName,
                JobTitleId = staff.StaffMemberFk?.Jobs?.Select(j =>
                {
                    var jobTitleId = j.JobLevel?.JobTitleFk?.Id;
                    return jobTitleId.Value;

                }).FirstOrDefault()
            };
        }).ToList();


        var jobTitleId = _jobTitleRepository.GetAll()
                .Where(s => s.Name == StaticRoleNames.JobRoles.Doctor).Select(s => s.Id).FirstOrDefault();

        return results
            .Where(j => j.JobTitleId == jobTitleId)
            .ToList();
    }

}
