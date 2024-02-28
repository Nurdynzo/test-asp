using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using Abp.UI;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;
namespace Plateaumed.EHR.PatientProfile.Command
{
    public class SaveMenstrualBloodFlowCommandHandler : ISaveMenstrualBloodFlowCommandHandler
    {
        private readonly IRepository<PatientMenstrualFlow, long> _patientMenstrualFlowRepository;
        private readonly IAbpSession _abpSession;
        private readonly IObjectMapper _objectMapper;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public SaveMenstrualBloodFlowCommandHandler(
            IRepository<PatientMenstrualFlow, long> patientMenstrualFlowRepository,
            IAbpSession abpSession, 
            IObjectMapper objectMapper, 
            IUnitOfWorkManager unitOfWorkManager)
        {
            _patientMenstrualFlowRepository = patientMenstrualFlowRepository;
            _abpSession = abpSession;
            _objectMapper = objectMapper;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task<SaveMenstrualBloodFlowCommandRequest> Handle(SaveMenstrualBloodFlowCommandRequest request)
        {
            var patientMenstrualFlow = _objectMapper.Map<PatientMenstrualFlow>(request);
            if (_abpSession.TenantId is null)
            {
                throw new UserFriendlyException("TenantId cannot be null");
            }
            patientMenstrualFlow.TenantId = _abpSession.TenantId.Value;
            await _patientMenstrualFlowRepository.InsertAsync(patientMenstrualFlow);
            await _unitOfWorkManager.Current.SaveChangesAsync();
            request.Id = patientMenstrualFlow.Id;
            return request;
        }
    }
}