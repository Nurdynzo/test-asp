using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using Plateaumed.EHR.Patients.Dtos;

namespace Plateaumed.EHR.ReviewAndSaves.Abstraction
{
    public interface IGetPatientReviewDetailedHistoryQueryHandler : ITransientDependency
    {
        Task<PagedResultDto<ReviewDetailedHistoryReturnDto>> Handle(long patientId);
    }
}
