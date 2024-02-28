using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Abp.UI;
using Newtonsoft.Json;
using Plateaumed.EHR.Encounters;
using Plateaumed.EHR.ReferAndConsults.Abstraction;
using Plateaumed.EHR.ReferAndConsults.Dtos;

namespace Plateaumed.EHR.ReferAndConsults.Handlers;

public class EditReferralOrConsultLetterCommandHandler : IEditReferralOrConsultLetterCommandHandler
{
    private readonly IRepository<AllInputs.PatientReferralOrConsultLetter, long> _referAndConsultRepository;
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly IEncounterManager _encounterManager;

    public EditReferralOrConsultLetterCommandHandler(IRepository<AllInputs.PatientReferralOrConsultLetter, long> referAndConsultRepositor,
        IUnitOfWorkManager unitOfWorkManager,
        IEncounterManager encounterManager)
    {
        _referAndConsultRepository = referAndConsultRepositor;
        _unitOfWorkManager = unitOfWorkManager;
        _encounterManager = encounterManager;
    }

    public async Task<CreateReferralOrConsultLetterDto> Handle(CreateReferralOrConsultLetterDto requestDto)
    {
        if (requestDto == null)
            throw new UserFriendlyException("Request cannot be empty.");

        if (requestDto.EncounterId <= 0)
            throw new UserFriendlyException("EncounterId is required.");

        await _encounterManager.CheckEncounterExists(requestDto.EncounterId);

        var model = _referAndConsultRepository.GetAllList().FirstOrDefault(s => s.Id == requestDto.Id) ??
            throw new UserFriendlyException("No existing record found to edit.");
        if (model != null)
        {
            model.Type = requestDto.Type;
            model.EncounterId = requestDto.EncounterId;
            model.OtherNote = requestDto.OtherNote;
            model.ReceivingConsultant = requestDto.ReceivingConsultant;
            model.ReceivingHospital = requestDto.ReceivingHospital;
            model.ReceivingUnit = requestDto.ReceivingUnit;
            model.JsonData = requestDto.Type == InputType.Referral ? JsonConvert.SerializeObject(requestDto.ReferralLetter) :
                                          JsonConvert.SerializeObject(requestDto.ConsultLetter);

            await _referAndConsultRepository.UpdateAsync(model);
            await _unitOfWorkManager.Current.SaveChangesAsync();
        }


        return requestDto;
    }
}