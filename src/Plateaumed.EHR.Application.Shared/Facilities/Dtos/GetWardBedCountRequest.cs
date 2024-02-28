namespace Plateaumed.EHR.Facilities.Dtos;

public class GetWardBedCountRequest
{
    public long? FacilityId { get; set; }
    public long? WardId { get; set; }
}