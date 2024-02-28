using System;
using Abp.AutoMapper;
using Plateaumed.EHR.Sessions.Dto;

namespace Plateaumed.EHR.Models.Common
{
    [AutoMapFrom(typeof(ApplicationInfoDto)),
     AutoMapTo(typeof(ApplicationInfoDto))]
    public class ApplicationInfoPersistanceModel
    {
        public string Version { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}