using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Patients.Dtos;

namespace Plateaumed.EHR.Patients
{
    public interface IPatientRelationsAppService : IApplicationService
    {
        Task<PagedResultDto<GetPatientRelationForViewDto>> GetAll(
            GetAllPatientRelationsInput input
        );

        Task<List<NextOfKinDto>> GetAllNextOfKin(long patientId);

        Task Delete(EntityDto<long> input);
    }
}
