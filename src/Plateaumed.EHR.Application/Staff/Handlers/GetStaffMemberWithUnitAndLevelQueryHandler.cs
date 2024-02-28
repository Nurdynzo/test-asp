using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.ObjectMapping;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Authorization.Users.Dto;
using Plateaumed.EHR.Organizations.Dto;
using Plateaumed.EHR.Staff.Abstractions;
using Plateaumed.EHR.Staff.Dtos;

namespace Plateaumed.EHR.Staff.Handlers;

public class GetStaffMemberWithUnitAndLevelQueryHandler : IGetStaffMemberWithUnitAndLevelQueryHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IObjectMapper _objectMapper;

    public GetStaffMemberWithUnitAndLevelQueryHandler(IUserRepository userRepository, IObjectMapper objectMapper)
    {
        _objectMapper = objectMapper;
        _userRepository = userRepository;
    }

    public async Task<GetStaffMemberResponse> Handle(EntityDto<long> input)
    {
        var user = await _userRepository.GetAll()
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
                       .ThenInclude(j => j.Unit)
                       .FirstOrDefaultAsync(x => x.Id == input.Id) ??
                   throw new UserFriendlyException("User does not exist");

        if (user.StaffMemberFk == null) throw new UserFriendlyException("User is not a staff member");

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
                jobDto.JobTitle = new JobTitleDto()
                {
                    Id = j.JobLevel?.JobTitleFk?.Id ?? 0,
                    Name = j.JobLevel?.JobTitleFk?.Name,
                    ShortName = j.JobLevel?.JobTitleFk?.ShortName,
                    FacilityId = j.JobLevel?.JobTitleFk?.FacilityId ?? 0
                };
                jobDto.TeamRole = j.TeamRole?.Name;
                jobDto.UnitId = j.UnitId;
                jobDto.Unit = new OrganizationUnitDto()
                {
                    ParentId = j.Unit?.ParentId,
                    Code = j.Unit?.Code,
                    DisplayName = j.Unit?.DisplayName,
                    ShortName = j.Unit?.ShortName,
                    IsActive = j.Unit?.IsActive,
                    FacilityId = j.Unit?.FacilityId,
                }; 
                jobDto.JobLevelId = j.JobLevelId;
                jobDto.JobLevel = new JobLevelDto()
                {
                    Name = j.JobLevel?.Name,
                    Rank = j.JobLevel?.Rank,
                    ShortName = j.JobLevel?.ShortName,
                    JobTitleId = j.JobLevel?.JobTitleId
                };
                return jobDto;

            }).ToList()
        };
    }

}
