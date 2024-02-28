using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.Insurance.Dtos
{
    public class GetInsuranceProviderForEditOutput
    {
        public CreateOrEditInsuranceProviderDto InsuranceProvider { get; set; }

    }
}