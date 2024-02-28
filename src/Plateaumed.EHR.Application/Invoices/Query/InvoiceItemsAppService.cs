using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Plateaumed.EHR.Invoices.Dtos;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Authorization;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Plateaumed.EHR.Invoices.Query
{
    [AbpAuthorize(AppPermissions.Pages_InvoiceItems)]
    public class InvoiceItemsAppService : EHRAppServiceBase, IInvoiceItemsAppService
    {
        private readonly IRepository<InvoiceItem, long> _invoiceItemRepository;

        public InvoiceItemsAppService(IRepository<InvoiceItem, long> invoiceItemRepository)
        {
            _invoiceItemRepository = invoiceItemRepository;

        }

        public async Task<PagedResultDto<GetInvoiceItemForViewDto>> GetAll(GetAllInvoiceItemsInput input)
        {

            var filteredInvoiceItems = _invoiceItemRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter) || e.Notes.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.Contains(input.NameFilter))
                        .WhereIf(input.MinQuantityFilter != null, e => e.Quantity >= input.MinQuantityFilter)
                        .WhereIf(input.MaxQuantityFilter != null, e => e.Quantity <= input.MaxQuantityFilter)
                        .WhereIf(input.MinUnitPriceFilter != null, e => e.UnitPrice.Amount >= input.MinUnitPriceFilter)
                        .WhereIf(input.MaxUnitPriceFilter != null, e => e.UnitPrice.Amount <= input.MaxUnitPriceFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NotesFilter), e => e.Notes.Contains(input.NotesFilter));

            var pagedAndFilteredInvoiceItems = filteredInvoiceItems
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var invoiceItems = from o in pagedAndFilteredInvoiceItems
                               select new
                               {

                                   o.Name,
                                   o.Quantity,
                                   o.UnitPrice,
                                   o.DiscountAmount,
                                   o.DiscountPercentage,
                                   o.SubTotal,
                                   o.Notes,
                                   o.Id
                               };

            var totalCount = await filteredInvoiceItems.CountAsync();

            var dbList = await invoiceItems.ToListAsync();
            var results = new List<GetInvoiceItemForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetInvoiceItemForViewDto()
                {
                    InvoiceItem = new InvoiceItemDto
                    {

                        Name = o.Name,
                        Quantity = o.Quantity,
                        UnitPrice = o.UnitPrice.Amount,
                        DiscountAmount = o.DiscountAmount.Amount,
                        DiscountPercentage = o.DiscountPercentage,
                        SubTotal = o.SubTotal.Amount,
                        Notes = o.Notes,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetInvoiceItemForViewDto>(
                totalCount,
                results
            );

        }

        [AbpAuthorize(AppPermissions.Pages_InvoiceItems_Edit)]
        public async Task<GetInvoiceItemForEditOutput> GetInvoiceItemForEdit(EntityDto<long> input)
        {
            var invoiceItem = await _invoiceItemRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetInvoiceItemForEditOutput { InvoiceItem = ObjectMapper.Map<CreateOrEditInvoiceItemDto>(invoiceItem) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditInvoiceItemDto input)
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

        [AbpAuthorize(AppPermissions.Pages_InvoiceItems_Create)]
        protected virtual async Task Create(CreateOrEditInvoiceItemDto input)
        {
            var invoiceItem = ObjectMapper.Map<InvoiceItem>(input);

            if (AbpSession.TenantId != null)
            {
                invoiceItem.TenantId = AbpSession.TenantId;
            }

            await _invoiceItemRepository.InsertAsync(invoiceItem);

        }

        [AbpAuthorize(AppPermissions.Pages_InvoiceItems_Edit)]
        protected virtual async Task Update(CreateOrEditInvoiceItemDto input)
        {
            var invoiceItem = await _invoiceItemRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, invoiceItem);

        }

        [AbpAuthorize(AppPermissions.Pages_InvoiceItems_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _invoiceItemRepository.DeleteAsync(input.Id);
        }

    }
}