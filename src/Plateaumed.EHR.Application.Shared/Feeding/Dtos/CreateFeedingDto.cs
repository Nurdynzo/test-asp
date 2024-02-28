using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Plateaumed.EHR.Authorization.Users;

namespace Plateaumed.EHR.Feeding.Dtos;

public class CreateFeedingDto
{
    public long PatientId { get; set; }
    public List<string> FeedingSnowmedIds { get; set; }
    public int Volume { get; set; }
    [StringLength(UserConsts.MaxDescriptionLength, MinimumLength = UserConsts.MinDescriptionLength)]
    public string Description { get; set; }
    public long EncounterId { get; set; }
}
