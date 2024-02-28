using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using Plateaumed.EHR.Patients.Dtos;

namespace Plateaumed.EHR.Patients;

public interface IGetAccidentAndEmergencyLandingListQueryHandler : ITransientDependency
{
    Task<PagedResultDto<GetAccidentAndEmergencyLandingListResponse>> Handle(
        GetAccidentAndEmergencyLandingListRequest request);
}