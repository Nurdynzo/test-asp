using Plateaumed.EHR.MultiTenancy;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Plateaumed.EHR.MultiTenancy.Dtos;
using Plateaumed.EHR.Dto;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Plateaumed.EHR.Storage;

namespace Plateaumed.EHR.MultiTenancy
{
    [AbpAuthorize(AppPermissions.Pages_TenantDocuments)]
    public class TenantDocumentsAppService : EHRAppServiceBase, ITenantDocumentsAppService
    {
        private readonly IRepository<TenantDocument, long> _tenantDocumentRepository;

        private readonly ITempFileCacheManager _tempFileCacheManager;
        private readonly IBinaryObjectManager _binaryObjectManager;

        public TenantDocumentsAppService(
            IRepository<TenantDocument, long> tenantDocumentRepository,
            ITempFileCacheManager tempFileCacheManager,
            IBinaryObjectManager binaryObjectManager
        )
        {
            _tenantDocumentRepository = tenantDocumentRepository;

            _tempFileCacheManager = tempFileCacheManager;
            _binaryObjectManager = binaryObjectManager;
        }

        public async Task<PagedResultDto<GetTenantDocumentForViewDto>> GetAll(
            GetAllTenantDocumentsInput input
        )
        {
            var filteredTenantDocuments = _tenantDocumentRepository
                .GetAll()
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    e => false || e.FileName.Contains(input.Filter)
                );

            var pagedAndFilteredTenantDocuments = filteredTenantDocuments
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var tenantDocuments =
                from o in pagedAndFilteredTenantDocuments
                select new { o.Type, o.Document, o.FileName, Id = o.Id };

            var totalCount = await filteredTenantDocuments.CountAsync();

            var dbList = await tenantDocuments.ToListAsync();
            var results = new List<GetTenantDocumentForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetTenantDocumentForViewDto()
                {
                    TenantDocument = new TenantDocumentDto
                    {
                        Type = o.Type,
                        Document = o.Document,
                        FileName = o.FileName,
                        Id = o.Id,
                    }
                };
                res.TenantDocument.DocumentFileName = await GetBinaryFileName(o.Document);

                results.Add(res);
            }

            return new PagedResultDto<GetTenantDocumentForViewDto>(totalCount, results);
        }

        [AbpAuthorize(AppPermissions.Pages_TenantDocuments_Edit)]
        public async Task<GetTenantDocumentForEditOutput> GetTenantDocumentForEdit(
            EntityDto<long> input
        )
        {
            var tenantDocument = await _tenantDocumentRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetTenantDocumentForEditOutput
            {
                TenantDocument = ObjectMapper.Map<CreateOrEditTenantDocumentDto>(tenantDocument)
            };

            output.DocumentFileName = await GetBinaryFileName(tenantDocument.Document);

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditTenantDocumentDto input)
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

        [AbpAuthorize(AppPermissions.Pages_TenantDocuments_Create)]
        protected virtual async Task Create(CreateOrEditTenantDocumentDto input)
        {
            var tenantDocument = ObjectMapper.Map<TenantDocument>(input);

            if (AbpSession.TenantId != null)
            {
                tenantDocument.TenantId = (int)AbpSession.TenantId;
            }

            tenantDocument.Document = await GetBinaryObjectFromCache(input.DocumentToken);
            await _tenantDocumentRepository.InsertAsync(tenantDocument);
        }

        [AbpAuthorize(AppPermissions.Pages_TenantDocuments_Edit)]
        protected virtual async Task Update(CreateOrEditTenantDocumentDto input)
        {
            var tenantDocument = await _tenantDocumentRepository.FirstOrDefaultAsync(
                (long)input.Id
            );
            ObjectMapper.Map(input, tenantDocument);
            tenantDocument.Document = await GetBinaryObjectFromCache(input.DocumentToken);
        }

        [AbpAuthorize(AppPermissions.Pages_TenantDocuments_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _tenantDocumentRepository.DeleteAsync(input.Id);
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

        [AbpAuthorize(AppPermissions.Pages_TenantDocuments_Edit)]
        public async Task RemoveDocumentFile(EntityDto<long> input)
        {
            var tenantDocument = await _tenantDocumentRepository.FirstOrDefaultAsync(input.Id);
            if (tenantDocument == null)
            {
                throw new UserFriendlyException(L("EntityNotFound"));
            }

            if (!tenantDocument.Document.HasValue)
            {
                throw new UserFriendlyException(L("FileNotFound"));
            }

            await _binaryObjectManager.DeleteAsync(tenantDocument.Document.Value);
            tenantDocument.Document = null;
        }
    }
}
