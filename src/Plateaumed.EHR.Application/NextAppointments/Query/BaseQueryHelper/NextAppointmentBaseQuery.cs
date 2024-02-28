using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.NextAppointments.Abstractions;
using Plateaumed.EHR.NextAppointments.Dtos;
using Plateaumed.EHR.Organizations;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Staff;

namespace Plateaumed.EHR.NextAppointments.Query.BaseQueryHelper;
public class NextAppointmentBaseQuery : INextAppointmentBaseQuery
{
    private readonly IRepository<PatientAppointment, long> _patientAppointmentRepository;
    private readonly IRepository<OrganizationUnitExtended, long> _organizationUnitExtendedRepository;
    private readonly IRepository<StaffMember, long> _staffRepository;
    private readonly IRepository<FacilityStaff, long> _facilityRepository;
    private readonly IRepository<OrganizationUnitTime, long> _operationTimeRepository;
    private readonly IRepository<PatientEncounter, long> _encounterRepository;
    
    public NextAppointmentBaseQuery(
                IRepository<PatientAppointment, long> patientAppointmentRepository,
                IRepository<OrganizationUnitExtended, long> organizationUnitExtendedRepository,
                IRepository<StaffMember, long> staffRepository,
                IRepository<FacilityStaff, long> facilityRepository,
                IRepository<PatientEncounter, long> encounterRepository,
                IRepository<OrganizationUnitTime, long> operationTimeRepository
        )
    {
        _patientAppointmentRepository = patientAppointmentRepository;
        _organizationUnitExtendedRepository = organizationUnitExtendedRepository;
        _staffRepository = staffRepository;
        _facilityRepository = facilityRepository;
        _encounterRepository = encounterRepository;
        _operationTimeRepository = operationTimeRepository;
    }

    public async Task<List<NextAppointmentUnitReturnDto>> GetAllPossibleUnitsAndClinics(long userId, long patientId, int? tenantId, long facilityId, long encounterId)
    {
        List<NextAppointmentUnitReturnDto> returnUnits;
        //validate and get doctor role info using the userId

        //Get Staff id and other facility details
        var staffInfo = await GetStaffFacilities(userId, facilityId) ??
            throw new UserFriendlyException("Staff not in the selected facility.");

        //Get doctor and patient encounter
        var encounters = _encounterRepository.GetAll().Where(encounter => encounter.Id == encounterId && encounter.IsDeleted == false
                               && encounter.FacilityId == facilityId)
                        .Select(s=> new PatientDoctorEncounterDto
                          {
                              Id = s.Id,
                              TenantId = s.TenantId,
                              PatientId = s.PatientId,
                              StaffId = s.StaffEncounters.Any(staff => staff.StaffId == staffInfo.StaffMemberId) ? staffInfo.StaffMemberId : 0,
                              AppointmentId = s.AppointmentId,
                              TimeIn = s.TimeIn,
                              TimeOut = s.TimeOut,
                              Status = s.Status,
                              CreationTime = s.CreationTime,
                              CreatorUserId = s.CreatorUserId,
                              AdmissionId = s.AdmissionId,
                              FacilityId = s.FacilityId,
                              ServiceCentre = s.ServiceCentre,
                              UnitId = s.UnitId
                          }).FirstOrDefault()
                ?? throw new UserFriendlyException("Patient encounter not found.");

        if(encounters.StaffId == 0)
            throw new UserFriendlyException("Doctor encounter with the selected patient not found.");

        //Get all units and clinic within the facility
        var allFacilitiesUnits = await GetOrganizationUnits(facilityId) ?? 
                throw new UserFriendlyException("No unit or clinics found.");

        //Get staff Job => Unit,Clinics

        switch (encounters.ServiceCentre)
        {
            case ServiceCentreType.AccidentAndEmergency:
                //ServiceCentreType == A&E then return all unit at the hospital/Facility
                returnUnits = allFacilitiesUnits.
                    Where(unit => unit.Type == OrganizationUnitType.Unit).
                    Select(s => new NextAppointmentUnitReturnDto
                    {
                        Id = s.Id,
                        Name = s.DisplayName,
                        IsClinic = false,
                        OperatingTimes = s.OperatingTimes.Where(t => t.IsActive).Select(t => new OperationTimeDto
                        {
                            Id = t.Id,
                            DayOfTheWeek = t.DayOfTheWeek,
                            OpeningTime = t.OpeningTime,
                            ClosingTime = t.ClosingTime
                        }).ToList()
                    }).ToList();
                break;
            case ServiceCentreType.InPatient:
                //ServiceCentreType == InPatient return all unit the doctor is assign to
                returnUnits = (from all in allFacilitiesUnits
                                     join job in staffInfo.Jobs on all.Id equals job.UnitId
                                     where all.Type == OrganizationUnitType.Unit
                               select new NextAppointmentUnitReturnDto
                                     {
                                         Id = all.Id,
                                         Name = all.DisplayName,
                                         IsClinic = all.Type == OrganizationUnitType.Clinic,
                                         OperatingTimes = all.OperatingTimes.Where(t => t.IsActive).Select(t => new OperationTimeDto
                                         {
                                             Id = t.Id,
                                             DayOfTheWeek = t.DayOfTheWeek,
                                             OpeningTime = t.OpeningTime,
                                             ClosingTime = t.ClosingTime
                                         }).ToList()
                                     }).ToList();
                break;
            default:
                //ServiceCentreType == OutPatient return all clinics where the patient in the same clinic as the doctor
                returnUnits = (from all in allFacilitiesUnits
                               join job in staffInfo.Jobs on all.Id equals job.UnitId
                               where all.Id == encounters.UnitId && job.UnitId == encounters.UnitId && all.Type == OrganizationUnitType.Clinic
                               select new NextAppointmentUnitReturnDto
                               {
                                   Id = all.Id,
                                   Name = all.DisplayName,
                                   IsClinic = all.Type == OrganizationUnitType.Clinic,
                                   OperatingTimes = all.OperatingTimes.Where(t => t.IsActive).Select(t => new OperationTimeDto
                                   {
                                       Id = t.Id,
                                       DayOfTheWeek = t.DayOfTheWeek,
                                       OpeningTime = t.OpeningTime,
                                       ClosingTime = t.ClosingTime
                                   }).ToList()
                               }).ToList();
                break;
        }

        return returnUnits;
    }
    
    public async Task<NextAppointmentReturnDto> GetNextAppointmentById(long nextAppointmentId)
    {
        var result = await _patientAppointmentRepository.GetAll().
            Where(s => s.Id == nextAppointmentId).
            Select(s => new NextAppointmentReturnDto
            {
                Id = s.Id,
                Title = s.Title,
                Duration = s.Duration,
                PatientId = s.PatientId,
                UserId = s.AttendingPhysicianFk.UserId,
                UnitId = s.AttendingClinicId.GetValueOrDefault(),
                DateType = DateTypes.Date,
                SeenIn = 0,
                StaffId = (long)s.AttendingPhysicianId,
                IsToSeeSameDoctor = true,
                DayOfTheWeek = GetDayOfTheWeek(s.StartTime),
                AppointmentDate = s.StartTime
            }).FirstOrDefaultAsync();

        return result;
    }

    private static DaysOfTheWeek GetDayOfTheWeek(DateTime appointmentDate)
    {
        return appointmentDate.DayOfWeek switch
        {
            DayOfWeek.Monday => DaysOfTheWeek.Monday,
            DayOfWeek.Tuesday => DaysOfTheWeek.Tuesday,
            DayOfWeek.Wednesday => DaysOfTheWeek.Wednesday,
            DayOfWeek.Thursday => DaysOfTheWeek.Thursday,
            DayOfWeek.Friday => DaysOfTheWeek.Friday,
            DayOfWeek.Saturday => DaysOfTheWeek.Saturday,
            _ => DaysOfTheWeek.Sunday
        };
    }

    public async Task<List<NextAppointmentReturnDto>> GetNextAppointmentByDoctorId(long userId)
    {
        var staff = _staffRepository.GetAll().FirstOrDefault(s => s.UserId == userId) ?? new StaffMember();

        var result = await _patientAppointmentRepository.GetAll().Where(s => s.AttendingPhysicianId == staff.Id && s.IsDeleted == false
                    && s.StartTime >= DateTime.Now).
            Select(s => new NextAppointmentReturnDto
            {
                Id = s.Id,
                Title = s.Title,
                Duration = s.Duration,
                PatientId = s.PatientId,
                UserId = s.AttendingPhysicianFk.UserId,
                UnitId = s.AttendingClinicId.GetValueOrDefault(),
                DateType = DateTypes.Date,
                SeenIn = 0,
                StaffId = (long)s.AttendingPhysicianId,
                IsToSeeSameDoctor = true,
                DayOfTheWeek = GetDayOfTheWeek(s.StartTime),
                AppointmentDate = s.StartTime
            }).ToListAsync();

        return result;
    }

    public async Task<List<NextAppointmentReturnDto>> GetNextAppointmentByPatientId(long patientId)
    {
        var result = await _patientAppointmentRepository.GetAll().Where(s => s.PatientId == patientId && s.IsDeleted == false
                && s.StartTime >= DateTime.Now).
            Select(s => new NextAppointmentReturnDto
            {
                Id = s.Id,
                Title = s.Title,
                Duration = s.Duration,
                PatientId = s.PatientId,
                UserId = s.AttendingPhysicianFk.UserId,
                UnitId = s.AttendingClinicId.GetValueOrDefault(),
                DateType = DateTypes.Date,
                SeenIn = 0,
                StaffId = (long)s.AttendingPhysicianId,
                IsToSeeSameDoctor = true,
                DayOfTheWeek = GetDayOfTheWeek(s.StartTime),
                AppointmentDate = s.StartTime
            }).ToListAsync();

        return result;
    }

    public async Task<List<NextAppointmentReturnDto>> GetDoctorAndPatientAppointment(long patientId, long doctorUserId)
    {
        var staff = _staffRepository.GetAll().FirstOrDefault(s => s.UserId == doctorUserId) ?? new StaffMember();
        var result = await _patientAppointmentRepository.GetAll().Where(s => s.PatientId == patientId 
                && s.AttendingPhysicianId == staff.Id && s.IsDeleted == false
                && s.StartTime >= DateTime.Now).
            Select(s => new NextAppointmentReturnDto
            {
                Id = s.Id,
                Title = s.Title,
                Duration = s.Duration,
                PatientId = s.PatientId,
                UserId = s.AttendingPhysicianFk.UserId,
                UnitId = s.AttendingClinicId.GetValueOrDefault(),
                DateType = DateTypes.Date,
                SeenIn = 0,
                StaffId = (long)s.AttendingPhysicianId,
                IsToSeeSameDoctor = true,
                DayOfTheWeek = GetDayOfTheWeek(s.StartTime),
                AppointmentDate = s.StartTime
            }).ToListAsync();

        return result;
    }

    public async Task<List<OperationTimeDto>> GetOperationUnitTime(long unitId)
    {
        var result = await _operationTimeRepository.GetAll().Where(s => s.OrganizationUnitExtendedId == unitId && s.IsDeleted == false).
            Select(s => new OperationTimeDto
            {
                Id = s.OrganizationUnitExtendedId.GetValueOrDefault(),
                DayOfTheWeek = s.DayOfTheWeek,
                OpeningTime = s.OpeningTime,
                ClosingTime = s.ClosingTime,
                DayOfTheWeekNo = s.DayOfTheWeek == DaysOfTheWeek.Monday ? 1 :
                                 s.DayOfTheWeek == DaysOfTheWeek.Tuesday ? 2 :
                                 s.DayOfTheWeek == DaysOfTheWeek.Wednesday ? 3 :
                                 s.DayOfTheWeek == DaysOfTheWeek.Thursday ? 4 :
                                 s.DayOfTheWeek == DaysOfTheWeek.Friday ? 5 :
                                 s.DayOfTheWeek == DaysOfTheWeek.Saturday ? 6: 7
            }).OrderBy(o => o.DayOfTheWeekNo).ToListAsync();

        return result;
    }

    public async Task<StaffMemberJobDto> GetStaffFacilities(long doctorUserId, long facilityId)
    {
        var staff = await (from staffMember in _staffRepository.GetAll().Include(x=>x.Jobs)
                                     join facility in _facilityRepository.GetAll() on staffMember.Id equals facility.StaffMemberId
                                     where staffMember.UserId == doctorUserId && facility.FacilityId == facilityId
                                     select new StaffMemberJobDto
                                     {
                                         FacilityId = facility.FacilityId,
                                         StaffMemberId = facility.StaffMemberId,
                                         UserId = staffMember.UserId,
                                         StaffCode = staffMember.StaffCode,
                                         ContractEndDate = staffMember.ContractEndDate,
                                         Jobs = staffMember.Jobs.Count == 0 ? new List<StaffJobDto>() : 
                                         staffMember.Jobs.Where(f=>f.FacilityId == facilityId).Select(job=> new StaffJobDto
                                         {
                                             JobLevelId = job.JobLevelId,
                                             FacilityId = job.FacilityId,
                                             UnitId = job.UnitId,
                                             DepartmentId = job.DepartmentId,
                                             ServiceCentre = job.JobServiceCentres.Count == 0 ? new List<ServiceCentreType> { ServiceCentreType.OutPatient } : job.JobServiceCentres.Select(sc => sc.ServiceCentre).ToList()
                                         }).ToList()
                                     }).FirstOrDefaultAsync();

        return staff;
    }

    private async Task<List<OrganizationUnitExtended>> GetOrganizationUnits(long facilityId)
    {
        var orgUnit = _organizationUnitExtendedRepository.GetAll();
        var operationTime = _operationTimeRepository.GetAll();

        var organizationUnitsExtended = await orgUnit.
            Where(unit => unit.FacilityId == facilityId)
                      .Select(s => new OrganizationUnitExtended
                      {
                          Id = s.Id,
                          ParentId = s.ParentId,
                          Code = s.Code,
                          DisplayName = s.DisplayName,
                          TenantId = s.TenantId,
                          ShortName = s.ShortName,
                          IsActive = s.IsActive,
                          Type = s.Type,
                          FacilityId = s.FacilityId,
                          OperatingTimes = operationTime.Where(pt => pt.OrganizationUnitExtendedId == s.Id).ToList(),
                      }).ToListAsync();

        return organizationUnitsExtended;
    }

    public async Task<OrganizationUnitExtended> GetOrganizationUnit(long unitId)
    {
        var orgUnit = _organizationUnitExtendedRepository.GetAll();
        var operationTime = _operationTimeRepository.GetAll();

        var organizationUnitsExtended = await orgUnit.
            Where(unit => unit.Id == unitId)
                      .Select(s => new OrganizationUnitExtended
                      {
                          Id = s.Id,
                          ParentId = s.ParentId,
                          Code = s.Code,
                          DisplayName = s.DisplayName,
                          TenantId = s.TenantId,
                          ShortName = s.ShortName,
                          IsActive = s.IsActive,
                          Type = s.Type,
                          FacilityId = s.FacilityId,
                          OperatingTimes = operationTime.Where(s => s.OrganizationUnitExtendedId == s.Id).ToList(),
                      }).FirstOrDefaultAsync();

        return organizationUnitsExtended;
    }
}
