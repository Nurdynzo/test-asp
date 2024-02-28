using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Command
{
    public class CreateBloodTransfusionHistoryHandler : ICreateBloodTransfusionHistoryHandler
    {
        private readonly IRepository<BloodTransfusionHistory, long> _bloodTransfusionHistoryRepository;
        private readonly IObjectMapper _mapper;

        public CreateBloodTransfusionHistoryHandler(IRepository<BloodTransfusionHistory, long> bloodTransfusionHistoryRepository,
            IObjectMapper mapper)
        {
            _bloodTransfusionHistoryRepository = bloodTransfusionHistoryRepository;
            _mapper = mapper;
        }

        public async Task Handle(CreateBloodTransfusionHistoryRequestDto request)
        {
            if(request.NumberOfPints > 0)
            {
                var transfusionHistory = _mapper.Map<BloodTransfusionHistory>(request);
                await _bloodTransfusionHistoryRepository.InsertAsync(transfusionHistory);
            }
        }
    }
}
