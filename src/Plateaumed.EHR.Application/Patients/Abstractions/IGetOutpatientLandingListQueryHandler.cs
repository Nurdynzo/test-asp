using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using Plateaumed.EHR.Patients.Dtos;

namespace Plateaumed.EHR.Patients.Abstractions;

public interface IGetOutpatientLandingListQueryHandler : ITransientDependency
{
    Task<PagedResultDto<GetPatientLandingListOuptDto>> Handle(GetAllForLookupTableInput input);
}