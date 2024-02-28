using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.VitalSigns.Dto;

namespace Plateaumed.EHR.VitalSigns;

public interface IGetApgarScoringQueryHandler : ITransientDependency
{
    Task<List<GetApgarScoringResponse>> Handle();
}