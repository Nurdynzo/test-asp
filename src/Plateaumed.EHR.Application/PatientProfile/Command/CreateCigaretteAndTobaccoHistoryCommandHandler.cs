using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Command
{
    public class CreateCigaretteAndTobaccoHistoryCommandHandler : ICreateCigaretteAndTobaccoHistoryCommandHandler
    {
        private readonly IRepository<CigeretteAndTobaccoHistory, long> _cigaretteAndTobaccoHistoryRepository;
        private readonly IObjectMapper _mapper;

        public CreateCigaretteAndTobaccoHistoryCommandHandler(IRepository<CigeretteAndTobaccoHistory, long> cigaretteAndTobaccoHistoryRepository,
            IObjectMapper mapper)
        {
            _mapper = mapper;
            _cigaretteAndTobaccoHistoryRepository = cigaretteAndTobaccoHistoryRepository;
        }

        public async Task Handle(CreateCigaretteHistoryRequestDto request)
        {
            var newRecord = _mapper.Map<CigeretteAndTobaccoHistory>(request);
            await _cigaretteAndTobaccoHistoryRepository.InsertAsync(newRecord).ConfigureAwait(false);
        }
    }
}
