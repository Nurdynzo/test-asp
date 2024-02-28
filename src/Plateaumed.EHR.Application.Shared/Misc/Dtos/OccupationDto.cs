using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.Misc.Dtos
{
    public class OccupationDto: EntityDto<long>
    {
        public string Name { get; set; }
    }
}
