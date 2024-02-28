using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Plateaumed.EHR.Authorization.Users;

namespace Plateaumed.EHR.WoundDressing.Dtos;

public class CreateWoundDressingDto
{
    public long PatientId { get; set; }
    public long? Stamp { get; set; }
    public List<string> WoundDressingSnowmedIds { get; set; }
    public long EncounterId { get; set; }

    [StringLength(UserConsts.MaxDescriptionLength, MinimumLength = UserConsts.MinDescriptionLength)]
    public string Description { get; set; }
}
