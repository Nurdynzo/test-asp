using Abp.Application.Services.Dto;
namespace Plateaumed.EHR.PatientProfile.Dto
{
    public class GetContraceptionSuggestionQueryResponse: EntityDto<long>
    {
        public string Name { get; set; }

        public long? SnomedId { get; set; }
    }
}