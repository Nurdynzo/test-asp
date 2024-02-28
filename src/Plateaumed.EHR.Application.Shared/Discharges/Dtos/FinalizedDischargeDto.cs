using Plateaumed.EHR.DoctorDischarge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Discharges.Dtos
{
    public class FinalizeNormalDischargeDto
    {
        public long DischargeId { get; set; }
        public DischargeEntryType DischargeType { get; set; }
        public string DischargeTypeStr { get; set; }
        public string StatusStr { get; set; }
        public string Note { get; set; }
        public string PatientAssessment { get; set; }
        public string ActivitiesOfDailyLiving { get; set; }
        public string Drugs { get; set; }
    }
    public class FinalizeMarkAsDeceaseDischargeDto
    {
        public long DischargeId { get; set; }
        public bool IsBroughtInDead { get; set; }
        public string DischargeTypeStr { get; set; }
        public string StatusStr { get; set; }
        public DateTime? DateOfDeath { get; set; }
        public string TimeOfDeath { get; set; }
        public string TimeCPRCommenced { get; set; }
        public string TimeCPREnded { get; set; }
        public List<PatientCauseOfDeathDto> CausesOfDeath { get; set; }
        public string Note { get; set; }
    }
    public class FinalizeNurseDischargeDto
    {
        public long? Id { get; set; }
        public long PatientId { get; set; }
        public DischargeEntryType DischargeType { get; set; }
        public string DischargeTypeStr { get; set; }
        public DischargeStatus Status { get; set; }
        public string StatusStr { get; set; }
        public bool IsBroughtInDead { get; set; }
        public DateTime? DateOfDeath { get; set; }
        public string TimeOfDeath { get; set; }
        public string TimeCPRCommenced { get; set; }
        public string TimeCPREnded { get; set; }
        public List<PatientCauseOfDeathDto> CausesOfDeath { get; set; }
        public string Note { get; set; }
        public string PatientAssessment { get; set; }
        public string ActivitiesOfDailyLiving { get; set; }
        public string Drugs { get; set; }
        public long? AppointmentId { get; set; }
        public long EncounterId { get; set; }
    }
}
