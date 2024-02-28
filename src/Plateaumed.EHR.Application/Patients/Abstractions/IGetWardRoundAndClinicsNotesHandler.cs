using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using Plateaumed.EHR.Patients.Dtos;

namespace Plateaumed.EHR.Patients.Abstractions;

/// <summary>
/// Get Ward Round And Clinics Notes
/// </summary>
public interface IGetWardRoundAndClinicsNotesHandler: ITransientDependency
{
    /// <summary>
    /// handler for GetWardRoundAndClinicsNotes
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<PagedResultDto<GetPatientWardRoundAndClinicNotesResponse>> Handle(GetPatientWardRoundAndClinicNotesQueryRequest request);
}