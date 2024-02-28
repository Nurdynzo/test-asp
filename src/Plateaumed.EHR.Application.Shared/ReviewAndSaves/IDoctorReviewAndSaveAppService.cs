using Abp.Application.Services;
using Plateaumed.EHR.NextAppointments.Dtos;
using Plateaumed.EHR.ReviewAndSaves.Dtos;
using Plateaumed.EHR.Staff.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plateaumed.EHR.ReviewAndSaves
{
    public interface IDoctorReviewAndSaveAppService : IApplicationService
    {
        Task<List<GetStaffMembersSimpleResponseDto>> GetOnBehalfOfList();
        Task<FirstVisitNoteDto> GetFirstVisitNote(long encounterId, long? doctorUserId);

    }
}