using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Invoices.Dtos;
namespace Plateaumed.EHR.Invoices.Abstraction
{
    public interface IGetEditedInvoiceForDownloadQueryHandler: ITransientDependency
    {
        Task<List<GetEditedInvoiceForDownloadResponse>> Handle(GetEditedInvoiceForDownloadRequest request, long facilityId);
    }
}