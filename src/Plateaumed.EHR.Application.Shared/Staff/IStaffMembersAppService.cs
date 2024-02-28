using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Staff.Dtos;

namespace Plateaumed.EHR.Staff
{
    public interface IStaffMembersAppService : IApplicationService
    {
        Task<PagedResultDto<GetStaffMembersResponse>> GetAll(GetAllStaffMembersRequest request);
        Task<PagedResultDto<GetStaffMemberResponse>> GetAllStaffWithJobs(GetStaffMembersWithJobsRequest request);
        Task<GetStaffMemberForEditResponse> GetStaffMember(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditStaffMemberRequest input);

        Task Delete(EntityDto<long> input);
    }
}