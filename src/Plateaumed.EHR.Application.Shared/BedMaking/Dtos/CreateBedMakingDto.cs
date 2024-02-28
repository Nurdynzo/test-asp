using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Plateaumed.EHR.Authorization.Users;

namespace Plateaumed.EHR.BedMaking.Dtos;

public class CreateBedMakingDto
{
    public long PatientId { get; set; }
    public long? Stamp { get; set; }
    public List<string> BedMakingSnowmedIds { get; set; }
    
    [StringLength(UserConsts.MaxDescriptionLength, MinimumLength = UserConsts.MinDescriptionLength)]
    public string Note { get; set; }
    public long EncounterId { get; set; }
}
