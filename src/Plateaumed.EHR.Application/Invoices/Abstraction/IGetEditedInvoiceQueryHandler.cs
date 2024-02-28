using Abp.Application.Services.Dto;
using Abp.Dependency;
using Plateaumed.EHR.Invoices.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Invoices.Abstraction
{
    public interface IGetEditedInvoiceQueryHandler : ITransientDependency
    {
        Task<PagedResultDto<GetEditedInvoiceResponseDto>> Handle(GetEditedInvoiceRequestDto request, long facilityId);
    }
}
