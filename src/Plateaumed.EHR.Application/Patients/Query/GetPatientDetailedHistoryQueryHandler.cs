using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Plateaumed.EHR.Misc.Country;
using Plateaumed.EHR.Patients.Dtos;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Patients.Abstractions;

namespace Plateaumed.EHR.Patients.Query;

/// <inheritdoc />
public class GetPatientDetailedHistoryQueryHandler : IGetPatientDetailedHistoryQueryHandler
{
    private readonly IRepository<Patient,long> _patientRepository;
    private readonly IRepository<Country, int> _countryRepository;
    private readonly IRepository<PatientCodeMapping,long> _patientCodeMappingRepository;

    /// <summary>
    /// constructor for GetPatientDetailedHistoryQueryHandler
    /// </summary>
    /// <param name="patientRepository"></param>
    /// <param name="countryRepository"></param>
    /// <param name="patientCodeMappingRepository"></param>
    public GetPatientDetailedHistoryQueryHandler(
        IRepository<Patient, long> patientRepository,
        IRepository<Country, int> countryRepository,
        IRepository<PatientCodeMapping, long> patientCodeMappingRepository)
    {
        _patientRepository = patientRepository;
        _countryRepository = countryRepository;
        _patientCodeMappingRepository = patientCodeMappingRepository;
    }

    /// <inheritdoc />
    public async Task<PatientDetailsQueryResponse> Handle(long patientId, long facilityId)
    {
        var query = from p in _patientRepository.GetAll()
            join c in _patientCodeMappingRepository.GetAll() on p.Id equals c.PatientId
            join country in _countryRepository.GetAll() on p.CountryId equals country.Id into countryGroup
            from country in countryGroup.DefaultIfEmpty()
            where p.Id == patientId && c.FacilityId == facilityId
            select new PatientDetailsQueryResponse
            {
                Address = p.Address,
                DateOfBirth = p.DateOfBirth,
                EmailAddress = p.EmailAddress,
                FullName = string.IsNullOrEmpty(p.MiddleName)
                    ? $"{p.FirstName} {p.LastName}"
                    : $"{p.FirstName} {p.MiddleName} {p.LastName}",
                Gender = p.GenderType,
                MaritalStatus = p.MaritalStatus,
                Id = p.Id,
                PhoneNumber = p.PhoneNumber,
                Nationality = country.Name,
                PatientCode = c.PatientCode,
                NoOfFemaleChildren = p.NoOfFemaleChildren,
                NoOfMaleChildren = p.NoOfMaleChildren,
                NoOfMaleSiblings = p.NoOfMaleSiblings,
                NoOfFemaleSiblings = p.NoOfFemaleSiblings,
                TotalNoOfSiblings = p.NumberOfSiblings,
                TotalNoOfChildren = p.NumberOfChildren,
                PictureUrl = p.PictureUrl
            };
        return await query.FirstOrDefaultAsync();
    }
}