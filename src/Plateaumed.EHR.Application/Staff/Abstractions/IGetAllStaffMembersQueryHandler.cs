using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using Abp.Runtime.Session;
using Plateaumed.EHR.Staff.Dtos;

namespace Plateaumed.EHR.Staff.Abstractions;

/// <summary>
/// Handler to get a list of staff members.
/// </summary>
public interface IGetAllStaffMembersQueryHandler : ITransientDependency
{
    /// <summary>
    /// Handle the request.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<PagedResultDto<GetStaffMembersResponse>> Handle(GetAllStaffMembersRequest request);

    Task<List<GetStaffMembersSimpleResponseDto>> SearchStaffHandle(string searchFilter, bool isAnaethetist, IAbpSession abpSession);
}