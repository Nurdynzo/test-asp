using Plateaumed.EHR.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Plateaumed.EHR.Discharges.Dtos
{
    public class EditDischargeMedicationDto
    {
        public long DischargeId { get; set; }
        public long patientId { get; set; }
        public List<CreateDischargeMedicationDto> Medication { get; set; }
    }
    public class CreateDischargeMedicationDto
    {
        public long MedicationId { get; set; }
    }
}
