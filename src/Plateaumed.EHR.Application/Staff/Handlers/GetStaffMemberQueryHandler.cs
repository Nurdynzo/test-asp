using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Authorization.Users.Dto;
using Plateaumed.EHR.Facilities.Dtos;
using Plateaumed.EHR.Misc.Country;
using Plateaumed.EHR.Organizations.Dto;
using Plateaumed.EHR.Staff.Abstractions;
using Plateaumed.EHR.Staff.Dtos;

namespace Plateaumed.EHR.Staff.Handlers;

public class GetStaffMemberQueryHandler : IGetStaffMemberQueryHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IObjectMapper _objectMapper;
    private readonly IRepository<Country, int> _countryRepository;

    public GetStaffMemberQueryHandler(IUserRepository userRepository, IObjectMapper objectMapper, 
        IRepository<Country, int> countryRepository)
    {
        _objectMapper = objectMapper;
        _countryRepository = countryRepository;
        _userRepository = userRepository;
    }

    public async Task<GetStaffMemberForEditResponse> Handle(EntityDto<long> input)
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
                       .ThenInclude(j => j.Ward)
                       .Include(u => u.StaffMemberFk.Jobs)
                       .ThenInclude(u => u.Department)
                       .Include(u => u.StaffMemberFk.Jobs)
                       .ThenInclude(j => j.Unit)
                       .FirstOrDefaultAsync(x => x.Id == input.Id) ??

                   throw new UserFriendlyException("User does not exist");

        if (user.StaffMemberFk == null) throw new UserFriendlyException("User is not a staff member");

        string phoneCode = null;
        if (user.CountryId != null)
        {
            var countryPhoneCode = await _countryRepository.FirstOrDefaultAsync(c => c.Id == user.CountryId);
            phoneCode = countryPhoneCode?.PhoneCode;
        }

        return new GetStaffMemberForEditResponse
        {
            User = _objectMapper.Map<UserEditDto>(user),
            StaffMemberId = user.StaffMemberFk.Id,
            StaffCode = user.StaffMemberFk.StaffCode,
            PhoneCode = phoneCode,
            ContractStartDate = user.StaffMemberFk.ContractStartDate,
            ContractEndDate = user.StaffMemberFk.ContractEndDate,
            AdminRole = user.StaffMemberFk.AdminRole?.Name,
            Jobs = user.StaffMemberFk.Jobs.Select(j =>
            {
                var jobDto = _objectMapper.Map<GetStaffJob>(j);
                jobDto.ServiceCentres = j.JobServiceCentres.Select(js => js.ServiceCentre).ToList();
                jobDto.StaffWards = j.WardsJobs.Select(wj => _objectMapper.Map<WardDto>(wj.Ward)).ToList();
                jobDto.JobTitleId = j.JobLevel?.JobTitleFk?.Id;
                jobDto.Wards = jobDto.Wards = j.WardsJobs.Select(jw => jw.WardId).ToList();
                jobDto.TeamRole = j.TeamRole?.Name;
                jobDto.UnitId = j.UnitId;
                jobDto.Unit = _objectMapper.Map<OrganizationUnitDto>(j.Unit);
                jobDto.DepartmentId = j.DepartmentId;
                jobDto.Department = _objectMapper.Map<OrganizationUnitDto>(j.Department);

                return jobDto;

            }).ToList()
        };
    }

}