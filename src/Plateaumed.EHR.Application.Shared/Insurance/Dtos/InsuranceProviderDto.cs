using Plateaumed.EHR.Insurance;

using System;
using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.Insurance.Dtos
{
    public class InsuranceProviderDto : EntityDto<long>
    {
        public string Name { get; set; }
        public bool isActive { get; set; }
        public InsuranceProviderType Type { get; set; }
    }
}