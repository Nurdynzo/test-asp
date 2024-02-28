using Abp.Application.Services;
using Plateaumed.EHR.ReviewAndSaves.Dtos;
using System.Threading.Tasks;

namespace Plateaumed.EHR.ReviewAndSaves
{
    public interface INurseReviewAndSaveAppService : IApplicationService
    {
        Task<NursingRecordDto> GetNursingRecord(long encounterId);
    }
}
