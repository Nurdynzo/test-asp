using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Dto
{
    public class CreateReviewOfSystemsRequestDto
    {
        public string Name { get; set; }

        public long SnomedId { get; set; }

        public SymptomsCategory Category { get; set; }

        public long? Id { get; set; }
    }
}
