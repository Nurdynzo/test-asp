using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.VitalSigns.Dto;

namespace Plateaumed.EHR.VitalSigns;

public interface IGetGcsScoringQueryHandler : ITransientDependency
{
    Task<List<GetGCSScoringResponse>> Handle(long patientId);
}