using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Plateaumed.EHR.Staff;
using System.Linq;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Facilities.Abstractions;

namespace Plateaumed.EHR.Facilities.Handler;

public class GetCurrentUserFacilityIdQueryHandler : IGetCurrentUserFacilityIdQueryHandler
{
    private readonly IRepository<StaffMember, long> _staffMemberRepository;
    private readonly IRepository<FacilityStaff, long> _facilityStaffRepository;
    private readonly IAbpSession _abpSession;

    public GetCurrentUserFacilityIdQueryHandler(IRepository<StaffMember, long> staffMemberRepository,
        IRepository<FacilityStaff, long> facilityStaffRepository, IAbpSession abpSession)
    {
        _staffMemberRepository = staffMemberRepository;
        _facilityStaffRepository = facilityStaffRepository;
        _abpSession = abpSession;
    }

    public async Task<long?> Handle(long userId)
    {
        return await GetUserFacilityId(userId);
    }

    public async Task<long?> Handle()
    {
        var userId = _abpSession.UserId ?? throw new UserFriendlyException("User not currently logged in");
        return await GetUserFacilityId(userId);
    }

    private async Task<long?> GetUserFacilityId(long userId) 
    {
        var facilityId = await (from f in _staffMemberRepository.GetAll()
            join s in _facilityStaffRepository.GetAll() on f.Id equals s.StaffMemberId
            where f.UserId == userId && s.IsDefault
            select s.FacilityId).FirstOrDefaultAsync();
        return facilityId;
    }
}