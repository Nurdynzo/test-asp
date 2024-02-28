using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Investigations.Dto;

namespace Plateaumed.EHR.Investigations.Handlers
{
    public class GetInvestigationForPatientCommandHandler : IGetInvestigationForPatientCommandHandler
    {
        private readonly IRepository<InvestigationRequest, long> _investigationRequestRepository;
        private readonly IAbpSession _abpSession;

        public GetInvestigationForPatientCommandHandler(
            IRepository<InvestigationRequest, long> investigationRequestRepository,
            IAbpSession abpSession)
        {
            _investigationRequestRepository = investigationRequestRepository;
            _abpSession = abpSession;
        }

        public async Task<Dictionary<long, GetInvestigationForPatientResponse>> GetInvestigationForPatient(GetPatientInvestigationRequest request)
        {
            ValidatePatient(request);
            return await GetInvestigationRequest(request);
        }

        private void ValidatePatient(GetPatientInvestigationRequest request)
        {
            if (request.PatientId == 0) throw new UserFriendlyException("PatiendId not valid");

            if (request.InvestigationIds.Count <= 0) throw new UserFriendlyException("Investigations not found");
        }

        private async Task<Dictionary<long, GetInvestigationForPatientResponse>> GetInvestigationRequest(GetPatientInvestigationRequest request)
        {
            var investigationIds = request.InvestigationIds.Distinct();
            var dict = await _investigationRequestRepository
                .GetAll()
                .IgnoreQueryFilters()
                .Include(x => x.Investigation)
                .Where(x =>
                    x.PatientId == request.PatientId &&
                    investigationIds.Contains(x.InvestigationId) &&
                    x.TenantId == _abpSession.TenantId) // this is required to filter by current tenant id
                .OrderByDescending(x => x.CreationTime)
                .ToDictionaryAsync(
                    x => x.InvestigationId,
                    x => new GetInvestigationForPatientResponse
                    {
                        Status = x.InvestigationStatus ?? null,
                        PatientId = request.PatientId,
                        DateRequested = x.CreationTime,
                        NameOfInvestigation = x.Investigation?.Name,
                        IsDeleted = x.IsDeleted
                    }
                );

            return dict;
        }

    }
}
