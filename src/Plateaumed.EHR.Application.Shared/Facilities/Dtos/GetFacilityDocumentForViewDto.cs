namespace Plateaumed.EHR.Facilities.Dtos
{
    public class GetFacilityDocumentForViewDto
    {
        public FacilityDocumentDto FacilityDocument { get; set; }

        public string FacilityGroupName { get; set; }

        public string FacilityName { get; set; }
    }
}
