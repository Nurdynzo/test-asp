using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Misc.Dtos
{
    public class GetRegionsForListDto
    {
        public long Id {get; set;}
        public string Name {get; set;}
        public string ShortName {get; set;}
        public string CountryCode {get; set;}
    }
}