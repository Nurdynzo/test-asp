using System.Threading.Tasks;
using Plateaumed.EHR.ReferAndConsults.Dtos;
using Plateaumed.EHR.ReferAndConsults.Abstraction;
using Abp.ObjectMapping;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Plateaumed.EHR.ReferAndConsults.Handlers;

public class GetPatientReferralQueryHandler : IGetPatientReferralQueryHandler
{
    private readonly IRepository<AllInputs.PatientReferralOrConsultLetter, long> _referAndConsultRepository;
    private readonly IObjectMapper _objectMapper;

    public GetPatientReferralQueryHandler(
        IRepository<AllInputs.PatientReferralOrConsultLetter, long> referAndConsultRepository,
            IObjectMapper objectMapper
        )
    {
        _referAndConsultRepository = referAndConsultRepository;
        _objectMapper = objectMapper;
    }

    public async Task<ReferralOrConsultReturnDto> Handle(long encounterId)
    {
        var referralResponse = await _referAndConsultRepository.GetAll()
                        .FirstOrDefaultAsync(s => s.EncounterId == encounterId && s.Type == InputType.Referral)
                        ?? null;
        var result = new ReferralOrConsultReturnDto();
        if (referralResponse != null)
        {
            result = new ReferralOrConsultReturnDto()
            {
                Id = referralResponse.Id,
                ReceivingHospital = referralResponse.ReceivingHospital,
                ReceivingUnit = referralResponse.ReceivingUnit,
                ReceivingConsultant = referralResponse.ReceivingConsultant,
                EncounterId = referralResponse.EncounterId.Value,
                Type = referralResponse.Type,
                OtherNote = referralResponse.OtherNote
            };
        }

        return result;
    }

}
