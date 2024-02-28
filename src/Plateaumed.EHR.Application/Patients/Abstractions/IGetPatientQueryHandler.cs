using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using Plateaumed.EHR.Patients.Dtos;

namespace Plateaumed.EHR.Patients.Abstractions;

/// <summary>
/// Get Ward Round And Clinics Notes
/// </summary>
public interface IGetPatientQueryHandler : ITransientDependency
{
    /// <summary>
    /// handler for GetPatientQueryHandler
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<CreateOrEditPatientDto> Handle(EntityDto<long> input);
}
