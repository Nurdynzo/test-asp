using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
namespace Plateaumed.EHR.PriceSettings.Dto
{
    public class UpdatePricingCommandRequest
    {
        [Required]
        public long FacilityId { get; set; }

        public IFormFile FormFile { get; set; }
    }
}
