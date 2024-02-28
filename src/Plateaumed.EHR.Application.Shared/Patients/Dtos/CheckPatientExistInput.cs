
using System.ComponentModel.DataAnnotations;
using Plateaumed.EHR.Authorization.Users;

namespace Plateaumed.EHR.Patients.Dtos
{
    public class CheckPatientExistInput
    {
        [Required(ErrorMessage = "Gender is required")]
        public GenderType GenderType { get; set; }

        [Required(ErrorMessage = "PhoneNumber is required")]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        public string PhoneNumber { get; set; }

    }
}
