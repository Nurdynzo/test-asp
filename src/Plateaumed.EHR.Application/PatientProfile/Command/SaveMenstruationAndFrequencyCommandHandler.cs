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
    public class SaveMenstruationAndFrequencyCommandHandler : ISaveMenstruationAndFrequencyCommandHandler
    {
        private readonly IRepository<PatientMensurationDuration,long> _mensurationDurationRepository;
        private readonly IObjectMapper _objectMapper;
        private readonly IAbpSession _abpSession;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public SaveMenstruationAndFrequencyCommandHandler(
            IRepository<PatientMensurationDuration, long> mensurationDurationRepository, 
            IObjectMapper objectMapper, 
            IAbpSession abpSession,
            IUnitOfWorkManager unitOfWorkManager)
        {
            _mensurationDurationRepository = mensurationDurationRepository;
            _objectMapper = objectMapper;
            _abpSession = abpSession;
            _unitOfWorkManager = unitOfWorkManager;
        }
        public async Task<SaveMenstruationAndFrequencyCommand> Handle(SaveMenstruationAndFrequencyCommand request)
        {
            var item = _objectMapper.Map<PatientMensurationDuration>(request);
            if (_abpSession.TenantId is null)
            {
                throw new UserFriendlyException("TenantId cannot be null.");
            }
            item.TenantId = _abpSession.TenantId.Value;
            await _mensurationDurationRepository.InsertAsync(item);
            await _unitOfWorkManager.Current.SaveChangesAsync();
            request.Id = item.Id;
            return request;
        }
    }
}