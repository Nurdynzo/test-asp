using Plateaumed.EHR.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Plateaumed.EHR.Discharges.Dtos
{
    public class EditDischargePlanItemDto
    {
        public long DischargeId { get; set; }
        public long patientId { get; set; }
        public List<CreateDischargePlanItemDto> PlanItems { get; set; }
    }

    public class CreateDischargePlanItemDto
    {
        public long PlanItemId { get; set; }
    }
}
