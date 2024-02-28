using Abp.Application.Services;
using Plateaumed.EHR.NextAppointments.Dtos;
using Plateaumed.EHR.ReferAndConsults.Dtos;
using Plateaumed.EHR.ReviewAndSaves.Dtos;
using Plateaumed.EHR.Staff.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plateaumed.EHR.ReferAndConsults
{
    public interface IReferAndConsultAppService : IApplicationService
    {
        Task<CreateReferralOrConsultLetterDto> SaveReferralOrConsultLetter(CreateReferralOrConsultLetterDto input);
        Task<ConsultReturnDto> GetConsultationLetter(ConsultRequestDto request);

        Task<ReferralReturnDto> GetReferralLetter(ReferralRequestDto request);

    }
}