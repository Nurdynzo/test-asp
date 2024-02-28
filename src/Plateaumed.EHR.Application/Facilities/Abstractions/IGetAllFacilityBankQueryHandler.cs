using Abp.Application.Services.Dto;
using Abp.Dependency;
using Plateaumed.EHR.Facilities.Dtos;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Facilities.Abstractions
{
    public interface IGetAllFacilityBankQueryHandler : ITransientDependency
    {
        Task<PagedResultDto<GetFacilityBankForViewDto>> Handle(GetAllFacilityBanksInput input);
    }
}
