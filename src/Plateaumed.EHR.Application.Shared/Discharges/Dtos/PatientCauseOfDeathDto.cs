using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Discharges.Dtos
{
    public class PatientCauseOfDeathDto
    {
        public string CausesOfDeath { get; set; }
    }
}
