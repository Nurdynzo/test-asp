using System;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Authorization.Users;

namespace Plateaumed.EHR.Staff.Dtos
{
    public class JobLevelDto : EntityDto<long>
    {
        public string Name { get; set; }

        public int? Rank { get; set; }

        public string ShortName { get; set; }

        public long? JobTitleId { get; set; }

        public bool IsActive { get; set; }

        public bool IsStatic { get; set; }

        public TitleType? TitleOfAddress { get; set; }
    }
}
