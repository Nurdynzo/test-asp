using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.ReviewAndSaves.Dtos;
using Plateaumed.EHR.VitalSigns.Dto;

namespace Plateaumed.EHR.ReviewAndSaves.Abstraction;

public interface IGetReviewAndSavePatientVitalSignQueryHandler : ITransientDependency
{
    Task<List<PatientVitalsSummaryResponseDto>> Handle(long patientId, long encounterId);
}
