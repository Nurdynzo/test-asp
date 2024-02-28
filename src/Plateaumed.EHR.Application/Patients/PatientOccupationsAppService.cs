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
    [AbpAuthorize(AppPermissions.Pages_PatientOccupations)]
    public class PatientOccupationsAppService : EHRAppServiceBase, IPatientOccupationsAppService
    {
        private readonly IRepository<PatientOccupation, long> _patientOccupationRepository;

        public PatientOccupationsAppService(
            IRepository<PatientOccupation, long> patientOccupationRepository
        )
        {
            _patientOccupationRepository = patientOccupationRepository;
        }

        public async Task<PagedResultDto<GetPatientOccupationForViewDto>> GetAll(GetAllPatientOccupationsInput input)
        {
            var filteredPatientOccupations = _patientOccupationRepository
                .GetAll()
                .Include(e => e.Occupation)
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    e => false || e.Occupation.Name.Contains(input.Filter)
                )
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.NameFilter),
                    e => e.Occupation.Name.Contains(input.NameFilter)
                );

            var pagedAndFilteredPatientOccupations = filteredPatientOccupations
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var patientOccupations =
                from o in pagedAndFilteredPatientOccupations
                select new
                {
                    o.Occupation.Name,
                    Id = o.Id,
                };

            var totalCount = await filteredPatientOccupations.CountAsync();

            var dbList = await patientOccupations.ToListAsync();
            var results = new List<GetPatientOccupationForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetPatientOccupationForViewDto()
                {
                    PatientOccupation = new PatientOccupationDto {  Id = o.Id, },
                };

                results.Add(res);
            }

            return new PagedResultDto<GetPatientOccupationForViewDto>(totalCount, results);
        }

        [AbpAuthorize(AppPermissions.Pages_PatientOccupations_Edit)]
        public async Task<GetPatientOccupationForEditOutput> GetPatientOccupationForEdit(
            EntityDto<long> input
        )
        {
            var patientOccupation = await _patientOccupationRepository.FirstOrDefaultAsync(
                input.Id
            );

            var output = new GetPatientOccupationForEditOutput
            {
                PatientOccupation = ObjectMapper.Map<CreateOrEditPatientOccupationDto>(
                    patientOccupation
                )
            };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditPatientOccupationDto input)
        {
            if (input.Id == null)
            {
                await Create(input);
            }
            else
            {
                await Update(input);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_PatientOccupations_Create)]
        protected virtual async Task Create(CreateOrEditPatientOccupationDto input)
        {
            var patientOccupation = ObjectMapper.Map<PatientOccupation>(input);

            await _patientOccupationRepository.InsertAsync(patientOccupation);
        }

        [AbpAuthorize(AppPermissions.Pages_PatientOccupations_Edit)]
        protected virtual async Task Update(CreateOrEditPatientOccupationDto input)
        {
            var patientOccupation = await _patientOccupationRepository.FirstOrDefaultAsync(
                (long)input.Id
            );
            ObjectMapper.Map(input, patientOccupation);
        }

        [AbpAuthorize(AppPermissions.Pages_PatientOccupations_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _patientOccupationRepository.DeleteAsync(input.Id);
        }
    }
}
