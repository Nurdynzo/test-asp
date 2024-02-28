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

public class CreateReferralOrConsultLetterCommandHandler : ICreateReferralOrConsultLetterCommandHandler
{
    private readonly IRepository<AllInputs.PatientReferralOrConsultLetter, long> _referAndConsultRepository;
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly IEncounterManager _encounterManager;

    public CreateReferralOrConsultLetterCommandHandler(IRepository<AllInputs.PatientReferralOrConsultLetter, long> referAndConsultRepositor, 
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

        var referralConsultationModel = new AllInputs.PatientReferralOrConsultLetter()
        {
            Type = requestDto.Type,
            EncounterId = requestDto.EncounterId,
            OtherNote = requestDto.OtherNote,
            ReceivingConsultant = requestDto.ReceivingConsultant,
            ReceivingHospital = requestDto.ReceivingHospital,
            ReceivingUnit = requestDto.ReceivingUnit,
            JsonData = requestDto.Type == InputType.Referral ? JsonConvert.SerializeObject(requestDto.ReferralLetter) :
                                          JsonConvert.SerializeObject(requestDto.ConsultLetter)
        };

        requestDto.Id = await _referAndConsultRepository.InsertAndGetIdAsync(referralConsultationModel);
        await _unitOfWorkManager.Current.SaveChangesAsync();

        return requestDto;
    }
}
