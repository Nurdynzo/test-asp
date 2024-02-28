using System.Collections.Generic;
using Abp;

namespace Plateaumed.EHR.Editions.Dto;

public class GetEditionByTenantOutput
{
    public EditionListDto Edition { get; set; }
    public List<NameValue> Features { get; set; }
}