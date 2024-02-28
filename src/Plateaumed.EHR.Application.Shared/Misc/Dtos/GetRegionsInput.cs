using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Misc.Dtos
{
    public class GetRegionsInput
    {
        public string Filter {get; set;}
        public string CountryCodeFilter {get; set;}
    }
}