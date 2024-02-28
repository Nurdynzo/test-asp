using Abp.Application.Services.Dto;
using Abp.Dependency;
using Plateaumed.EHR.Facilities.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Facilities.Abstractions
{
    public interface IGetAllFacilitiesQueryHandler : ITransientDependency
    {
        Task<List<FacilityDto>> Handle();
    }
}