using System.Threading.Tasks;
using Abp.Authorization;
using Plateaumed.EHR.Authorization;
using Plateaumed.EHR.Staff.Abstractions;
using Plateaumed.EHR.Staff.Dtos;

namespace Plateaumed.EHR.Staff
{
    [AbpAuthorize(AppPermissions.Pages_StaffMembers)]
    public class JobAppService : EHRAppServiceBase, IJobAppService
    {
        private readonly ICreateJobCommandHandler _createJob;
        private readonly IUpdateJobCommandHandler _updateJob;

        public JobAppService(ICreateJobCommandHandler createJob, IUpdateJobCommandHandler updateJob)
        {
            _createJob = createJob;
            _updateJob = updateJob;
        }

        [AbpAuthorize(AppPermissions.Pages_StaffMembers_Create)]
        public async Task Create(CreateOrEditJobRequest request)
        {
            await _createJob.Handle(request);
        }

        [AbpAuthorize(AppPermissions.Pages_StaffMembers_Create)]
        public async Task Update(CreateOrEditJobRequest request)
        {
            await _updateJob.Handle(request);
        }
    }
}
