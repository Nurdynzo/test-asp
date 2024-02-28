using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Bogus;
using Plateaumed.EHR.Patients.Abstractions;
using Plateaumed.EHR.Patients.Dtos;

namespace Plateaumed.EHR.Patients.Query;

/// <summary>
/// Get Ward Round And Clinics Notes
/// </summary>
public class GetWardRoundAndClinicsNotesHandler : IGetWardRoundAndClinicsNotesHandler
{
    
    /// <summary>
    /// Dummy implementation of GetWardRoundAndClinicsNotes this will still make use pf proper entities when ready
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<PagedResultDto<GetPatientWardRoundAndClinicNotesResponse>> Handle(GetPatientWardRoundAndClinicNotesQueryRequest request)
    {
        var result = GetWardRoundAndClinicsNotes().Take(request.MaxResultCount).Skip(request.SkipCount).ToList();
        
        return await Task.FromResult(new PagedResultDto<GetPatientWardRoundAndClinicNotesResponse>(result.Count, result));
    }

    private static IEnumerable<GetPatientWardRoundAndClinicNotesResponse> GetWardRoundAndClinicsNotes()
    {
       var result = new List<GetPatientWardRoundAndClinicNotesResponse>();
        var faker = new Faker();
        for (var i = 0; i < 30; i++)
        {
            result.Add(new GetPatientWardRoundAndClinicNotesResponse
            {
                Clinic = faker.Company.CompanyName(),
                DateTime = faker.Date.Past(),
                Notes = faker.Lorem.Paragraph(),
                Ward = faker.Company.CompanySuffix(),
                PatientId = faker.Random.Int()
            });
        }
        return result;
    }
}