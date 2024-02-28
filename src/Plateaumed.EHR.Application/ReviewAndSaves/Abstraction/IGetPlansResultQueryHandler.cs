using Abp.Dependency;
using Plateaumed.EHR.Admissions.Dto;
using Plateaumed.EHR.ReviewAndSaves.Dtos;
using System.Threading.Tasks;

namespace Plateaumed.EHR.ReviewAndSaves.Abstraction;

public interface IGetPlansResultQueryHandler : ITransientDependency
{
    Task<Plans> Handle(long patientId, long encounterId, AdmitPatientRequest admission);
}