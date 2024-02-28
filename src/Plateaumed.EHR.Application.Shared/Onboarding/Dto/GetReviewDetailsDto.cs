using Abp.Application.Services.Dto;
using System.Collections.Generic;

namespace Plateaumed.EHR.Onboarding.Dto
{
    public class GetReviewDetailsDto : EntityDto<long>
    {
        public string Name { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }

        public string Website { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string PostCode { get; set; }

        public List<OnboardingReviewsDto> ChildFacilities { get; set; }
    }
}
