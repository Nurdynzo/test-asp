using Plateaumed.EHR.Dto;

namespace Plateaumed.EHR.WebHooks.Dto
{
    public class GetAllSendAttemptsInput : PagedInputDto
    {
        public string SubscriptionId { get; set; }
    }
}
