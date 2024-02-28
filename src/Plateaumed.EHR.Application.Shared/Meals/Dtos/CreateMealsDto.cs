using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Plateaumed.EHR.Authorization.Users;

namespace Plateaumed.EHR.Meals.Dtos;

public class CreateMealsDto
{
    public long PatientId { get; set; }
    public long? Stamp { get; set; }
    public List<string> MealsSnowmedIds { get; set; }
    public long EncounterId { get; set; }

    [StringLength(UserConsts.MaxDescriptionLength, MinimumLength = UserConsts.MinDescriptionLength)]
    public string Description { get; set; }
}
