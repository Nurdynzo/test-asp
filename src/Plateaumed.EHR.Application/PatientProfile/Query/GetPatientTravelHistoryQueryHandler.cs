using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Misc.Country;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Patients.Dtos;

namespace Plateaumed.EHR.PatientProfile.Query;

public class GetPatientTravelHistoryQueryHandler : IGetPatientTravelHistoryQueryHandler
{
    private readonly IRepository<PatientTravelHistory,long> _patientTravelHistoryRepository;
    private readonly IRepository<Country> _countryRepository;
    private readonly IRepository<Region> _regionRepository;
    private readonly IRepository<User,long> _userRepository;

    public GetPatientTravelHistoryQueryHandler(IRepository<PatientTravelHistory, long> patientTravelHistoryRepository, IRepository<Country> countryRepository, IRepository<Region> regionRepository, IRepository<User, long> userRepository)
    {
        _patientTravelHistoryRepository = patientTravelHistoryRepository;
        _countryRepository = countryRepository;
        _regionRepository = regionRepository;
        _userRepository = userRepository;
    }

    public async Task<GetPatientTravelHistoryQueryResponse> Handle(long patientId)
    {
        var query = await (from h in _patientTravelHistoryRepository.GetAll()
                            join u in _userRepository.GetAll() on h.CreatorUserId equals u.Id
                            join c in _countryRepository.GetAll() on h.CountryId equals c.Id
                            //join r in _regionRepository.GetAll() on h.CityId equals r.Id
                            where h.PatientId == patientId
                            orderby h.CreationTime descending
                            select new PatientTravelHistoryResponse
                            {
                                CountryId = c.Id,
                                Country = c.Name,
                                City = h.City,
                                Duration = $"{h.Duration} {h.Interval}",
                                Date = h.Date,
                                CreatedBy = $"{u.Title}. {u.Name}",
                                DateCreated = h.CreationTime,
                                Id = h.Id,
                            }
        ).ToListAsync();

        return new GetPatientTravelHistoryQueryResponse()
        {
            PatientTravelHistory = query,
            LastCreatedBy = query.FirstOrDefault()?.CreatedBy,
            LastDateCreated = query.FirstOrDefault()?.DateCreated

        };
    }
}