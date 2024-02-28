using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Patients.Dtos;
using Plateaumed.EHR.Dto;

namespace Plateaumed.EHR.Patients
{
    public interface IPatientReferralDocumentsAppService : IApplicationService
    {
        Task<PagedResultDto<GetPatientReferralDocumentForViewDto>> GetAll(
            GetAllPatientReferralDocumentsInput input
        );

        Task<GetPatientReferralDocumentForEditOutput> GetPatientReferralDocumentForEdit(
            EntityDto<long> input
        );

        Task CreateOrEdit(CreateOrEditPatientReferralDocumentDto input);

        Task Delete(EntityDto<long> input);

        Task RemoveReferralDocumentFile(EntityDto<long> input);
    }
}
