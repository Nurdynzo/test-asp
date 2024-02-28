using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Investigations.Abstractions;
using Plateaumed.EHR.Investigations.Dto;
using Plateaumed.EHR.Staff;

namespace Plateaumed.EHR.Investigations.Handlers
{
    public class CreateInvestigationResultReviewerHandler: EHRAppServiceBase, ICreateInvestigationResultReviewerHandler
    {
        private readonly IRepository<InvestigationResultReviewer, long> _investigationResultReviewer;
        private readonly IRepository<InvestigationResult, long> _investigationResult;
        private readonly IRepository<StaffMember, long> _staffMembers;
        private readonly IAbpSession _abpSession;
        private readonly IRepository<ElectroRadPulmInvestigationResult, long> _investigationResultERadPlum;

        public CreateInvestigationResultReviewerHandler(IRepository<InvestigationResultReviewer, long> investigationResultReviewer,
            IAbpSession abpSession,
            IRepository<InvestigationResult, long> investigationResult,
            IRepository<StaffMember, long> staffMembers,
            IRepository<ElectroRadPulmInvestigationResult, long> investigationResultERadPlum)
        {
            _investigationResultReviewer = investigationResultReviewer;
            _abpSession = abpSession;
            _investigationResult = investigationResult;
            _staffMembers = staffMembers;
            _investigationResultERadPlum = investigationResultERadPlum;
        }

        public async Task Handle(InvestigationResultReviewerRequestDto request, long facilityId)
        {
            await ValidateInputs(request);

            var tenantId = _abpSession.TenantId ?? throw new UserFriendlyException("Tenant not found");

            if (request.Id > 0)
            {
                var query = await _investigationResultReviewer.GetAsync(request.Id) ?? throw new UserFriendlyException("Invalid Investigation Result Reviewer Id");
                query.ReviewerId = request.ReviewerId;
                await _investigationResultReviewer.UpdateAsync(query);
            }
            else
            {
                await _investigationResultReviewer.InsertAsync(new InvestigationResultReviewer
                {
                    TenantId = tenantId,
                    ReviewerId = request.ReviewerId,
                    InvestigationResultId = request.InvestigationResultId,
                    ElectroRadPulmInvestigationResultId = request.ElectroRadPulmInvestigationResultId,
                    FacilityId = facilityId
                });
            }
        }

        private async Task ValidateInputs(InvestigationResultReviewerRequestDto request)
        {
            if(!request.ElectroRadPulmInvestigationResultId.HasValue && !request.InvestigationResultId.HasValue)
                throw new UserFriendlyException("No investigation result selected for review");

            if (request.InvestigationResultId.HasValue)
                _ = await _investigationResult.GetAll().Where(x => x.Id == request.InvestigationResultId).FirstOrDefaultAsync() ?? throw new UserFriendlyException("Investigation Result not found");

            if(request.ElectroRadPulmInvestigationResultId.HasValue)
                _ = await _investigationResultERadPlum.GetAll().Where(x=>x.Id == request.ElectroRadPulmInvestigationResultId).FirstOrDefaultAsync() ??
                    throw new UserFriendlyException("Investigation Result not found for Electrophysiology and Rad+Pulm");

            if (request.ReviewerId.HasValue)
                _ = await _staffMembers.GetAll().Where(x => x.Id == request.ReviewerId).FirstOrDefaultAsync() ?? throw new UserFriendlyException("Staff Member not found");
        }
    }
}

