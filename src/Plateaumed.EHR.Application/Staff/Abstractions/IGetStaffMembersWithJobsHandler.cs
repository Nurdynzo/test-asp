using Abp.Application.Services.Dto;
using Abp.Dependency;
using Plateaumed.EHR.Staff.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Staff.Abstractions
{
    public interface IGetStaffMembersWithJobsHandler : ITransientDependency
    {
        Task<PagedResultDto<GetStaffMemberResponse>> Handle(GetStaffMembersWithJobsRequest input);
    }
}
