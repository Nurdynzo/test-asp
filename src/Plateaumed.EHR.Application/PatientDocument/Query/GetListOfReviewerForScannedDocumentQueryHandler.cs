using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Authorization.Roles;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.PatientDocument.Abstraction;

namespace Plateaumed.EHR.PatientDocument.Query;

public class GetListOfReviewerForScannedDocumentQueryHandler : IGetListOfReviewerForScannedDocumentQueryHandler
{
    private readonly IRepository<User,long> _userRepository;
    private readonly IRepository<UserRole,long> _userRoleRepository;
    private readonly IRepository<Role> _roleRepository;
    
    public GetListOfReviewerForScannedDocumentQueryHandler(IRepository<User, long> userRepository,
        IRepository<UserRole, long> roleRepository, IRepository<Role> roleRepository1)
    {
        _userRepository = userRepository;
        _userRoleRepository = roleRepository;
        _roleRepository = roleRepository1;
    }

    public async Task<List<ScannedDocumentReviewerQueryResponse>> Handle()
    {
        var query = from u in _userRepository.GetAll()
                    join ur in _userRoleRepository.GetAll() on u.Id equals ur.UserId
                    join r in _roleRepository.GetAll() on ur.RoleId equals r.Id
                    where r.Name == StaticRoleNames.JobRoles.FrontDesk
                    select new ScannedDocumentReviewerQueryResponse
                    {
                        Id = u.Id,
                        FullName = u.FullName
                    };
        return await query.ToListAsync();
                    
    }
}