using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using Abp.UI;
using Plateaumed.EHR.Procedures.Abstractions;
using Plateaumed.EHR.Procedures.Dtos;
namespace Plateaumed.EHR.Procedures.Handlers
{
    public class CreateSpecializedProcedureNurseDetailCommandHandler : ICreateSpecializedProcedureNurseDetailCommandHandler
    {
        private readonly IRepository<SpecializedProcedureNurseDetail,long> _specializedProcedureNurseDetailRepository;
        private readonly IAbpSession _abpSession;
        private readonly IObjectMapper _objectMapper;

        public CreateSpecializedProcedureNurseDetailCommandHandler(
            IRepository<SpecializedProcedureNurseDetail, long> specializedProcedureNurseDetailRepository,
            IAbpSession abpSession,
            IObjectMapper objectMapper)
        {
            _specializedProcedureNurseDetailRepository = specializedProcedureNurseDetailRepository;
            _abpSession = abpSession;
            _objectMapper = objectMapper;
        }
        public async Task Handle(CreateSpecializedProcedureNurseDetailCommand request)
        {
            if(_abpSession.TenantId is null)
                throw new UserFriendlyException("User TenantId can not be null");
            var entity = _objectMapper.Map<SpecializedProcedureNurseDetail>(request);
            entity.TenantId = _abpSession.TenantId.Value;
            await _specializedProcedureNurseDetailRepository.InsertAsync(entity);
        }
    }
}
