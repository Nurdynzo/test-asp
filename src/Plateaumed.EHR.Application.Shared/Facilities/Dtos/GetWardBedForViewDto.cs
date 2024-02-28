namespace Plateaumed.EHR.Facilities.Dtos
{
    public class GetWardBedForViewDto
    {
        public WardBedDto WardBed { get; set; }

        public string BedTypeName { get; set; }

        public string WardName { get; set; }

        public string Status { get; set; }
    }
}
