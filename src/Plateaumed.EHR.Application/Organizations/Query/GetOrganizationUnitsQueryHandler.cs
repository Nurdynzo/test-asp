using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Organizations.Dto;
using Plateaumed.EHR.Organizations.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Plateaumed.EHR.Organizations.Abstractions;

namespace Plateaumed.EHR.Organizations.Query
{
    public class GetOrganizationUnitsQueryHandler : IGetOrganizationUnitsQueryHandler
    {
        private readonly IRepository<OrganizationUnitExtended, long> _organizationUnitExtendedRepository;

        public GetOrganizationUnitsQueryHandler(
            IRepository<OrganizationUnitExtended, long> organizationUnitExtendedRepository)
        {
            _organizationUnitExtendedRepository = organizationUnitExtendedRepository;
        }

        public async Task<ListResultDto<OrganizationUnitDto>> Handle(GetOrganizationUnitsInput input, int tenantId)
        {
            var organizationUnitsExtended = await
                _organizationUnitExtendedRepository.GetAllIncluding(x => x.UserOrganizationUnits,
                        x => x.OrganizationUnitRoles,
                        x => x.OperatingTimes)
                    .IgnoreQueryFilters()
                    .WhereIf(input.UnitId.HasValue, x => x.Id == input.UnitId.Value)
                    .WhereIf(!input.IncludeClinics, x => x.Type != OrganizationUnitType.Clinic)
                    .Where(x => x.FacilityId == input.FacilityId)
                    .ToListAsync();

            return new ListResultDto<OrganizationUnitDto>(
                organizationUnitsExtended.Select(ou => new OrganizationUnitDto
                {
                    Id = ou.Id,
                    ParentId = ou.ParentId,
                    Code = ou.Code,
                    DisplayName = ou.DisplayName,
                    ShortName = ou.ShortName,
                    IsActive = ou.IsActive,
                    IsStatic = ou.IsStatic,
                    Type = ou.Type switch
                    {
                        OrganizationUnitType.Facility => "Facility",
                        OrganizationUnitType.Unit => "Unit",
                        OrganizationUnitType.Department => "Department",
                        _ => "Clinic"
                    },
                    ServiceCentre = ou.ServiceCentre,
                    FacilityId = ou.FacilityId,
                    MemberCount = ou.UserOrganizationUnits.Count,
                    RoleCount = ou.OrganizationUnitRoles.Count,
                    OperatingTimes = ou.OperatingTimes?.Select(o => new OrganizationUnitTimeDto
                    {
                        DayOfTheWeek = o.DayOfTheWeek,
                        OpeningTime = o.OpeningTime,
                        ClosingTime = o.ClosingTime,
                        IsActive = o.IsActive,
                        OrganizationUnitExtendedId = o.OrganizationUnitExtendedId
                    }).ToArray()
                }).ToList());
        }
    }
}
