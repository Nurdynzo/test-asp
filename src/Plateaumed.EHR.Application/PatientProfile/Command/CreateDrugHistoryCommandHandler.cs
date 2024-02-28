using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Command
{
    public class CreateDrugHistoryCommandHandler : ICreateDrugHistoryCommandHandler
    {
        private readonly IRepository<DrugHistory, long> _drugHistoryRepository;
        private readonly IObjectMapper _mapper;
        
        public CreateDrugHistoryCommandHandler(IRepository<DrugHistory, long> drugHistoryRepository,
            IObjectMapper mapper)
        {
            _drugHistoryRepository = drugHistoryRepository;
            _mapper = mapper;
        }

        public async Task Handle(CreateDrugHistoryRequestDto request)
        {
            var history = _mapper.Map<DrugHistory>(request);
            await _drugHistoryRepository.InsertAsync(history).ConfigureAwait(false);
        }
    }
}
