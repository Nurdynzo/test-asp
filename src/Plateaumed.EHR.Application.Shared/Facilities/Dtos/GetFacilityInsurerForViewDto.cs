namespace Plateaumed.EHR.Facilities.Dtos
{
    public class GetFacilityInsurerForViewDto
    {
        public FacilityInsurerDto FacilityInsurer { get; set; }

        public string FacilityGroupName { get; set; }

        public string FacilityName { get; set; }

        public string InsuranceProviderName { get; set; }

    }
}