using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plateaumed.EHR.PatientProfile.Dto
{
    public class GetImplantSuggestionResponse : EntityDto<long>
    {
        public string Name { get; set; }
        public long? SnomedId { get; set; }
    }
}
