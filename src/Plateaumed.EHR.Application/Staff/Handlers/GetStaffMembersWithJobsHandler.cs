using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Linq.Extensions;
using Abp.ObjectMapping;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Authorization.Users.Dto;
using Plateaumed.EHR.Staff.Abstractions;
using Plateaumed.EHR.Staff.Dtos;

namespace Plateaumed.EHR.Staff.Handlers
{
    public class GetStaffMembersWithJobsHandler : IGetStaffMembersWithJobsHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IObjectMapper _objectMapper;

        public GetStaffMembersWithJobsHandler(IUserRepository userRepository, IObjectMapper objectMapper)
        {
            _objectMapper = objectMapper;
            _userRepository = userRepository;
        }

        public async Task<PagedResultDto<GetStaffMemberResponse>> Handle(GetStaffMembersWithJobsRequest request)
        {
            var filterTerms = !string.IsNullOrWhiteSpace(request.Filter) ? request.Filter.ToLower().Split(" ") : null;

            var filteredStaffMembers = _userRepository.GetAll()
                           .Include(u => u.StaffMemberFk.AdminRole)
                           .Include(u => u.StaffMemberFk.Jobs)
                           .ThenInclude(j => j.TeamRole)
                           .Include(u => u.StaffMemberFk.Jobs)
                           .ThenInclude(j => j.JobServiceCentres)
                           .Include(u => u.StaffMemberFk.Jobs)
                           .ThenInclude(j => j.JobLevel.JobTitleFk)
                           .Include(u => u.StaffMemberFk.Jobs)
                           .ThenInclude(j => j.WardsJobs)
                           .Include(u => u.StaffMemberFk.Jobs)
                           .ThenInclude(j => j.Department)
                           .Include(u => u.StaffMemberFk.Jobs)
                           .ThenInclude(j => j.Facility)
                           .Include(u => u.StaffMemberFk.Jobs)
                           .ThenInclude(j => j.Unit)
                           .Include(u => u.StaffMemberFk.Jobs)
                            
                           .Include(j => j.StaffMemberFk.AssignedFacilities)
                           .Where(u => u.StaffMemberFk != null)
                           .WhereIf(
                                !string.IsNullOrWhiteSpace(request.Filter),
                                u => (u.Name.ToLower() + " " + (!string.IsNullOrEmpty(u.MiddleName) ? u.MiddleName + " " : string.Empty) + u.Surname.ToLower()).Contains(request.Filter.ToLower())
                            )
                           .WhereIf(
                                !string.IsNullOrWhiteSpace(request.StaffCodeFilter),
                                u => u.StaffMemberFk.StaffCode.ToLower().Contains(request.StaffCodeFilter.ToLower())
                            )
                           .WhereIf(
                                request.FacilityIdFilter.HasValue,
                                u => u.StaffMemberFk.AssignedFacilities.Any(f => f.FacilityId == request.FacilityIdFilter)
                            );

            var pagedAndFilteredStaffMembers = filteredStaffMembers
                .OrderBy(request.Sorting ?? "id asc")
                .PageBy(request);

            var totalCount = await filteredStaffMembers.CountAsync();

            var staffMembers = await pagedAndFilteredStaffMembers.ToListAsync();

            var results = staffMembers.Select(user =>
            {
                return new GetStaffMemberResponse
                {
                    User = _objectMapper.Map<UserEditDto>(user),
                    StaffCode = user.StaffMemberFk.StaffCode,
                    ContractStartDate = user.StaffMemberFk.ContractStartDate,
                    ContractEndDate = user.StaffMemberFk.ContractEndDate,
                    AdminRole = user.StaffMemberFk.AdminRole?.Name,
                    Jobs = user.StaffMemberFk.Jobs.Select(j =>
                    {
                        var jobDto = _objectMapper.Map<JobDto>(j);
                        jobDto.ServiceCentres = j.JobServiceCentres.Select(js => js.ServiceCentre).ToList();
                        jobDto.Wards = j.WardsJobs.Select(jw => jw.WardId).ToList();
                        jobDto.JobTitleId = j.JobLevel?.JobTitleFk?.Id;
                        jobDto.TeamRole = j.TeamRole?.Name;
                        jobDto.JobLevel = _objectMapper.Map<JobLevelDto>(j.JobLevel);
                        jobDto.JobLevelId = j.JobLevel?.Id;
                        if(j.JobLevel != null && j.JobLevel.JobTitleFk != null)
                        {
                            jobDto.JobTitle = _objectMapper.Map<JobTitleDto>(j.JobLevel.JobTitleFk);
                        }
                        return jobDto;

                    }).ToList()
                };
            }).ToList();

            return new PagedResultDto<GetStaffMemberResponse>(totalCount, results);
        }

    }
}
