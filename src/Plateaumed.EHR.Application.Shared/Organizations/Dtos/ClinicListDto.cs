using System;
using System.Collections.Generic;
using System.Text;

namespace Plateaumed.EHR.Organizations.Dtos
{
    public class ClinicListDto
    {
        public long Id { get; set; }

        public string DisplayName { get; set; }

        public bool IsActive { get; set; }
    }
}
