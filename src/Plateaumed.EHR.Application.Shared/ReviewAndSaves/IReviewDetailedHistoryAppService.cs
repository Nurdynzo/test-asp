using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Patients.Dtos;
using Plateaumed.EHR.ReviewAndSaves.Dtos;
using System.Threading.Tasks;

namespace Plateaumed.EHR.ReviewAndSaves
{
    public interface IReviewDetailedHistoryAppService : IApplicationService
    {
        Task<PagedResultDto<ReviewDetailedHistoryReturnDto>> GetPatientReviewDetailedHistory(long patientId);
        Task<ReviewDetailedHistoryReturnDto> SaveToReviewDetailedHistory(SaveToReviewDetailedHistoryRequestDto requestDto);
    }
}
