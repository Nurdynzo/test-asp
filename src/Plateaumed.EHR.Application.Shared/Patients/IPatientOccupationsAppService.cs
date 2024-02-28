using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Patients.Dtos;
using Plateaumed.EHR.Dto;

namespace Plateaumed.EHR.Patients
{
    public interface IPatientOccupationsAppService : IApplicationService
    {
        Task<PagedResultDto<GetPatientOccupationForViewDto>> GetAll(
            GetAllPatientOccupationsInput input
        );

        Task<GetPatientOccupationForEditOutput> GetPatientOccupationForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditPatientOccupationDto input);

        Task Delete(EntityDto<long> input);
    }
}
