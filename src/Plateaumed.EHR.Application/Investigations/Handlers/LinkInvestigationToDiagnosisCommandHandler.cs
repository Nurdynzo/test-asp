using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using Plateaumed.EHR.Diagnoses;
using Plateaumed.EHR.Investigations.Dto;

namespace Plateaumed.EHR.Investigations.Handlers
{
    public class LinkInvestigationToDiagnosisCommandHandler : ILinkInvestigationToDiagnosisCommandHandler
    {
        private readonly IRepository<InvestigationRequest, long> _investigationRequestRepository;
        private readonly IRepository<Diagnosis, long> _diagnosisRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public LinkInvestigationToDiagnosisCommandHandler(
            IRepository<InvestigationRequest, long> investigationRequestRepository,
            IRepository<Diagnosis, long> diagnosisRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _investigationRequestRepository = investigationRequestRepository;
            _diagnosisRepository = diagnosisRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task Handle(LinkInvestigationToDiagnosisRequest request)
        {
            var diagnosis = await _diagnosisRepository.GetAsync(request.DiagnosisId) ??
                            throw new UserFriendlyException("Diagnosis not found");
            var investigationRequest = await _investigationRequestRepository.GetAsync(request.InvestigationRequestId) ??
                                       throw new UserFriendlyException("Investigation request not found");
            investigationRequest.Diagnosis = diagnosis;
            await _investigationRequestRepository.UpdateAsync(investigationRequest);
            await _unitOfWorkManager.Current.SaveChangesAsync();
        }
    }
}
