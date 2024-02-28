using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Command
{
    public class CreateSurgicalHistoryCommandHandler : ICreateSurgicalHistoryCommandHandler
    {
        private readonly IRepository<SurgicalHistory, long> _surgicalHistoryRepository;
        private readonly IObjectMapper _mapper;

        public CreateSurgicalHistoryCommandHandler(IRepository<SurgicalHistory, long> surgicalHistoryRepository,
            IObjectMapper mapper)
        {
            _surgicalHistoryRepository = surgicalHistoryRepository;
            _mapper = mapper;
        }

        public async Task Handle(CreateSurgicalHistoryRequestDto request)
        {
            if(!string.IsNullOrWhiteSpace(request.Diagnosis) || !string.IsNullOrWhiteSpace(request.Procedure))
            {
                var surgicalHistory = _mapper.Map<SurgicalHistory>(request);
                await _surgicalHistoryRepository.InsertAsync(surgicalHistory);
            }
        }
    }
}
