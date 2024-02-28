using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Patients.Dtos;
using Plateaumed.EHR.ReviewAndSaves.Dtos;

namespace Plateaumed.EHR.ReviewAndSaves.Abstraction;

public interface ISaveToReviewDetailedHistoryCommandHandler : ITransientDependency
{
    Task<ReviewDetailedHistoryReturnDto> Handle(SaveToReviewDetailedHistoryRequestDto requestDto);
}