using Abp.Application.Services.Dto;
using Plateaumed.EHR.Patients;

namespace Plateaumed.EHR.Sessions.Dto
{
    public class PatientLoginInfoDto
    {
        public string PatientCode { get; set; }

        public decimal WalletBalance { get; set; }
    }
}
