using Plateaumed.EHR.Patients;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Dto
{
    public class UpdateFamilyMembersDto
    {
        public long Id { get; set; }
        public Relationship Relationship { get; set; }
        public bool IsAlive { get; set; }
        public int AgeAtDeath { get; set; }
        public string CausesOfDeath { get; set; }
        public int AgeAtDiagnosis { get; set; }
    }
}
