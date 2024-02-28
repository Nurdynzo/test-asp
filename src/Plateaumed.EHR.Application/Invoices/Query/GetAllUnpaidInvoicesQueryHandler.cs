using Abp.Collections.Extensions;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Invoices.Abstraction;
using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.Invoices.Dtos.UnpaidInvoicesDtos;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Plateaumed.EHR.Utility;

namespace Plateaumed.EHR.Invoices.Query
{
    /// <inheritdoc/>
    public class GetAllUnpaidInvoicesQueryHandler : IGetAllUnpaidInvoicesQueryHandler
    {

        private readonly IInvoicesBaseQuery _baseQuery;

        public GetAllUnpaidInvoicesQueryHandler(IInvoicesBaseQuery baseQuery)
        {
            _baseQuery = baseQuery;
        }

        /// <inheritdoc/>
        public async Task<UnpaidInvoicesResponse> Handle(UnpaidInvoicesRequest request)
        {
            var query = _baseQuery.GetPatientUnpaidInvoicesBaseQuery(request.PatientId);

            var resultQuery = await query.ToListAsync();

            if (resultQuery.Count==0)
            {
                return new UnpaidInvoicesResponse { Invoices = resultQuery };
            }

            var totalUnpaidAmount = resultQuery.Select(x => x.TotalAmount.ToMoney()).Sum();

            var result = new UnpaidInvoicesResponse
            {
                Invoices = resultQuery,
                TotalAmount = totalUnpaidAmount.ToMoneyDto()
            };

            return result;
        }
    }
}
