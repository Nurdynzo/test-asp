using System.Collections.Generic;
using Abp.Domain.Uow;
using Abp.Organizations;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Organizations.Abstractions;


namespace Plateaumed.EHR.Organizations.Handlers
{
    /// <inheritdoc />
    public class CreateDefaultOrganizationUnitsCommandHandler : ICreateDefaultOrganizationUnitsCommandHandler
    {
        private readonly IRepository<OrganizationUnitExtended, long> _organizationUnitRepository;
        private readonly OrganizationUnitManager _organizationUnitManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="organizationUnitRepository"></param>
        /// <param name="organizationUnitManager"></param>
        /// <param name="unitOfWorkManager"></param>
        public CreateDefaultOrganizationUnitsCommandHandler(
            IRepository<OrganizationUnitExtended, long> organizationUnitRepository,
            OrganizationUnitManager organizationUnitManager, IUnitOfWorkManager unitOfWorkManager)
        {
            _organizationUnitRepository = organizationUnitRepository;
            _organizationUnitManager = organizationUnitManager;
            _unitOfWorkManager = unitOfWorkManager;
        }

        /// <inheritdoc />
        public async Task Handle(int tenantId, long facilityId)
        {
            // Set name for unit as a proxy for the facility at the top of the hierarchy
            var facilityProxyName = $"Facility Unit {facilityId:x8}";
            var staticOrgUnits = StaticOrganizationalUnits.AllUnits();

            await CreateFacilityOrganizationUnitAsync(tenantId, facilityId, facilityProxyName,
                OrganizationUnitType.Facility, null, staticOrgUnits);
        }
        
        private async Task CreateFacilityOrganizationUnitAsync(
            int tenantId,
            long facilityId,
            string displayName,
            OrganizationUnitType type,
            ServiceCentreType? serviceCentre,
            IReadOnlyCollection<Unit> children = null,
            long? parentId = null,
            string shortName = null
        )
        {
            var existingUnit = _organizationUnitRepository
                .GetAll()
                .IgnoreQueryFilters()
                .Where(ou => ou.TenantId == tenantId)
                .Where(ou => ou.FacilityId == facilityId)
                .Where(ou => ou.DisplayName == displayName)
                .FirstOrDefault(ou => ou.ParentId == parentId);

            if (existingUnit == null)
            {
                var unit = new OrganizationUnitExtended
                {
                    DisplayName = displayName,
                    ShortName = shortName,
                    ParentId = parentId,
                    TenantId = tenantId,
                    FacilityId = facilityId,
                    IsActive = true,
                    IsStatic = true,
                    Type = type,
                    ServiceCentre = serviceCentre,
                    Code = await _organizationUnitManager.GetNextChildCodeAsync(parentId)
                };

                await _organizationUnitRepository.InsertAsync(unit);

                await _unitOfWorkManager.Current.SaveChangesAsync();

                if (children?.Count > 0)
                {
                    foreach (var childUnit in children)
                        await CreateFacilityOrganizationUnitAsync(
                            tenantId,
                            facilityId,
                            childUnit.DisplayName,
                            childUnit.Type,
                            childUnit.ServiceCentre,
                            childUnit.Children,
                            unit.Id,
                            childUnit.ShortName
                        );
                }
                else if (type == OrganizationUnitType.Unit)
                {
                    // Create default clinic for unit
                    await CreateFacilityOrganizationUnitAsync(
                        tenantId,
                        facilityId,
                        displayName,
                        OrganizationUnitType.Clinic,
                        serviceCentre,
                        null,
                        unit.Id,
                        null
                    );
                }
            }
        }
    }
}