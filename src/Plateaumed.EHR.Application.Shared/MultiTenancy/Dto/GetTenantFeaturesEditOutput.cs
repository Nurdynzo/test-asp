using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Editions.Dto;

namespace Plateaumed.EHR.MultiTenancy.Dto
{
    public class GetTenantFeaturesEditOutput
    {
        public List<NameValueDto> FeatureValues { get; set; }

        public List<FlatFeatureDto> Features { get; set; }
    }
}