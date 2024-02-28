using Abp.Application.Services.Dto;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Authorization.Users.Dto;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Facilities.Dtos;
using Plateaumed.EHR.Investigations;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Organizations;
using Plateaumed.EHR.Organizations.Dto;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Staff.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using Plateaumed.EHR.Patients.Dtos;
using Plateaumed.EHR.Symptom.Dtos;
using Plateaumed.EHR.Symptom;
using Plateaumed.EHR.PhysicalExaminations.Dto;
using Plateaumed.EHR.PhysicalExaminations;
using Plateaumed.EHR.VitalSigns.Dto;
using MockQueryable.NSubstitute;
using Plateaumed.EHR.ReviewAndSaves.Dtos;
using Plateaumed.EHR.Diagnoses.Dto;
using Plateaumed.EHR.Discharges.Dtos;
using Plateaumed.EHR.Staff;
using Plateaumed.EHR.NurseCarePlans;

namespace Plateaumed.EHR.Tests.ReviewAndSaves.Util
{
    public class Common
    {
        public static GetStaffMemberResponse GetStaffByUserId(long staffUserId)
        {
            var Ids = new EntityDto<long>();
            Ids.Id = staffUserId;
            var staffMembers = new GetStaffMemberResponse
            {
                User = GetAlUsers().FirstOrDefault(s => s.Id == 1),
                StaffCode = "930039",
                ContractStartDate = DateTime.Now.AddYears(-3),
                ContractEndDate = DateTime.Now.AddYears(3),
                AdminRole = "Doctor",
                Jobs = GetJobs().Where(s => s.Id == 1).ToList()
            };

            return staffMembers;
        }
        public static GetStaffMemberResponse GetDoctorByUserId_No_Job(long staffUserId)
        {
            var Ids = new EntityDto<long>();
            Ids.Id = staffUserId;
            var staffMembers = new GetStaffMemberResponse
            {
                User = GetAlUsers().FirstOrDefault(s => s.Id == 1),
                StaffCode = "930039",
                ContractStartDate = DateTime.Now.AddYears(-3),
                ContractEndDate = DateTime.Now.AddYears(3),
                AdminRole = "Doctor",
                Jobs = null
            };

            return staffMembers;
        }
        public static User GetUserById(long staffUserId)
        {
            var user = GetAlUsers().FirstOrDefault(s => s.Id == staffUserId);
            return new User()
            {
                Id = user.Id.Value,
                Title = user.Title,
                Name = user.Name,
                MiddleName = user.MiddleName,
                Surname = user.Surname,
                UserName = user.UserName,
                DateOfBirth = user.DateOfBirth,
                Gender = user.Gender,
                EmailAddress = user.EmailAddress
            };
        }

        public static GetStaffMemberResponse GetDoctorByUserId_No_JobUnit_Id(long staffUserId)
        {
            var Ids = new EntityDto<long>();
            Ids.Id = staffUserId;
            var staffMembers = new GetStaffMemberResponse
            {
                User = GetAlUsers().FirstOrDefault(s => s.Id == 1),
                StaffCode = "930039",
                ContractStartDate = DateTime.Now.AddYears(-3),
                ContractEndDate = DateTime.Now.AddYears(3),
                AdminRole = "Doctor",
                Jobs = GetJobs().Where(s => s.Id == 2).ToList()
            };

            return staffMembers;
        }
        public static GetStaffMemberResponse GetDoctorByUserId_No_JobLevel_Id(long staffUserId)
        {
            var Ids = new EntityDto<long>();
            Ids.Id = staffUserId;
            var staffMembers = new GetStaffMemberResponse
            {
                User = GetAlUsers().FirstOrDefault(s => s.Id == 1),
                StaffCode = "930039",
                ContractStartDate = DateTime.Now.AddYears(-3),
                ContractEndDate = DateTime.Now.AddYears(3),
                AdminRole = "Doctor",
                Jobs = GetJobs_With_NoLevels().Where(s => s.Id == 2).ToList()
            };

            return staffMembers;
        }
        public static List<UserEditDto> GetAlUsers()
        {
            var Users = new List<UserEditDto>();
            Users.Add(new UserEditDto()
            {
                Id = 1,
                Title = TitleType.Dr,
                Name = "James Obi",
                MiddleName = "",
                Surname = "Obi",
                UserName = "James101",
                DateOfBirth = DateTime.Now.AddYears(-25),
                Gender = GenderType.Male,
                EmailAddress = "james10101@yahoo.com"
            });
            Users.Add(new UserEditDto()
            {
                Id = 2,
                Title = TitleType.Mrs,
                Name = "Sarah Obi",
                MiddleName = "",
                Surname = "Obi",
                UserName = "Sarah101",
                DateOfBirth = DateTime.Now.AddYears(-25),
                Gender = GenderType.Female,
                EmailAddress = "Sarah10101@yahoo.com"
            });
            Users.Add(new UserEditDto()
            {
                Id = 3,
                Title = TitleType.Mr,
                Name = "Michael Obi",
                MiddleName = "",
                Surname = "Obi",
                UserName = "Michael101",
                DateOfBirth = DateTime.Now.AddYears(-25),
                Gender = GenderType.Male,
                EmailAddress = "Michael10101@yahoo.com"
            });
            Users.Add(new UserEditDto()
            {
                Id = 4,
                Title = TitleType.Dr,
                Name = "Steve Toyin",
                MiddleName = "",
                Surname = "Toyin",
                UserName = "Toyin101",
                DateOfBirth = DateTime.Now.AddYears(-35),
                Gender = GenderType.Male,
                EmailAddress = "Toyin@yahoo.com"
            });
            return Users;
        }
        public static List<Job> GetStaffJobs()
        {
            var entity = new List<Job>();
            var serviceCenter = new List<ServiceCentreType>();
            serviceCenter.Add(ServiceCentreType.AccidentAndEmergency);
            entity.Add(new Job()
            {
                Id = 1,
                IsPrimary = true,
                FacilityId = 1,
                JobLevelId = 1,
                DepartmentId = 1,
                UnitId = 1
            });
            return entity;
        }
        public static List<JobDto> GetJobs()
        {
            var entity = new List<JobDto>();
            var serviceCenter = new List<ServiceCentreType>();
            serviceCenter.Add(ServiceCentreType.AccidentAndEmergency);
            entity.Add(new JobDto()
            {
                Id = 1,
                IsPrimary = true,
                FacilityId = 1,
                Facility = GetFacilities().FirstOrDefault(s=>s.Id == 1),
                JobTitleId = 1,
                JobTitle = GetJobTitle().FirstOrDefault(s => s.Id == 1),
                JobLevelId = 1,
                JobLevel = GetJobLevels().FirstOrDefault(s => s.Id == 1),
                TeamRole = "Doctor",
                DepartmentId = 1,
                Department = GetOrganizationUnit().FirstOrDefault(s => s.Type == "Department"),
                UnitId = 1,
                Unit = GetOrganizationUnit().FirstOrDefault(s => s.Id == 1),
                ServiceCentres = serviceCenter
            });
            entity.Add(new JobDto()
            {
                Id = 2,
                IsPrimary = true,
                FacilityId = 1,
                Facility = GetFacilities().FirstOrDefault(s => s.Id == 1),
                JobTitleId = 1,
                JobTitle = GetJobTitle().FirstOrDefault(s => s.Id == 1),
                JobLevelId = 1,
                JobLevel = GetJobLevels().FirstOrDefault(s => s.Id == 1),
                TeamRole = "Doctor",
                DepartmentId = 1,
                Department = GetOrganizationUnit().FirstOrDefault(s => s.Type == "Department"),
                ServiceCentres = serviceCenter
            });
            return entity;
        }

        public static List<JobDto> GetJobs_With_NoLevels()
        {
            var entity = new List<JobDto>();
            var serviceCenter = new List<ServiceCentreType>();
            serviceCenter.Add(ServiceCentreType.AccidentAndEmergency);
            entity.Add(new JobDto()
            {
                Id = 1,
                IsPrimary = true,
                FacilityId = 1,
                Facility = GetFacilities().FirstOrDefault(s => s.Id == 1),
                JobTitleId = 1,
                JobTitle = GetJobTitle().FirstOrDefault(s => s.Id == 1),
                JobLevelId = 0,
                JobLevel = null,
                TeamRole = "Doctor",
                DepartmentId = 1,
                Department = GetOrganizationUnit().FirstOrDefault(s => s.Type == "Department"),
                UnitId = 1,
                Unit = GetOrganizationUnit().FirstOrDefault(s => s.Id == 1),
                ServiceCentres = serviceCenter
            });
            entity.Add(new JobDto()
            {
                Id = 2,
                IsPrimary = true,
                FacilityId = 1,
                Facility = GetFacilities().FirstOrDefault(s => s.Id == 1),
                JobTitleId = 1,
                JobTitle = GetJobTitle().FirstOrDefault(s => s.Id == 1),
                JobLevelId = 0,
                JobLevel = null,
                TeamRole = "Doctor",
                DepartmentId = 1,
                Department = GetOrganizationUnit().FirstOrDefault(s => s.Type == "Department"),
                UnitId = 1,
                Unit = GetOrganizationUnit().FirstOrDefault(s => s.Id == 1),
                ServiceCentres = serviceCenter
            });
            return entity;
        }

        public static List<FacilityDto> GetFacilities()
        {
            var entity = new List<FacilityDto>();
            entity.Add(new FacilityDto()
            {
                Id = 1,
                Name = "Mikano",
                EmailAddress = "EmailAddress@mail.com",
                PhoneNumber = "08023727327",
                Level = FacilityLevel.Primary
            });
            entity.Add(new FacilityDto()
            {
                Id = 2,
                Name = "lagos builden",
                EmailAddress = "EmailAddress@mail.com",
                PhoneNumber = "08023727327",
                Level = FacilityLevel.Primary
            });
            entity.Add(new FacilityDto()
            {
                Id = 3,
                Name = "child builden",
                EmailAddress = "EmailAddress@mail.com",
                PhoneNumber = "08023727327",
                Level = FacilityLevel.Primary
            });
            return entity;
        }

        public static List<Facility> DBFacility()
        {
            var entity = new List<Facility>();
            entity.Add(new Facility()
            {
                Id = 1,
                Name = "Mikano",
                EmailAddress = "EmailAddress@mail.com",
                PhoneNumber = "08023727327",
                Level = FacilityLevel.Primary
            });
            entity.Add(new Facility()
            {
                Id = 2,
                Name = "lagos builden",
                EmailAddress = "EmailAddress@mail.com",
                PhoneNumber = "08023727327",
                Level = FacilityLevel.Primary
            });
            entity.Add(new Facility()
            {
                Id = 3,
                Name = "child builden",
                EmailAddress = "EmailAddress@mail.com",
                PhoneNumber = "08023727327",
                Level = FacilityLevel.Primary
            });
            return entity;
        }
        public static List<JobTitleDto> GetJobTitle()
        {
            var entity = new List<JobTitleDto>();
            entity.Add(new JobTitleDto()
            {
                Id = 1,
                Name = "Doctor",
                ShortName = "Dr",
                IsActive = true,
                FacilityId = 1,
                JobLevels = null
            });
            entity.Add(new JobTitleDto()
            {
                Id = 2,
                Name = "Nurse",
                ShortName = "Nurse",
                IsActive = true,
                FacilityId = 1,
                JobLevels = null
            });
            entity.Add(new JobTitleDto()
            {
                Id = 3,
                Name = "Pharmacist",
                ShortName = "Pharmacist",
                IsActive = true,
                FacilityId = 1,
                JobLevels = null
            });
            return entity;
        }

        public static List<JobLevelDto> GetJobLevels()
        {
            var entity = new List<JobLevelDto>();
            entity.Add(new JobLevelDto()
            {
                Id = 1,
                Name = "Consultant",
                ShortName = "C",
                IsActive = true,
                Rank = 1,
                JobTitleId = 1
            });
            entity.Add(new JobLevelDto()
            {
                Id = 2,
                Name = "Chief Matron",
                ShortName = "CM",
                IsActive = true,
                Rank = 2,
                JobTitleId = 2
            });
            entity.Add(new JobLevelDto()
            {
                Id = 3,
                Name = "Chief Pharmacist",
                ShortName = "CP",
                IsActive = true,
                Rank = 3,
                JobTitleId = 3
            });
            return entity;
        }

        
        public static List<OrganizationUnitExtended> GetOrganizationUnitExtended()
        {
            var entity = new List<OrganizationUnitExtended>();
            entity.Add(new OrganizationUnitExtended()
            {
                Id = 1,
                ParentId = 1,
                Code = "00001",
                DisplayName = "Accident & Emergency (A & E) Medicine",
                ShortName = "A & E Medicine",
                IsActive = true,
                Type = OrganizationUnitType.Unit,
                FacilityId = 1,

            });
            entity.Add(new OrganizationUnitExtended()
            {
                Id = 2,
                ParentId = 1,
                Code = "00001.00001",
                DisplayName = "Neurology",
                ShortName = "Neuro Medicine",
                IsActive = true,
                Type = OrganizationUnitType.Department,
                FacilityId = 1,
            });
            entity.Add(new OrganizationUnitExtended()
            {
                Id = 3,
                ParentId = 1,
                Code = "00001.00002",
                DisplayName = "Renal",
                ShortName = "Clinic",
                IsActive = true,
                Type = OrganizationUnitType.Clinic,
                FacilityId = 1,
            });
            return entity;
        }
        public static List<OrganizationUnitDto> GetOrganizationUnit()
        {
            var entity = new List<OrganizationUnitDto>();
            entity.Add(new OrganizationUnitDto()
            {
                Id = 1,
                ParentId = 1,
                Code = "00001",
                DisplayName = "Accident & Emergency (A & E) Medicine",
                ShortName = "A & E Medicine",
                IsActive = true,
                Type = "Unit",
                FacilityId = 1,
                
            });
            entity.Add(new OrganizationUnitDto()
            {
                Id = 2,
                ParentId = 1,
                Code = "00001.00001",
                DisplayName = "Neurology",
                ShortName = "Neuro Medicine",
                IsActive = true,
                Type = "Department",
                FacilityId = 1,
            });
            entity.Add(new OrganizationUnitDto()
            {
                Id = 3,
                ParentId = 1,
                Code = "00001.00002",
                DisplayName = "Renal",
                ShortName = "Clinic",
                IsActive = true,
                Type = "Clinic",
                FacilityId = 1,
            });
            return entity;
        }
        public static PagedResultDto<JobTitleDto> GetStaffJobs(GetAllJobTitlesInput request)
        {
            var results = new List<JobTitleDto>();
            results = GetJobTitle();
            return new PagedResultDto<JobTitleDto>(results.Count, results);
        }
        public static StaffMember GetStaffMember(long userId)
        {
            var results = new StaffMember()
            {
                Id = 1,
                UserId = userId,
                UserFk = null,
                StaffCode = "0001",
                AdminRoleId = 1,
                ContractStartDate = DateTime.Now,
                ContractEndDate = DateTime.Now,
                Jobs = GetStaffJobs()
            };
            return results;
        }
        public static List<GetStaffMembersResponse> GetDoctorsByUnit(long unitId, long facilityId, long jobTitleId)
        {
            var request = new GetAllStaffMembersRequest()
            {
                JobTitleIdFilter = jobTitleId,
                FacilityIdFilter = facilityId
            };
            var staffMembers = new List<GetStaffMembersResponse>();
            var user = GetAlUsers().Where(s=>s.Id ==1).FirstOrDefault();
            staffMembers.Add(new GetStaffMembersResponse()
            {
                Id = 1,
                Title = user.Title,
                Name = user.Name,
                MiddleName = user.MiddleName,
                Surname = user.Surname,
                EmailAddress = user.EmailAddress,
                PhoneNumber = user.PhoneNumber,
                JobTitle = "Doctor",
                JobLevel = "Consultant",
                StaffCode = "0010",
                Department = "A&E",
                Unit = "A & E Medicine",
                UnitId = 1,
                ContractStartDate = DateTime.Now.AddYears(-3),
                ContractEndDate = DateTime.Now.AddYears(3),
                IsActive = true
            });
            user = GetAlUsers().Where(s => s.Id == 4).FirstOrDefault();
            staffMembers.Add(new GetStaffMembersResponse()
            {
                Id = 2,
                Title = user.Title,
                Name = user.Name,
                MiddleName = user.MiddleName,
                Surname = user.Surname,
                EmailAddress = user.EmailAddress,
                PhoneNumber = user.PhoneNumber,
                JobTitle = "Doctor",
                JobLevel = "Consultant",
                StaffCode = "0010",
                Department = "A&E",
                Unit = "A & E Medicine",
                UnitId = 1,
                ContractStartDate = DateTime.Now.AddYears(-1),
                ContractEndDate = DateTime.Now.AddYears(3),
                IsActive = true
            });

            var _list = staffMembers.Where(u => u.UnitId == unitId).ToList();

            return _list;
        }

        public static List<Patient> GetPatients(long patientId)
        {
            var results = new List<Patient>();
            results.Add(new Patient()
            {
                Id = patientId,
                UuId = Guid.NewGuid(),
                GenderType = GenderType.Male,
                FirstName = "Michael",
                LastName = "Philip",
                PhoneNumber = "08023723722",
                DateOfBirth = DateTime.Now.AddDays(-20),
                EmailAddress = "sm@mail.com",
                IsNewToHospital = true,
                Title = TitleType.Mr,
                MiddleName = "Joe",
                CountryId = 127,
                UserId = 100
            });
            return results;
        }
        public static GetPatientDetailsOutDto GetPatientDetails(long patientId)
        {
            var patient = GetPatients(patientId).FirstOrDefault();
            var results = new GetPatientDetailsOutDto()
            {
                Id = patientId,
                FullName = $"{patient.FirstName} {patient.LastName}",
                DateOfBirth = patient.DateOfBirth,
                Gender = patient.GenderType,
                PatientCode = "0001",
                LengthOfStayDays = 365,
                LastSeenByDoctor = DateTime.Now,
                LastSeenByDoctorName = "Dr. Jame"
            };
            return results;
        }
        public static IQueryable<PatientEncounter> GetPatientEncounters(long patientId, bool isIncludePatient = true)
        {
            var entity = new List<PatientEncounter>();
            entity.Add(new PatientEncounter()
            {
                Id = 1,
                TenantId = 1,
                PatientId = patientId,
                Patient = isIncludePatient ? GetPatients(1).FirstOrDefault() : null,
                AppointmentId = 1,
                TimeIn = DateTime.Now,
                TimeOut = DateTime.Now,
                Status = EncounterStatusType.InProgress,
                ServiceCentre = ServiceCentreType.AccidentAndEmergency,
                UnitId = 1,
                Unit = GetOrganizationUnitExtended().FirstOrDefault(s=>s.Id == 1),
                FacilityId = 1,
                Facility = DBFacility().FirstOrDefault(s => s.Id == 1),
                InvestigationResults = InvestigationResults(),
            });
            return entity.AsQueryable().BuildMock();
        }
        public static IQueryable<PatientEncounter> GetPatientEncounters(long encounterId, long staffId, long patientId, bool isPatientEncounterSet = true, bool isIncludePatient = true)
        {
            var entity = new List<PatientEncounter>();
            entity.Add(new PatientEncounter()
            {
                Id = encounterId,
                TenantId = 1,
                CreatorUserId = staffId,
                Patient = isIncludePatient ? GetPatients(patientId).FirstOrDefault() : null,
                FacilityId = 1,
                InvestigationResults = InvestigationResults(),
            });
            return entity.AsQueryable().BuildMock();
        }

        public static string GetNoteTitle()
        {
            return "Patient seen";
        }
        public static string GenerateNoteIntroduction()
        {
            return $"A 20 year old male, who presented at A & E Medicine";
        }
        public static List<InvestigationResult> InvestigationResults()
        {
            var results = new List<InvestigationResult>();
            results.Add(new InvestigationResult()
            {
                Id = 1,
                TenantId = 1, 
                PatientId = 1,
                InvestigationId = 1,
                InvestigationRequestId = 1,
                Name = "Inestigation Name",
                Reference = "Reference",
                SampleCollectionDate = DateOnly.MinValue,
                ResultDate = DateOnly.MaxValue,
                SampleTime = TimeOnly.MinValue,
                ResultTime = TimeOnly.MaxValue,
                Specimen = "Specimen",
                Conclusion = "Conclusion",
                SpecificOrganism = "SpecificOrganism",
                View = "View",
                Notes = "Sample Notes",
                EncounterId = 1
            });
            return results;
        }

        public static List<PatientSymptomSummaryForReturnDto> GetPatientSymptoms()
        {
            var results = new List<PatientSymptomSummaryForReturnDto>();
            results.Add(new PatientSymptomSummaryForReturnDto()
            {
                Id = 1,
                SymptomEntryTypeName = "Suggestion",
                SymptomEntryType = SymptomEntryType.Suggestion,
                Description = "Headache",
                SuggestionSummary = "Sample Summary",
                TypeNotes = null,
                CreatorUserId = 1,
                CreationTime = DateTime.Now,
                DeletionTime = DateTime.Now,
                AppointmentId = 1,
                OtherNote = string.Empty,
                JsonData = string.Empty
            });
            var typeNote = new List<SymptomTypeNoteRequestDto>();
            typeNote.Add(new SymptomTypeNoteRequestDto()
            {
                Type = "Family History",
                Note = "THis is a sample typed note"
            });
            results.Add(new PatientSymptomSummaryForReturnDto()
            {
                Id = 2,
                SymptomEntryTypeName = "TypeNote",
                SymptomEntryType = SymptomEntryType.TypeNote,
                Description = "Family History",
                SuggestionSummary = "",
                TypeNotes = typeNote,
                CreatorUserId = 1,
                CreationTime = DateTime.Now,
                DeletionTime = DateTime.Now,
                AppointmentId = 1,
                OtherNote = string.Empty,
                JsonData = string.Empty
            });
            return results;
        }

        public static List<PatientPhysicalExaminationResponseDto> GetPatientPhysicalExamination(long patientId)
        {
            var suggestionAnswer = new List<PatientPhysicalExamSuggestionAnswerDto>();
            suggestionAnswer.Add(new PatientPhysicalExamSuggestionAnswerDto()
            {
                SnowmedId = "79014000",
                Description = "This patient looks dehydrated",
                IsAbsent = true
            });
            var suggestion = new List<PatientPhysicalExamSuggestionQuestionDto>();
            suggestion.Add(new PatientPhysicalExamSuggestionQuestionDto()
            {
                HeaderName = "Inspection",
                PatientPhysicalExamSuggestionAnswers = suggestionAnswer
            });
            var results = new List<PatientPhysicalExaminationResponseDto>();
            results.Add(new PatientPhysicalExaminationResponseDto()
            {
                Id = 1,
                PhysicalExaminationEntryType = PhysicalExaminationEntryType.Suggestion,
                PhysicalExaminationEntryTypeName = "General Physical Examination",
                PhysicalExaminationTypeId = 1,
                PhysicalExaminationType = null,
                PatientId = patientId,
                OtherNote = "Sample note",
                CreationTime = DateTime.Now,
                DeletionTime = DateTime.Now,
                TypeNotes = null,
                Suggestions = suggestion
            });
            var typeNote = new List<PatientPhysicalExamTypeNoteRequestDto>();
            typeNote.Add(new PatientPhysicalExamTypeNoteRequestDto()
            {
                Type = "Dehydrated",
                Note = "This patient looks dehydrated"
            });
            results.Add(new PatientPhysicalExaminationResponseDto()
            {
                Id = 2,
                PhysicalExaminationEntryType = PhysicalExaminationEntryType.TypeNote,
                PhysicalExaminationEntryTypeName = "Dehydrated",
                PhysicalExaminationTypeId = 1,
                PhysicalExaminationType = null,
                PatientId = patientId,
                OtherNote = "",
                CreationTime = DateTime.Now,
                DeletionTime = DateTime.Now,
                TypeNotes = typeNote,
                Suggestions = null
            });
            return results;
        }

        public static List<PatientVitalsSummaryResponseDto> GetVitalsSummaryResponse(long patientId)
        {
            var vitalSigns = new List<PatientVitalResponseDto>();
            vitalSigns.Add(new PatientVitalResponseDto()
            {
                Id = 1,
                PatientId = patientId,
                PainScale = 1,
                VitalSignId = 1,
                VitalSign = new GetSimpleVitalSignsResponse()
                {
                    Id = 1,
                    Sign = "Eye",
                    LeftRight = true,
                    DecimalPlaces = 2
                },
                MeasurementSiteId = 1,
                MeasurementRangeId = 1,
                VitalReading = 98,
                ProcedureId = 1,
                CreationTime = DateTime.Now,
                LastModificationTime = DateTime.Now,
                PatientVitalType = "79014000",
                Position = "Left"
            });
            var results = new List<PatientVitalsSummaryResponseDto>();
            results.Add(new PatientVitalsSummaryResponseDto()
            {
                Date = DateTime.Now,
                PatientVitals = vitalSigns,
                Time = DateTime.Now
            });
            return results;
        }

        public static IQueryable<UploadedImageDto> GetPhysicalExaminationImages(long patientPhysicalExaminationId)
        {
            var images = new List<UploadedImageDto>();
            images.Add(new UploadedImageDto()
            {
                Id = patientPhysicalExaminationId,
                FileName = "filename.jpg",
                FileUrl = "filename.jpg",
            });

            return images.AsQueryable().BuildMock();
        }
        public static List<PatientTypeNoteDto> GetPatientTypeNoteDto(long doctorUserId, List<PatientSymptomSummaryForReturnDto> symptoms,
                                                            List<PatientPhysicalExaminationResponseDto> physicalExamination)
        {
            var typedNote = new List<PatientTypeNoteDto>();
            var presentingComplaintsTypeNote = symptoms == null ? null : symptoms.Where(x => x.CreatorUserId == doctorUserId &&
                                                                        x.SymptomEntryType == Symptom.SymptomEntryType.TypeNote)
                                                                        .Select(s => s.TypeNotes).ToList();

            foreach (var item in presentingComplaintsTypeNote)
            {
                typedNote.AddRange(item.Select(s => new PatientTypeNoteDto()
                {
                    Type = s.Type,
                    Note = s.Note
                }).ToList());
            }

            //Filter Physical Examination Type notes
            var physicalExaminationTypeNote = physicalExamination.Where(t => t.PhysicalExaminationEntryType == PhysicalExaminationEntryType.TypeNote)
                                                                        .ToList();
            foreach (var item in physicalExaminationTypeNote)
            {
                var tnote = item.TypeNotes;
                var patientPyhicalExamImages = item.ImageUploaded ? GetPhysicalExaminationImages(item.Id).ToList() : null;
                typedNote.AddRange(tnote.Select(s => new PatientTypeNoteDto()
                {
                    Type = s.Type,
                    Note = s.Note,
                    ImageFiles = patientPyhicalExamImages,
                    ImageUploaded = item.ImageUploaded
                }).ToList());
            }

            return typedNote;
        }

        public static List<PatientPhysicalExaminationDto> GeneratePatientPhysicalExamResult(List<PatientPhysicalExaminationResponseDto> physicalExamination)
        {
            var patientPhysicalExaminationResult = new List<PatientPhysicalExaminationDto>();
            var physicalExaminationSuggestion = physicalExamination == null ? null : physicalExamination.Where(x => x.PhysicalExaminationEntryType == PhysicalExaminationEntryType.Suggestion).ToList();
            foreach (var item in physicalExaminationSuggestion)
            {
                var suggestionAnswers = item.Suggestions;
                var entryDate = item.CreationTime;
                var patientPyhicalExamImages = item.ImageUploaded ? GetPhysicalExaminationImages(item.Id).ToList() : null;
                var results = suggestionAnswers.Select(s =>
                {
                    var answers = s.PatientPhysicalExamSuggestionAnswers.Select(a => a.Description).ToList();
                    var suggestionQuestion = new PatientPhysicalExaminationDto()
                    {
                        Header = s.HeaderName,
                        Answer = String.Join(",", answers),
                        ImageFiles = patientPyhicalExamImages,
                        CreatedAt = entryDate,
                        ImageUploaded = item.ImageUploaded
                    };

                    return suggestionQuestion;
                }).ToList();

                patientPhysicalExaminationResult.AddRange(results);
            }

            return patientPhysicalExaminationResult;
        }

        public static List<PatientVitalsSummaryResponseDto> GetPatientVitalSigns(long patientId)
        {
            //Get patient vital signs
            var vitalSign = GetVitalsSummaryResponse(patientId);

            var patientVitalSign = new List<PatientVitalsSummaryResponseDto>();
            foreach (var item in vitalSign)
            {
                var vitals = item.PatientVitals;
                var results = vitals.Select(s =>
                {
                    var vitalsigns = s.VitalSign;

                    var vitalSignsResponse = new List<PatientVitalResponseDto>();
                    vitalSignsResponse.Add(new PatientVitalResponseDto()
                    {
                        Id = s.Id,
                        VitalSign = s.VitalSign,
                        MeasurementSite = s.MeasurementSite,
                        MeasurementRange = s.MeasurementRange,
                        PatientVitalType = s.PatientVitalType,
                        Position = s.Position,
                        VitalReading = s.VitalReading
                    });

                    var vitals = new PatientVitalsSummaryResponseDto()
                    {
                        Date = DateTime.Now,
                        PatientVitals = vitalSignsResponse,
                        Time = DateTime.Now
                    };

                    return vitals;
                }).ToList();

                patientVitalSign.AddRange(results);
            }

            return patientVitalSign;
        }

        public static List<DiagnosisDto> GetPatientDiagnosis(long patientId, long encounterId)
        {
            var disgnosis = new List<DiagnosisDto>();
            //Get Patient Diagnosis
            disgnosis
                .Add(new DiagnosisDto()
                {
                    TenantId = 1,
                    PatientId = patientId,
                    EncounterId = encounterId,
                    Sctid = 1,
                    Description = "sample",
                    Notes = "Notes",
                    Status = 1,
                    CreatedAt = DateTime.Now,
                    CreatorUserId = 1
                });

            return disgnosis;
        }
        
        public static DischargeDto GetPatientDischarge(long patientId, long encounterId)
        {
            return new DischargeDto()
            {
                Id = 1,
                PatientId = patientId,
                EncounterId = encounterId,
                DischargeType = DoctorDischarge.DischargeEntryType.NORMAL,
                status = DoctorDischarge.DischargeStatus.INITIATED,
                AppointmentId = 1,
                DateOfDeath = DateTime.Now,
                IsBroughtInDead = false
            };
        }

        public static IQueryable<NursingCareSummary> GetNurseSummary(long patientId, long encounterId, bool isEncounterSet = true, bool isIncludePatientSet = true)
        {
            var entity = new List<NursingCareSummary>();
            entity.Add(new NursingCareSummary()
            {
                TenantId = 1,
                PatientId = patientId,
                Patient = GetPatients(patientId).FirstOrDefault(),
                EncounterId = encounterId,
                Encounter = isEncounterSet ? GetPatientEncounters(patientId, isIncludePatientSet).FirstOrDefault(): null,
                NursingDiagnosisText = string.Empty,
                NursingEvaluationText = string.Empty,
            });
            return entity.AsQueryable().BuildMock();
        }

        public static IQueryable<NursingCareSummary> GetNurseSummary_No_Records(long patientId, long encounterId, bool isEncounterSet = true, bool isIncludePatientSet = true)
        {
            var entity = new List<NursingCareSummary>();
            return entity.AsQueryable().BuildMock();
        }

        public static NursingCareSummary GetNurseSummary2(long patientId, long encounterId, bool isEncounterSet = true, bool isIncludePatientSet = true)
        {
            var entity = new NursingCareSummary();
            entity = new NursingCareSummary()
            {
                TenantId = 1,
                PatientId = patientId,
                Patient = GetPatients(patientId).FirstOrDefault(),
                EncounterId = encounterId,
                Encounter = isEncounterSet ? GetPatientEncounters(patientId, isIncludePatientSet).FirstOrDefault() : null,
                NursingDiagnosisText = string.Empty,
                NursingEvaluationText = string.Empty,
            };
            return entity;
        }
        public static List<PatientReviewDetailedHistory> GetAllList(long patientId, long encounterId)
        {
            var entity = new List<PatientReviewDetailedHistory>();
            entity.Add(new PatientReviewDetailedHistory()
            {
                TenantId = 1,
                PatientId = patientId,
                EncounterId = encounterId
            });
            return entity;
        }

        public static List<NursingRecord> GetAllNursingRecord(long encounterId)
        {
            var entity = new List<NursingRecord>();
            entity.Add(new NursingRecord()
            {
                Id = 1,
                EncounterId = encounterId,
                JsonNote = string.Empty,
                IsAutoSaved = false
            });
            return entity;
        }
        public static ReviewDetailedHistoryReturnDto GetReviewDetailedHistoryReturnDto(SaveToReviewDetailedHistoryRequestDto request)
        {
            var result = new ReviewDetailedHistoryReturnDto()
            {
                Id = 1,
                CreationTime = DateTime.Now,
                Title = request.Title,
                ShortDescription = request.ShortDescription,
                FirstVisitNote = request.FirstVisitNote,
                IsAutoSaved = request.IsAutoSaved
            };
            return result;
        }
        public static PatientEncounter GetPatientEncounter(long encounterId, int tenantId, long patientId)
        {
            var entity = new List<PatientEncounter>();
            entity.Add(new PatientEncounter()
            {
                Id = encounterId,
                TenantId = tenantId,
                PatientId = patientId,
                Patient = GetPatients(1).FirstOrDefault(),
                AppointmentId = 1,
                TimeIn = DateTime.Now,
                TimeOut = DateTime.Now,
                Status = EncounterStatusType.InProgress,
                ServiceCentre = ServiceCentreType.AccidentAndEmergency,
                UnitId = 1,
                Unit = GetOrganizationUnitExtended().FirstOrDefault(s => s.Id == 1),
                FacilityId = 1,
                Facility = DBFacility().FirstOrDefault(s => s.Id == 1),
                InvestigationResults = InvestigationResults(),
            });

            return entity.FirstOrDefault();
        }
        public static Plans GetPlansResult(long patientId)
        {
            return new Plans();
        }
    }
}