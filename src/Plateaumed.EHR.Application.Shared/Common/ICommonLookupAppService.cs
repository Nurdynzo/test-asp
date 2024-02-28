using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Common.Dto;
using Plateaumed.EHR.Editions.Dto;

namespace Plateaumed.EHR.Common
{
    public interface ICommonLookupAppService : IApplicationService
    {
        Task<ListResultDto<SubscribableEditionComboboxItemDto>> GetEditionsForCombobox(bool onlyFreeItems = false);

        Task<PagedResultDto<NameValueDto>> FindUsers(FindUsersInput input);

        GetDefaultEditionNameOutput GetDefaultEditionName();
    }
}