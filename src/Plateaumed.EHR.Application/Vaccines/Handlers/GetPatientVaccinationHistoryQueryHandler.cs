using System.Collections.Generic;
using System.Linq; 
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Vaccines.Abstractions;
using Plateaumed.EHR.Vaccines.Dto;

namespace Plateaumed.EHR.Vaccines.Handlers;

public class GetPatientVaccinationHistoryQueryHandler : IGetPatientVaccinationHistoryQueryHandler
{
    private readonly IRepository<VaccinationHistory, long> _vaccinationHistoryRepository;  
    private readonly IObjectMapper _objectMapper;
    
    public GetPatientVaccinationHistoryQueryHandler(IRepository<VaccinationHistory, long> vaccinationHistoryRepository, IObjectMapper objectMapper)
    {
        _vaccinationHistoryRepository = vaccinationHistoryRepository;
        _objectMapper = objectMapper;
    }
    
    public async Task<List<VaccinationHistoryResponseDto>> Handle(EntityDto<long> patientId)
    { 
        var vaccinationHistories = await _vaccinationHistoryRepository.GetAll().Include(v => v.Vaccine)
            .ThenInclude(v => v.Schedules)
            .Where(v => v.PatientId == patientId.Id)
            .OrderByDescending(o => o.Id)
            .Select(h => new VaccinationHistoryResponseDto
            {
                Id = h.Id,
                PatientId = h.PatientId,
                VaccineId = h.VaccineId,
                HasComplication = h.HasComplication,
                LastVaccineDuration = h.LastVaccineDuration,
                Note = h.Note,
                NumberOfDoses = h.NumberOfDoses,
                Vaccine = new GetVaccineResponse
                {
                    Id = h.Vaccine.Id,
                    Name = h.Vaccine.Name,
                    FullName = h.Vaccine.FullName,
                    Schedules = h.Vaccine.Schedules.Select(s => new VaccineScheduleDto 
                    { 
                        Id = s.Id,
                        Dosage = s.Dosage,
                        Doses = s.Doses,
                        RouteOfAdministration = s.RouteOfAdministration,
                        AgeMin = s.AgeMin,
                        AgeMinUnit = s.AgeMinUnit,
                        AgeMax = s.AgeMax,
                        AgeMaxUnit = s.AgeMaxUnit,
                        Notes = s.Notes
                    }).ToList()
                }
            })
            .ToListAsync();
        return vaccinationHistories;
    }
}