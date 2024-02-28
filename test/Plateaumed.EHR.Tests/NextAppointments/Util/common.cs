using Abp.UI;
using Castle.MicroKernel;
using Microsoft.Extensions.Azure;
using MockQueryable.NSubstitute;
using NPOI.SS.Formula.Functions;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Misc.Country;
using Plateaumed.EHR.NextAppointments;
using Plateaumed.EHR.NextAppointments.Dtos;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.PhysicalExaminations.Dto;
using Plateaumed.EHR.Staff;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Twilio.Types;

namespace Plateaumed.EHR.Tests.NextAppointments.Util
{
    public class common
    {
        public static List<CreateNextAppointmentDto> GetCreateNextAppointment(long patientId = 1)
        {
            var returnModel = new List<CreateNextAppointmentDto>();

            returnModel.Add(new CreateNextAppointmentDto
            {
                Id = 0,
                PatientId = patientId,
                UnitId = 17,
                DateType = DateTypes.Day,
                SeenIn = 5,
                IsToSeeSameDoctor = true,
                AppointmentDate = DateTime.Now,
                EncounterId = 2
            });
            returnModel.Add(new CreateNextAppointmentDto
            {
                Id = 0,
                PatientId = patientId,
                UnitId = 18,
                DateType = DateTypes.Day,
                SeenIn = 5,
                IsToSeeSameDoctor = true,
                AppointmentDate = DateTime.Now,
                EncounterId = 2
            });
            returnModel.Add(new CreateNextAppointmentDto
            {

                Id = 1,
                PatientId = patientId,
                UnitId = 17,
                DateType = DateTypes.Day,
                SeenIn = 5,
                IsToSeeSameDoctor = true,
                AppointmentDate = DateTime.Now,
                EncounterId = 2
            });
            return returnModel;
        }

        public static GetUnitOrClinicRequestModel GetUnitOrClinicRequest(long patientId = 1)
        {
            return new GetUnitOrClinicRequestModel
            {
                UserId = 5,
                PatientId = patientId,
                TenantId = 1,
                FacilityId = 2,
                EncounterId = 2
            };
        }
        public static List<NextAppointmentUnitReturnDto> GetAllPosibleUnitsAndClinics()
        {
            var returnValue = new List<NextAppointmentUnitReturnDto>();
            var operationTime = new List<OperationTimeDto>();
            operationTime.Add(new OperationTimeDto()
            {
                Id = 1,
                DayOfTheWeek = DaysOfTheWeek.Sunday,
                OpeningTime = DateTime.Now,
                ClosingTime = DateTime.Now.AddHours(8)
            });

            returnValue.Add(new NextAppointmentUnitReturnDto()
            {
                Id = 1,
                Name = "Accident & Emergency (A & E) Medicine",
                IsClinic = false,
                OperatingTimes = operationTime
            });
            returnValue.Add(new NextAppointmentUnitReturnDto()
            {
                Id = 2,
                Name = "Neurology",
                IsClinic = false,
                OperatingTimes = operationTime
            });


            return returnValue;
        }
        public static List<OperationTimeDto> GetOperationUnitTime()
        {
            var operationTime = new List<OperationTimeDto>();
            operationTime.Add(new OperationTimeDto()
            {
                Id = 1,
                DayOfTheWeek = DaysOfTheWeek.Sunday,
                OpeningTime = DateTime.Now,
                ClosingTime = DateTime.Now.AddHours(8),
                DayOfTheWeekNo = 7
            });

            return operationTime;
        }
        public static List<NextAppointmentReturnDto> GetPatientAllNextAppointmentById(long patientId)
        {
            var result = new List<NextAppointmentReturnDto>();
            result.Add(new NextAppointmentReturnDto()
            {
                Id = 1,
                PatientId = patientId,
                UserId = 4,
                UnitId = 17,
                DateType = DateTypes.Day,
                SeenIn = 6,
                IsToSeeSameDoctor = true,
                DayOfTheWeek = DaysOfTheWeek.Sunday,
                AppointmentDate = DateTime.Now,
                Title = "Patient Next Appointment",
                Duration = 10
            });

            return result;
        }
        public static IQueryable<PatientEncounter> GetPatientEncounter(int tenantId, long patientId)
        {
            var result = new List<PatientEncounter>();
            result.Add(new PatientEncounter()
            {
                Id = 2,
                TenantId = tenantId,
                PatientId = patientId,
                AppointmentId = 1,
                TimeIn = DateTime.Now,
                TimeOut = DateTime.Now.AddHours(2),
                Status = EncounterStatusType.TransferOutPending,
                ServiceCentre = ServiceCentreType.InPatient,
                UnitId = 17,
                FacilityId = 2
            });

            return result.AsQueryable();
        }
        public static StaffMemberJobDto GetStaffFacilities(long doctorUserId, long facilityId)
        {
            var jobs = new List<StaffJobDto>();
            jobs.Add(new StaffJobDto()
            {
                JobLevelId = 1,
                FacilityId = 2,
                UnitId = 17,
                DepartmentId = 2,
                ServiceCentre = new List<ServiceCentreType>() { ServiceCentreType.OutPatient }
            }); 
            jobs.Add(new StaffJobDto()
            {
                JobLevelId = 1,
                FacilityId = 2,
                UnitId = 18,
                DepartmentId = 2,
                ServiceCentre = new List<ServiceCentreType>() { ServiceCentreType.OutPatient }
            });
            return new StaffMemberJobDto
            {
                FacilityId = facilityId,
                StaffMemberId = 4,
                UserId = doctorUserId,
                StaffCode = "3323432",
                ContractEndDate = DateTime.Now.AddYears(2),
                Jobs = jobs
            };
        }

        public static PatientEncounter GetPatientEncounter(long encounterId, bool isvalidPatientId = true)
        {
            var patientEncounters = new List<PatientEncounter>();
            patientEncounters.Add(new PatientEncounter()
            {
                Id = encounterId,
                TenantId = 1,
                PatientId = isvalidPatientId ? 1 : 0,
                Patient = new Patient()
                {
                    Id = isvalidPatientId ? 1 : 0,
                    GenderType = GenderType.Male,
                    FirstName = "James",
                    LastName = "Tolu",
                    PhoneNumber = "09023627xxxx",
                    DateOfBirth = DateTime.Now.AddDays(-20),
                    EmailAddress = "mail@mail.com",
                    Title = TitleType.Dr,
                    CountryId = 127,
                    UserId = 100
                },
                AppointmentId = 1,
                TimeIn = DateTime.Now,
                TimeOut = DateTime.Now,
                UnitId = 1,
                FacilityId = 1,
                AdmissionId = 1
            });
            return patientEncounters.FirstOrDefault();
        }
    }

    public class GetUnitOrClinicRequestModel
    {
        public long UserId { get; set; }
        [Required]
        public long PatientId { get; set; }
        [Required]
        public int? TenantId { get; set; }
        [Required]
        public long FacilityId { get; set; }
        [Required]
        public long EncounterId { get; set; }
    }
}
