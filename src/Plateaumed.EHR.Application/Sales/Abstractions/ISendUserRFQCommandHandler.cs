﻿using Abp.Dependency;
using Plateaumed.EHR.Net.Emailing.ContactSales;
//using Plateaumed.EHR.Sales.Dto;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Sales.Abstractions
{
    public interface ISendUserRFQCommandHandler : ITransientDependency
    {
        Task Handle(QuotationRequest request);
    }
}