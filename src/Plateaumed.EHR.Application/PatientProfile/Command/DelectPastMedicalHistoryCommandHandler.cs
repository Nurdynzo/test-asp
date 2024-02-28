using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Command
{
    public class DeletePastMedicalHistoryCommandHandler : IDeletePastMedicalHistoryCommandHandler
    {
        private readonly IRepository<PatientPastMedicalCondition, long> _pastMedicalConditionRepository;

        public DeletePastMedicalHistoryCommandHandler(IRepository<PatientPastMedicalCondition, long> pastMedicalConditionRepository)
        {
            _pastMedicalConditionRepository = pastMedicalConditionRepository;
        }

        public async Task Handle(long id)
        {
            var medicalHistory = await _pastMedicalConditionRepository.GetAll()
                .Include(x => x.Medications)
                .SingleOrDefaultAsync(x => x.Id == id)
                ?? throw new UserFriendlyException("Medical history does not exist");
            await _pastMedicalConditionRepository.DeleteAsync(medicalHistory).ConfigureAwait(false);
        }
    }
}
