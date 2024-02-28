using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using Abp.Runtime.Session;
using Plateaumed.EHR.Procedures.Dtos;

namespace Plateaumed.EHR.Procedures.Abstractions;

public interface IGetStatementOfPatientOrNextOfKinOrGuardianQueryHandler : ITransientDependency
{
    Task<StatementOfPatientOrNextOfKinOrGuardianResponseDto> Handle(long procedureId, IAbpSession abpSession);
}