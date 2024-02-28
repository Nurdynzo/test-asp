using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using Plateaumed.EHR.Patients.Dtos;

namespace Plateaumed.EHR.Patients;

public interface IGetInpatientLandingListQueryHandler : ITransientDependency
{
    Task<PagedResultDto<GetInpatientLandingListResponse>> Handle(GetInpatientLandingListRequest request);
}