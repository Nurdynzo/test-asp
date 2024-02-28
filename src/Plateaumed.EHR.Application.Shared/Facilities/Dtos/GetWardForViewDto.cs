namespace Plateaumed.EHR.Facilities.Dtos
{
    public class GetWardForViewDto
    {
        public WardDto Ward { get; set; }

        public long FacilityId { get; set; }
        
        public string FacilityName { get; set; }
    }
}
