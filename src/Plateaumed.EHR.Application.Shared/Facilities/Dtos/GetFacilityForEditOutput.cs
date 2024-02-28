namespace Plateaumed.EHR.Facilities.Dtos
{
    public class GetFacilityForEditOutput
    {
        public CreateOrEditFacilityDto Facility { get; set; }

        public string FacilityGroup { get; set; }

        public string FacilityType { get; set; }
    }
}
