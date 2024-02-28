using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;
using Plateaumed.EHR.Patients;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Command
{
    public class UpdateMajorInjuryCommandHandler : IUpdateMajorInjuryCommandHandler
    {
        private readonly IRepository<PatientMajorInjury, long> _majorInjuryRepository;

        public UpdateMajorInjuryCommandHandler(IRepository<PatientMajorInjury, long> majorInjuryRepository)
        {
            _majorInjuryRepository = majorInjuryRepository;
        }

        public async Task Handle(CreatePatientMajorInjuryRequest request)
        {
            if(request.Id > 0)
            {
                var majorInjuryToEdit = await _majorInjuryRepository.GetAll()
                    .SingleOrDefaultAsync(x => x.Id == request.Id)
                    ?? throw new UserFriendlyException("Major injury not found");
                majorInjuryToEdit.Diagnosis = request.Diagnosis;
                majorInjuryToEdit.PeriodOfInjury = request.PeriodOfInjury;
                majorInjuryToEdit.PeriodOfInjuryUnit = request.PeriodOfInjuryUnit;
                majorInjuryToEdit.IsOngoing = request.IsOngoing;
                majorInjuryToEdit.Notes = request.Notes;
                majorInjuryToEdit.IsComplicationPresent = request.IsComplicationPresent;

                await _majorInjuryRepository.UpdateAsync(majorInjuryToEdit).ConfigureAwait(false);
            }
        }
    }
}
