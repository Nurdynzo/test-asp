using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Command
{
    public class CreateAlcoholHistoryCommandHandler : ICreateAlcoholHistoryCommandHandler
    {
        private readonly IRepository<AlcoholHistory, long> _alcoholHistoryRepository;
        private readonly IObjectMapper _mapper;

        public CreateAlcoholHistoryCommandHandler(IRepository<AlcoholHistory, long> alcoholHistoryRepository,
            IObjectMapper mapper)
        {
            _alcoholHistoryRepository = alcoholHistoryRepository;
            _mapper = mapper;
        }

        public async Task Handle(CreateAlcoholHistoryRequestDto request)
        {
            var newHistory = _mapper.Map<AlcoholHistory>(request);
            await _alcoholHistoryRepository.InsertAsync(newHistory);
        }

    }
}
