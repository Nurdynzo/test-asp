using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Plateaumed.EHR.Patients.Dtos;
using Plateaumed.EHR.Dto;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Plateaumed.EHR.Storage;

namespace Plateaumed.EHR.Patients
{
    [AbpAuthorize(AppPermissions.Pages_PatientReferralDocuments)]
    public class PatientReferralDocumentsAppService
        : EHRAppServiceBase, IPatientReferralDocumentsAppService
    {
        private readonly IRepository<PatientReferralDocument, long> _patientReferralDocumentRepository;

        private readonly ITempFileCacheManager _tempFileCacheManager;
        private readonly IBinaryObjectManager _binaryObjectManager;

        public PatientReferralDocumentsAppService(
            IRepository<PatientReferralDocument, long> patientReferralDocumentRepository,
            ITempFileCacheManager tempFileCacheManager,
            IBinaryObjectManager binaryObjectManager
        )
        {
            _patientReferralDocumentRepository = patientReferralDocumentRepository;

            _tempFileCacheManager = tempFileCacheManager;
            _binaryObjectManager = binaryObjectManager;
        }

        public async Task<PagedResultDto<GetPatientReferralDocumentForViewDto>> GetAll(
            GetAllPatientReferralDocumentsInput input
        )
        {
            var filteredPatientReferralDocuments = _patientReferralDocumentRepository
                .GetAll()
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    e =>
                        false
                        || e.ReferringHealthCareProvider.Contains(input.Filter)
                        || e.DiagnosisSummary.Contains(input.Filter)
                );

            var pagedAndFilteredPatientReferralDocuments = filteredPatientReferralDocuments
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var patientReferralDocuments =
                from o in pagedAndFilteredPatientReferralDocuments
                select new { Id = o.Id };

            var totalCount = await filteredPatientReferralDocuments.CountAsync();

            var dbList = await patientReferralDocuments.ToListAsync();
            var results = new List<GetPatientReferralDocumentForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetPatientReferralDocumentForViewDto()
                {
                    PatientReferralDocument = new PatientReferralDocumentDto { Id = o.Id, }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetPatientReferralDocumentForViewDto>(totalCount, results);
        }

        [AbpAuthorize(AppPermissions.Pages_PatientReferralDocuments_Edit)]
        public async Task<GetPatientReferralDocumentForEditOutput> GetPatientReferralDocumentForEdit(
            EntityDto<long> input
        )
        {
            var patientReferralDocument = await _patientReferralDocumentRepository.FirstOrDefaultAsync(
                input.Id
            );

            var output = new GetPatientReferralDocumentForEditOutput
            {
                PatientReferralDocument = ObjectMapper.Map<CreateOrEditPatientReferralDocumentDto>(
                    patientReferralDocument
                )
            };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditPatientReferralDocumentDto input)
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

        [AbpAuthorize(AppPermissions.Pages_PatientReferralDocuments_Create)]
        protected virtual async Task Create(CreateOrEditPatientReferralDocumentDto input)
        {
            var patientReferralDocument = ObjectMapper.Map<PatientReferralDocument>(input);

            patientReferralDocument.ReferralDocument = await GetBinaryObjectFromCache(
                input.ReferralDocumentToken
            );

            await _patientReferralDocumentRepository.InsertAsync(patientReferralDocument);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_PatientReferralDocuments_Edit)]
        protected virtual async Task Update(CreateOrEditPatientReferralDocumentDto input)
        {
            var patientReferralDocument = await _patientReferralDocumentRepository.FirstOrDefaultAsync(
                (long)input.Id
            );
            ObjectMapper.Map(input, patientReferralDocument);
            patientReferralDocument.ReferralDocument = await GetBinaryObjectFromCache(
                input.ReferralDocumentToken
            );
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_PatientReferralDocuments_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _patientReferralDocumentRepository.DeleteAsync(input.Id);
        }

        private async Task<Guid?> GetBinaryObjectFromCache(string fileToken)
        {
            if (fileToken.IsNullOrWhiteSpace())
            {
                return null;
            }

            var fileCache = _tempFileCacheManager.GetFileInfo(fileToken);

            if (fileCache == null)
            {
                throw new UserFriendlyException(
                    "There is no such file with the token: " + fileToken
                );
            }

            var storedFile = new BinaryObject(
                AbpSession.TenantId,
                fileCache.File,
                fileCache.FileName
            );
            await _binaryObjectManager.SaveAsync(storedFile);

            return storedFile.Id;
        }

        private async Task<string> GetBinaryFileName(Guid? fileId)
        {
            if (!fileId.HasValue)
            {
                return null;
            }

            var file = await _binaryObjectManager.GetOrNullAsync(fileId.Value);
            return file?.Description;
        }

        [AbpAuthorize(AppPermissions.Pages_PatientReferralDocuments_Edit)]
        public async Task RemoveReferralDocumentFile(EntityDto<long> input)
        {
            var patientReferralDocument = await _patientReferralDocumentRepository.FirstOrDefaultAsync(
                input.Id
            );
            if (patientReferralDocument == null)
            {
                throw new UserFriendlyException(L("EntityNotFound"));
            }

            if (!patientReferralDocument.ReferralDocument.HasValue)
            {
                throw new UserFriendlyException(L("FileNotFound"));
            }

            await _binaryObjectManager.DeleteAsync(patientReferralDocument.ReferralDocument.Value);
            patientReferralDocument.ReferralDocument = null;
        }

        public async Task<GetPatientReferralDocumentForEditOutput> GetPatientReferralLetter(Guid documentId)
        {
            var patientReferralDocument = await _patientReferralDocumentRepository.FirstOrDefaultAsync(
                x => x.ReferralDocument == documentId).ConfigureAwait(false);
 
            var output = new GetPatientReferralDocumentForEditOutput
            {
                PatientReferralDocument = ObjectMapper.Map<CreateOrEditPatientReferralDocumentDto>(
                    patientReferralDocument
                )
            };

            return output;
        }
    }
}
