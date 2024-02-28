using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Plateaumed.EHR.Patients.Dtos;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Authorization;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Plateaumed.EHR.Patients
{
    [AbpAuthorize(AppPermissions.Pages_PatientRelations)]
    public class PatientRelationsAppService : EHRAppServiceBase, IPatientRelationsAppService
    {
        private readonly IRepository<PatientRelation, long> _patientRelationRepository;
        private readonly IRepository<Patient, long> _patientRepository;

        public PatientRelationsAppService(
            IRepository<PatientRelation, long> patientRelationRepository,
            IRepository<Patient, long> patientRepository
        )
        {
            _patientRelationRepository = patientRelationRepository;
            _patientRepository = patientRepository;
        }

        public async Task<PagedResultDto<GetPatientRelationForViewDto>> GetAll(
            GetAllPatientRelationsInput input
        )
        {
            var relationshipFilter = input.RelationshipFilter.HasValue
                ? (Relationship)input.RelationshipFilter
                : default;

            var filteredPatientRelations = _patientRelationRepository
                .GetAll()
                .Include(e => e.PatientFk)
                .ThenInclude(x=>x.PatientCodeMappings)
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    e =>
                        false
                        || e.FirstName.Contains(input.Filter)
                        || e.MiddleName.Contains(input.Filter)
                        || e.LastName.Contains(input.Filter)
                        || e.Address.Contains(input.Filter)
                        || e.PhoneNumber.Contains(input.Filter)
                        || e.Email.Contains(input.Filter)
                )
                .WhereIf(
                    input.RelationshipFilter.HasValue && input.RelationshipFilter > -1,
                    e => e.Relationship == relationshipFilter
                )
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.FirstNameFilter),
                    e => e.FirstName.Contains(input.FirstNameFilter)
                )
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.MiddleNameFilter),
                    e => e.MiddleName.Contains(input.MiddleNameFilter)
                )
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.LastNameFilter),
                    e => e.LastName.Contains(input.LastNameFilter)
                )
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.PatientPatientCodeFilter),
                    e =>
                        e.PatientFk != null
                        && e.PatientFk.PatientCodeMappings.Any(x=>x.PatientCode.Equals(input.PatientPatientCodeFilter)
                        && x.PatientId == e.PatientId)
                );

            var pagedAndFilteredPatientRelations = filteredPatientRelations
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var patientRelations =
                from o in pagedAndFilteredPatientRelations
                join o1 in _patientRepository.GetAll() on o.PatientId equals o1.Id into j1
                from s1 in j1.DefaultIfEmpty()
                select new
                {
                    o.Relationship,
                    o.FirstName,
                    o.MiddleName,
                    o.LastName,
                    o.Title,
                    o.Address,
                    o.PhoneNumber,
                    o.Email,
                    o.IsGuardian,
                    Id = o.Id,
                    PatientPatientCode = s1 == null || s1.PatientCodeMappings == null || s1.PatientCodeMappings.Count == 0
                        ? ""
                        : s1.PatientCodeMappings.FirstOrDefault().PatientCode
                };

            var totalCount = await filteredPatientRelations.CountAsync();

            var dbList = await patientRelations.ToListAsync();
            var results = new List<GetPatientRelationForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetPatientRelationForViewDto()
                {
                    PatientRelation = new PatientRelationDto
                    {
                        Relationship = o.Relationship,
                        FirstName = o.FirstName,
                        MiddleName = o.MiddleName,
                        LastName = o.LastName,
                        Title = o.Title,
                        Address = o.Address,
                        PhoneNumber = o.PhoneNumber,
                        Email = o.Email,
                        IsGuardian = o.IsGuardian,
                        Id = o.Id,
                    },
                    PatientPatientCode = o.PatientPatientCode
                };

                results.Add(res);
            }

            return new PagedResultDto<GetPatientRelationForViewDto>(totalCount, results);
        }

        /// <summary>
        /// An endpoint to get a patient's list of next-of-kin by patient Id
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        async public Task<List<NextOfKinDto>> GetAllNextOfKin(long patientId)
        {

            var result = await _patientRelationRepository.GetAll()
                .Where(p => !p.IsGuardian && p.PatientId == patientId)
                .Select(p => new NextOfKinDto
                {

                    Id = p.Id,
                    FullName = p.FullName,
                    Relationship = p.Relationship,
                    PhoneNumber = p.PhoneNumber,

                }).ToListAsync();

            return result;
        }

        [AbpAuthorize(AppPermissions.Pages_PatientRelations_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _patientRelationRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_PatientRelations)]
        public async Task<
            PagedResultDto<PatientRelationPatientLookupTableDto>
        > GetAllPatientForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _patientRepository
                .GetAll()
                .Include(x=>x.PatientCodeMappings)
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    e => e.PatientCodeMappings != null && e.PatientCodeMappings.Any(x=>x.PatientCode.Contains(input.Filter))
                );

            var totalCount = await query.CountAsync();

            var patientList = await query.PageBy(input).ToListAsync();

            var lookupTableDtoList = new List<PatientRelationPatientLookupTableDto>();
            foreach (var patient in patientList)
            {
                lookupTableDtoList.Add(
                    new PatientRelationPatientLookupTableDto
                    {
                        Id = patient.Id,
                        DisplayName = patient.PatientCodeMappings.FirstOrDefault()?.PatientCode
                    }
                );
            }

            return new PagedResultDto<PatientRelationPatientLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }
    }
}
