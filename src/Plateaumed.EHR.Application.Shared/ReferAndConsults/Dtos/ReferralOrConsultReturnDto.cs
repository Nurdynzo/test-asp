using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plateaumed.EHR.ReferAndConsults.Dtos
{
    public class ReferralOrConsultReturnDto
    {
        public long? Id { get; set; }
        public string ReceivingHospital { get; set; }
        public string ReceivingUnit { get; set; }
        public string ReceivingConsultant { get; set; }
        public long EncounterId { get; set; }
        public InputType Type { get; set; }
        public string OtherNote { get; set; }
        public ReferralReturnDto ReferralLetter { get; set; }
        public ConsultReturnDto ConsultLetter { get; set; }
    }
}
