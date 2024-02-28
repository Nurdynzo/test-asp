using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Misc.Dtos;

namespace Plateaumed.EHR.Misc
{
    public interface ICountriesAppService : IApplicationService
    {
        Task<PagedResultDto<GetCountryForViewDto>> GetAll(GetAllCountriesInput input);

        Task<ListResultDto<GetCountriesForListDto>> GetCountries(GetCountriesInput input);

        Task<ListResultDto<GetCountryPhoneCodesForListDto>> GetCountryPhoneCodes(GetCountryPhoneCodesInput input);

        Task<List<GetNationalitiesOutput>> GetNationalities(GetNationalitiesInput input);

        Task<GetCountryForEditOutput> GetCountryForEdit(EntityDto<int> input);

        Task CreateOrEdit(CreateOrEditCountryDto input);

        Task Delete(EntityDto<int> input);
    }
}
