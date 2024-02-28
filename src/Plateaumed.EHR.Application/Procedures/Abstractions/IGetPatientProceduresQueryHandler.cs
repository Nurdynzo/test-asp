using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using Plateaumed.EHR.Procedures.Dtos;

namespace Plateaumed.EHR.Procedures.Abstractions;

public interface IGetPatientProceduresQueryHandler : ITransientDependency
{
    Task<List<PatientProcedureResponseDto>> Handle(long patientId, string procedureType, long? parentProcedureId);
}