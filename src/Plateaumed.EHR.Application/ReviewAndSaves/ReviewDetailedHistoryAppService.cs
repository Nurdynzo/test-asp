using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Microsoft.AspNetCore.Mvc;
using Plateaumed.EHR.Authorization;
using Plateaumed.EHR.Patients.Dtos;
using Plateaumed.EHR.ReviewAndSaves.Abstraction;

namespace Plateaumed.EHR.ReviewAndSaves;

[AbpAuthorize(AppPermissions.Pages_ReviewDetailedHistory)]
public class ReviewDetailedHistoryAppService : EHRAppServiceBase, IReviewDetailedHistoryAppService
{
    private readonly IGetPatientReviewDetailedHistoryQueryHandler _getReviewDetailHistoryQueryHandler;
    private readonly ISaveToReviewDetailedHistoryCommandHandler _saveCommandHandler;

    public ReviewDetailedHistoryAppService(IGetPatientReviewDetailedHistoryQueryHandler getReviewDetailHistoryQueryHandler,
        ISaveToReviewDetailedHistoryCommandHandler saveCommandHandler)
    {
        _getReviewDetailHistoryQueryHandler = getReviewDetailHistoryQueryHandler;
        _saveCommandHandler = saveCommandHandler;
    }
    [AbpAuthorize(AppPermissions.Pages_ReviewDetailedHistory_Save)]
    [HttpPost]
    public async Task<ReviewDetailedHistoryReturnDto> SaveToReviewDetailedHistory(SaveToReviewDetailedHistoryRequestDto requestDto)
        => await _saveCommandHandler.Handle(requestDto);

    [HttpGet]
    public async Task<PagedResultDto<ReviewDetailedHistoryReturnDto>> GetPatientReviewDetailedHistory(long patientId)
        => await _getReviewDetailHistoryQueryHandler.Handle(patientId);

}
