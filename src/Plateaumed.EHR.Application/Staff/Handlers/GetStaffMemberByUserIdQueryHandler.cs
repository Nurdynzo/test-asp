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

public class GetStaffMemberByUserIdQueryHandler : IGetStaffMemberByUserIdQueryHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IObjectMapper _objectMapper;

    public GetStaffMemberByUserIdQueryHandler(IUserRepository userRepository, IObjectMapper objectMapper)
    {
        _objectMapper = objectMapper;
        _userRepository = userRepository;
    }

    public async Task<User> Handle(long userId)
    {

        var user = await _userRepository.GetAll()
                       .Include(u => u.StaffMemberFk)
                       .Include(u => u.StaffMemberFk)
                       .ThenInclude(j => j.AssignedFacilities)
                       .Include(u => u.StaffMemberFk)
                       .ThenInclude(j => j.Jobs)
                       .FirstOrDefaultAsync(x => x.Id == userId) ??
                   throw new UserFriendlyException("User does not exist");
        return user;
    }

}
