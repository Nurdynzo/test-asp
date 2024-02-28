using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Command
{
    public class UpdateBloodTransfusionHistory : IUpdateBloodTransfusionHistory
    {
        private readonly IRepository<BloodTransfusionHistory, long> _bloodTranfusionHistoryRepository;

        public UpdateBloodTransfusionHistory(IRepository<BloodTransfusionHistory, long> bloodTranfusionHistoryRepository)
        {
            _bloodTranfusionHistoryRepository = bloodTranfusionHistoryRepository;
        }

        public async Task Handle(long id, CreateBloodTransfusionHistoryRequestDto dto)
        {
            var historyToEdit = await _bloodTranfusionHistoryRepository.GetAll().SingleOrDefaultAsync(x => x.Id == id)
                ?? throw new UserFriendlyException("History not found");
            
            historyToEdit.PeriodSinceLastTransfusion = dto.PeriodSinceLastTransfusion;
            historyToEdit.Interval = dto.Interval;
            historyToEdit.NumberOfPints = dto.NumberOfPints;
            historyToEdit.NoComplications = dto.NoComplications;
            historyToEdit.Note = dto.Note ?? historyToEdit.Note;
            await _bloodTranfusionHistoryRepository.UpdateAsync(historyToEdit);
        }
    }
}
