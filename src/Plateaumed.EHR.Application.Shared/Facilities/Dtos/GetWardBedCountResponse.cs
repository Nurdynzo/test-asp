using System.Collections.Generic;

namespace Plateaumed.EHR.Facilities.Dtos;

public class GetWardBedCountResponse
{
    public long BedTypeId { get; set; }
    public string BedTypeName { get; set; }
    public int Count { get; set; }
    public List<WardBedDto> WardBeds { get; set; }
}