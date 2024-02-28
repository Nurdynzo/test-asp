using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Plateaumed.EHR.Facilities.Dtos;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Abp.IO.Extensions;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Plateaumed.EHR.Storage;
using Microsoft.AspNetCore.Mvc;
using Plateaumed.EHR.Facilities.Abstractions;
using Plateaumed.EHR.Facilities.Dto;
using Plateaumed.EHR.PatientDocument.Abstraction;
using System.IO;

namespace Plateaumed.EHR.Facilities
{
    [AbpAuthorize(AppPermissions.Pages_FacilityDocuments)]
    public class FacilityDocumentsAppService : EHRAppServiceBase, IFacilityDocumentsAppService
    {
        private readonly IRepository<FacilityDocument, long> _facilityDocumentRepository;
        private readonly IRepository<Facility, long> _lookup_facilityRepository;

        private readonly ITempFileCacheManager _tempFileCacheManager;
        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly IUploadFacilityLogoCommandHandler _uploadFacilityLogo;
        private readonly IGetFileQueryHandler _getFile;

        public FacilityDocumentsAppService(
            IRepository<FacilityDocument, long> facilityDocumentRepository,
            IRepository<Facility, long> lookup_facilityRepository,
            ITempFileCacheManager tempFileCacheManager,
            IBinaryObjectManager binaryObjectManager,
            IUploadFacilityLogoCommandHandler uploadFacilityLogo,
            IGetFileQueryHandler getFile
        )
        {
            _getFile = getFile;
            _facilityDocumentRepository = facilityDocumentRepository;
            _lookup_facilityRepository = lookup_facilityRepository;

            _tempFileCacheManager = tempFileCacheManager;
            _binaryObjectManager = binaryObjectManager;
            _uploadFacilityLogo = uploadFacilityLogo;
        }

        public async Task<PagedResultDto<GetFacilityDocumentForViewDto>> GetAll(
            GetAllFacilityDocumentsInput input
        )
        {
            var filterTerms = !string.IsNullOrWhiteSpace(input.Filter) ? input.Filter.ToLower().Split(" ") : null;

            var filteredFacilityDocuments = _facilityDocumentRepository
                .GetAll()
                .Include(e => e.FacilityGroupFk)
                .Include(e => e.FacilityFk)
                .WhereIf(
                    filterTerms != null,
                    f => filterTerms.All(term => f.FileName.ToLower().Contains(term)
                    || f.DocumentType.ToString().ToLower().Contains(term)
                    || (f.FacilityFk != null && f.FacilityFk.Name.ToLower().Contains(term))
                    || (f.FacilityGroupFk != null && f.FacilityGroupFk.Name.ToLower().Contains(term))
                ))
                .WhereIf(
                    input.FacilityIdFilter.HasValue,
                    f => f.FacilityId == input.FacilityIdFilter
                )
                .WhereIf(
                    input.FacilityGroupIdFilter.HasValue,
                    f => f.FacilityGroupId == input.FacilityGroupIdFilter
                );

            var pagedAndFilteredFacilityDocuments = filteredFacilityDocuments
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var totalCount = await filteredFacilityDocuments.CountAsync();

            var facilityDocuments = await pagedAndFilteredFacilityDocuments.ToListAsync();

            var results = new List<GetFacilityDocumentForViewDto>();

            foreach (var facilityDocument in facilityDocuments)
            {
                var documentOutput = new GetFacilityDocumentForViewDto
                {
                    FacilityDocument = ObjectMapper.Map<FacilityDocumentDto>(facilityDocument),
                    FacilityGroupName = facilityDocument.FacilityGroupFk?.Name,
                    FacilityName = facilityDocument.FacilityFk?.Name
                };

                documentOutput.FacilityDocument.DocumentFileName = await GetBinaryFileName(facilityDocument.Document);

                results.Add(documentOutput);
            };

            return new PagedResultDto<GetFacilityDocumentForViewDto>(totalCount, results);
        }

        public async Task<List<GetFacilityDocumentForViewDto>> GetAllFacilityDocumentForView(EntityDto<long> input)
        {
            var filteredFacilityDocuments = _facilityDocumentRepository
                .GetAll()
                .Include(e => e.FacilityGroupFk)
                .Include(e => e.FacilityFk)
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.Id.ToString()),
                    f => f.FacilityId == input.Id
                );
            var facilityDocuments = await filteredFacilityDocuments.ToListAsync();
            var results = new List<GetFacilityDocumentForViewDto>();

            foreach (var facilityDocument in facilityDocuments)
            {
                var documentOutput = new GetFacilityDocumentForViewDto
                {
                    FacilityDocument = ObjectMapper.Map<FacilityDocumentDto>(facilityDocument),
                    FacilityGroupName = facilityDocument.FacilityGroupFk?.Name,
                    FacilityName = facilityDocument.FacilityFk?.Name
                };
                documentOutput.FacilityDocument.DocumentFileName = await GetBinaryFileName(facilityDocument.Document);
                results.Add(documentOutput);
            };

            return results;
        }

        [AbpAuthorize(AppPermissions.Pages_FacilityDocuments_Edit)]
        public async Task<GetFacilityDocumentForEditOutput> GetFacilityDocumentForEdit(
            EntityDto<long> input
        )
        {
            var facilityDocument = await _facilityDocumentRepository.GetAll()
                .Include(f => f.FacilityGroupFk)
                .Include(f => f.FacilityFk)
                .Where(f => f.Id == input.Id)
                .FirstOrDefaultAsync();

            var output = new GetFacilityDocumentForEditOutput
            {
                FacilityDocument = ObjectMapper.Map<CreateOrEditFacilityDocumentDto>(facilityDocument),
                Facility = facilityDocument.FacilityFk?.Name,
                FacilityGroup = facilityDocument.FacilityGroupFk?.Name,
            };

            output.DocumentFileName = await GetBinaryFileName(facilityDocument.Document);

            return output;
        }

        /// <summary>
        /// Uploads a logo for a facility
        /// </summary>
        /// <param name="request"></param>
        public async Task<Guid> UploadLogo([FromForm] UploadFacilityLogoRequest request)
        {
            return await _uploadFacilityLogo.Handle(request);
        }

        /// <summary>
        /// Get file by Id
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        public async Task<string> GetFacilityLogo(Guid fileId)
        {
            var (stream, _) = await _getFile.Handle(fileId);

            using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            var bytes = memoryStream.ToArray();
            var base64String = Convert.ToBase64String(bytes);

            return base64String;
        }

        public async Task CreateOrEdit(CreateOrEditFacilityDocumentDto input)
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

        [AbpAuthorize(AppPermissions.Pages_FacilityDocuments_Create)]
        protected virtual async Task Create(CreateOrEditFacilityDocumentDto input)
        {
            var facilityDocument = ObjectMapper.Map<FacilityDocument>(input);

            if (AbpSession.TenantId != null)
            {
                facilityDocument.TenantId = (int)AbpSession.TenantId;
            }

            facilityDocument.Document = await GetBinaryObjectFromCache(input.DocumentToken);

            await _facilityDocumentRepository.InsertAsync(facilityDocument);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_FacilityDocuments_Edit)]
        protected virtual async Task Update(CreateOrEditFacilityDocumentDto input)
        {
            var facilityDocument = await _facilityDocumentRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, facilityDocument);
            facilityDocument.Document = await GetBinaryObjectFromCache(input.DocumentToken);

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_FacilityDocuments_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _facilityDocumentRepository.DeleteAsync(input.Id);
            await CurrentUnitOfWork.SaveChangesAsync();
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

        [AbpAuthorize(AppPermissions.Pages_FacilityDocuments_Edit)]
        public async Task RemoveDocumentFile(EntityDto<long> input)
        {
            var facilityDocument = await _facilityDocumentRepository.FirstOrDefaultAsync(input.Id);
            if (facilityDocument == null)
            {
                throw new UserFriendlyException(L("EntityNotFound"));
            }

            if (!facilityDocument.Document.HasValue)
            {
                throw new UserFriendlyException(L("FileNotFound"));
            }

            await _binaryObjectManager.DeleteAsync(facilityDocument.Document.Value);
            facilityDocument.Document = null;

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_FacilityDocuments_Edit)]
        public async Task<
            PagedResultDto<FacilityDocumentFacilityLookupTableDto>
        > GetAllFacilitiesForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_facilityRepository
                .GetAll()
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    e => e.Name != null && e.Name.Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var facilityList = await query.PageBy(input).ToListAsync();

            var lookupTableDtoList = new List<FacilityDocumentFacilityLookupTableDto>();
            foreach (var facility in facilityList)
            {
                lookupTableDtoList.Add(
                    new FacilityDocumentFacilityLookupTableDto
                    {
                        Id = facility.Id,
                        DisplayName = facility.Name?.ToString()
                    }
                );
            }

            return new PagedResultDto<FacilityDocumentFacilityLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }
    }
}
