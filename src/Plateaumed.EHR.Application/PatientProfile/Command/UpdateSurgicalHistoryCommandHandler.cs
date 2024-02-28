using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Command
{
    public class UpdateSurgicalHistoryCommandHandler : IUpdateSurgicalHistoryCommandHandler
    {
        private readonly IRepository<SurgicalHistory, long> _surgicalHistoryRepository;

        public UpdateSurgicalHistoryCommandHandler(IRepository<SurgicalHistory, long> surgicalHistoryRepository)
        {
            _surgicalHistoryRepository = surgicalHistoryRepository;
        }

        public async Task Handle(long  surgicalHistoryId, CreateSurgicalHistoryRequestDto request)
        {
            var historyToEdit = await _surgicalHistoryRepository.GetAll().SingleOrDefaultAsync(x => x.Id == surgicalHistoryId)
            ?? throw new UserFriendlyException("History not found");

            historyToEdit.Diagnosis = request.Diagnosis ?? historyToEdit.Diagnosis;
            historyToEdit.DiagnosisSnomedId = request.DiagnosisSnomedId ?? historyToEdit.DiagnosisSnomedId;
            historyToEdit.Procedure = request.Procedure ?? historyToEdit.Procedure;
            historyToEdit.ProcedureSnomedId = request.ProcedureSnomedId ?? historyToEdit.ProcedureSnomedId;
            historyToEdit.PeriodSinceSurgery = request.PeriodSinceSurgery;
            historyToEdit.Interval = request.Interval;
            historyToEdit.NoComplicationsPresent = request.NoComplicationsPresent;
            historyToEdit.Note = request.Note ?? historyToEdit.Note;
            await _surgicalHistoryRepository.UpdateAsync(historyToEdit);
        }
    }
}
