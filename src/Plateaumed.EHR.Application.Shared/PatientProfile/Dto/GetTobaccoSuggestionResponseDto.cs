using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Dto
{
    public class GetTobaccoSuggestionResponseDto : EntityDto<long>
    {
        public string ModeOfConsumption { get; set; }
        public long SnomedId { get; set; }
    }
}
