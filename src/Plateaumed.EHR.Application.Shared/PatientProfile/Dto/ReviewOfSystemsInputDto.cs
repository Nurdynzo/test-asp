using System.Collections.Generic;

namespace Plateaumed.EHR.PatientProfile.Dto
{
    public class ReviewOfSystemsInputDto
    {
        public long PatientId { get; set; }

        public List<CreateReviewOfSystemsRequestDto> ReviewOfSystemsInputs { get; set; }

    }
}
