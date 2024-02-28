using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class CreateOrEditWardBedDto : EntityDto<long?>
    {
        public int Count { get; set; }

        public bool IsActive { get; set; }

        public long? BedTypeId { get; set; }
        
        public string BedTypeName { get; set; }

        public long? WardId { get; set; }
    }
}
