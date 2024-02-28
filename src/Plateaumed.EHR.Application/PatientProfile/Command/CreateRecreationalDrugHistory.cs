using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Command
{
    public class CreateRecreationalDrugHistory : ICreateRecreationalDrugHistory
    {
        private readonly IRepository<RecreationalDrugHistory, long> _recreationDrugHistoryRepository;
        private readonly IObjectMapper _mapper;

        public CreateRecreationalDrugHistory(IRepository<RecreationalDrugHistory, long> recreationDrugHistoryRepository,
            IObjectMapper mapper)
        {
            _recreationDrugHistoryRepository = recreationDrugHistoryRepository;
            _mapper = mapper;
        }

        public async Task Handle(CreateRecreationalDrugsHistoryRequestDto request)
        {
            var newHistory = _mapper.Map<RecreationalDrugHistory>(request);
            await _recreationDrugHistoryRepository.InsertAsync(newHistory);
        }

       
    }
}