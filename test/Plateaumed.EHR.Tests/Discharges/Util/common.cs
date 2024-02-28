using Plateaumed.EHR.AllInputs;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Discharges.Dtos;
using Plateaumed.EHR.DoctorDischarge;
using Plateaumed.EHR.Medication.Dtos;
using Plateaumed.EHR.PatientAppointments.Query.BaseQueryHelper;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Staff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plateaumed.EHR.Organizations;

namespace Plateaumed.EHR.Tests.Discharges.Util
{
    public class Common
    {
        public static IQueryable<Discharge> GetDischargeAsQueryable(long patientId = 1, int tenantId = 1, int appointmentId = 1)
        {
            var list = new List<Discharge>()
            {
                new ()
                {
                    Id = 1,
                    TenantId = tenantId,
                    PatientId = patientId,
                    AppointmentId = appointmentId,
                    IsFinalized = true,
                    DischargeType = DischargeEntryType.NORMAL,
                    status = DischargeStatus.FINALIZED,
                    IsBroughtInDead = false
                },
                new ()
                {
                    Id = 2,
                    TenantId = tenantId,
                    PatientId = patientId,
                    AppointmentId = appointmentId,
                    IsFinalized = false,
                    DischargeType = DischargeEntryType.DAMA,
                    status = DischargeStatus.FINALIZED,
                    IsBroughtInDead = false
                },
                new ()
                {
                    Id = 3,
                    TenantId = tenantId,
                    PatientId = patientId,
                    AppointmentId = appointmentId,
                    IsFinalized = false,
                    DischargeType = DischargeEntryType.DAMA,
                    status = DischargeStatus.INITIATED,
                    IsBroughtInDead = false
                },
                new ()
                {
                    Id = 4,
                    TenantId = tenantId,
                    PatientId = patientId,
                    AppointmentId = appointmentId,
                    IsFinalized = true,
                    DischargeType = DischargeEntryType.DAMA,
                    status = DischargeStatus.FINALIZED,
                    IsBroughtInDead = false
                },
                new ()
                {
                    Id = 5,
                    TenantId = tenantId,
                    PatientId = patientId,
                    AppointmentId = appointmentId,
                    IsFinalized = false,
                    DischargeType = DischargeEntryType.DAMA,
                    status = DischargeStatus.INITIATED,
                    IsBroughtInDead = false
                },
                new ()
                {
                    Id = 10,
                    TenantId = tenantId,
                    PatientId = 2,
                    AppointmentId = appointmentId,
                    IsFinalized = false,
                    DischargeType =DischargeEntryType.DECEASED,
                    status = DischargeStatus.INITIATED,
                    IsBroughtInDead = true,
                    DateOfDeath = DateTime.Now,
                    TimeOfDeath = "12:00AM",
                    TimeCPRCommenced = "12:00AM",
                    TimeCPREnded = "12:00AM"
                },
            }.AsQueryable();

            return list;
        }
        public static CreateDischargeDto GetCreateDischargeRequest_No_DischargeType(long patientId = 1)
        {
            var requestModel = new CreateDischargeDto
            {
                Id = 0,
                PatientId = patientId,
                Status = DischargeStatus.INITIATED,
                IsBroughtInDead = false,
                Prescriptions = GetDischargePrescriptions(1),
                PlanItems = GetDischargePlanItems(1),
                AppointmentId = 1,
            };
            return requestModel;
        }
        public static CreateDischargeDto GetCreateDischargeRequest_No_Prescription(long patientId = 1)
        {
            var requestModel = new CreateDischargeDto
            {
                Id = 0,
                PatientId = patientId,
                DischargeType = DischargeEntryType.NORMAL,
                Status = DischargeStatus.INITIATED,
                IsBroughtInDead = false,
                Prescriptions = null,
                PlanItems = GetDischargePlanItems(1),
                AppointmentId = 1,
            };
            return requestModel;
        }

        public static CreateDischargeDto GetCreateDischargeRequest_No_PlanItems(long patientId = 1)
        {
            var requestModel = new CreateDischargeDto
            {
                Id = 0,
                PatientId = patientId,
                DischargeType = DischargeEntryType.NORMAL,
                Status = DischargeStatus.INITIATED,
                IsBroughtInDead = false,
                Prescriptions = GetDischargePrescriptions(1),
                PlanItems = null,
                AppointmentId = 1,
            };
            return requestModel;
        }

        public static CreateDischargeDto GetCreateDischargeRequest_No_AppointmentId(long patientId = 1)
        {
            var requestModel = new CreateDischargeDto
            {
                Id = 5,
                PatientId = patientId,
                DischargeType = DischargeEntryType.NORMAL,
                Status = DischargeStatus.INITIATED,
                IsBroughtInDead = false,
                Prescriptions = GetDischargePrescriptions(1),
                PlanItems = GetDischargePlanItems(1),
                AppointmentId = null,
            };
            return requestModel;
        }
        public static CreateNormalDischargeDto GetNormalDischargeRequest(long encounterId = 1)
        {
            var returnModel = new List<CreateDischargeDto>();

            return new CreateNormalDischargeDto
            {
                Id = 0,
                EncounterId = encounterId,
                DischargeType = DischargeEntryType.NORMAL,
                Prescriptions = GetDischargePrescriptions(1),
                PlanItems = GetDischargePlanItems(1),
                Note = "My note"
            };
        }
        public static List<CreateDischargeDto> GetCreateDischargeRequest(long encounterId = 1)
        {
            var returnModel = new List<CreateDischargeDto>();

            returnModel.Add(new CreateDischargeDto
            {
                Id = 0,
                PatientId = 1,
                EncounterId = encounterId,
                IsBroughtInDead = false,
                DischargeType = DischargeEntryType.NORMAL,
                Prescriptions = GetDischargePrescriptions(1),
                PlanItems = GetDischargePlanItems(1),
                Note = "My note"
            });
            returnModel.Add(new CreateDischargeDto
            {
                Id = 2,
                PatientId = 1,
                EncounterId = encounterId,
                DischargeType = DischargeEntryType.DAMA,
                Prescriptions = GetDischargePrescriptions(1),
                PlanItems = GetDischargePlanItems(1),
                Note = "My note"
            });
            returnModel.Add(new CreateDischargeDto
            {
                Id = 3,
                PatientId = 1,
                EncounterId = encounterId,
                DischargeType = DischargeEntryType.SUSPENSEADMISSION,
                Prescriptions = GetDischargePrescriptions(1),
                PlanItems = GetDischargePlanItems(1),
                Note = "My note"
            });
            

            return returnModel;
        }
        public static List<CreateDischargeDto> GetMarkAsDeceasedDischargeRequest(long encounterId = 1)
        {
            var returnModel = new List<CreateDischargeDto>();
            var causesOfDeath = new List<PatientCauseOfDeathDto>();
            causesOfDeath.Add(new PatientCauseOfDeathDto()
            {
                CausesOfDeath = "Accident"
            });
            returnModel.Add(new CreateDischargeDto
            {
                Id = 1,
                EncounterId = encounterId,
                DischargeType = DischargeEntryType.DECEASED,
                IsBroughtInDead = true,
                Note = "My note"
            });
            returnModel.Add(new CreateDischargeDto
            {
                Id = 2,
                EncounterId = encounterId,
                DischargeType = DischargeEntryType.DECEASED,
                IsBroughtInDead = false,
                TimeCPRCommenced = "12Noon",
                TimeCPREnded = "12Noon",
                TimeOfDeath = "12Noon",
                CausesOfDeath = causesOfDeath,
                Note = "My note"
            });


            return returnModel;
        }
        public static List<CreateDischargeMedicationDto> GetDischargeMedicationRequest(long patientId = 1)
        {
            var returnModel = new List<CreateDischargeMedicationDto>();

            returnModel.Add(new CreateDischargeMedicationDto
            {
                MedicationId = 1
            });
            returnModel.Add(new CreateDischargeMedicationDto
            {
                MedicationId = 2
            });
            returnModel.Add(new CreateDischargeMedicationDto
            {
                MedicationId = 3
            });
            returnModel.Add(new CreateDischargeMedicationDto
            {
                MedicationId = 4
            });
            returnModel.Add(new CreateDischargeMedicationDto
            {
                MedicationId = 3
            });
            returnModel.Add(new CreateDischargeMedicationDto
            {
                MedicationId = 4
            });
            returnModel.Add(new CreateDischargeMedicationDto
            {
                MedicationId = 0
            });

            return returnModel;
        }

        public static EditDischargeMedicationDto GetEditDischargeMedicationRequest(long patientId = 1, long dischargeId = 1)
        {
            var returnModel = new List<CreateDischargeMedicationDto>();

            returnModel.Add(new CreateDischargeMedicationDto
            {
                MedicationId = 1
            });
            returnModel.Add(new CreateDischargeMedicationDto
            {
                MedicationId = 2
            });
            returnModel.Add(new CreateDischargeMedicationDto
            {
                MedicationId = 3
            });
            returnModel.Add(new CreateDischargeMedicationDto
            {
                MedicationId = 4
            });
            returnModel.Add(new CreateDischargeMedicationDto
            {
                MedicationId = 3
            });
            returnModel.Add(new CreateDischargeMedicationDto
            {
                MedicationId = 4
            });
            returnModel.Add(new CreateDischargeMedicationDto
            {
                MedicationId = 0
            });

            var model = new EditDischargeMedicationDto()
            {
                DischargeId = dischargeId,
                patientId = patientId,
                Medication = returnModel
            };
            

            return model;
        }
        public static List<DischargeMedicationDto> GetDischargeMedication()
        {
            var dischargeMedications = new List<DischargeMedicationDto>();

            dischargeMedications.Add(new DischargeMedicationDto()
            {
                MedicationId = 1,
                PatientId = 1,
                DischargeId = 1,
                ProductId = 1,
                ProductName = "Paracetamol",
                ProductSource = "Hospital",
                Note = "Text Note"
            });
            return dischargeMedications;
        }

        public static IQueryable<PatientMedicationForReturnDto> GetDischargeMedications(long dischargeId, int? tenantId)
        {
            var dischargeMedications = new List<PatientMedicationForReturnDto>();

            dischargeMedications.Add(new PatientMedicationForReturnDto()
            {
                Id = 1,
                PatientId = 1,
                ProductId = 1,
                ProductName = "Paracetamol",
                ProductSource = "Hospital",
                Note = "Text Note"
            });
            return dischargeMedications.AsQueryable();
        }
        public static IQueryable<DischargePlanItemDto> GetDischargePlanItem()
        {
            var dischargePlanItem = new List<DischargePlanItemDto>();

            dischargePlanItem.Add(new DischargePlanItemDto()
            {
                PlanItemId = 1,
                PatientId = 1,
                DischargeId = 1,
                Description = "Description",
                CreationTime = DateTime.Now,
                DeletionTime = DateTime.Now
            });
            return dischargePlanItem.AsQueryable();
        }
        public static IQueryable<DischargePlanItemDto> GetDischargePlanItem(long dischargeId, int? tenantId)
        {
            var dischargePlanItem = new List<DischargePlanItemDto>();

            dischargePlanItem.Add(new DischargePlanItemDto()
            {
                PlanItemId = 1,
                PatientId = 1,
                DischargeId = dischargeId,
                Description = "Description",
                CreationTime = DateTime.Now,
                DeletionTime = DateTime.Now
            });
            return dischargePlanItem.AsQueryable();
        }

        public static IQueryable<DischargePlanItemDto> GetDischargePlanItemAsIQuerable()
        {
            var dischargePlanItem = new List<DischargePlanItemDto>();

            dischargePlanItem.Add(new DischargePlanItemDto()
            {
                PlanItemId = 1,
                PatientId = 1,
                DischargeId = 1,
                Description = "Description",
                CreationTime = DateTime.Now,
                DeletionTime = DateTime.Now
            });
            return dischargePlanItem.AsQueryable();
        }
        

        public static List<CreateDischargePlanItemDto> GetDischargedPlanItemsRequest(long patientId = 1)
        {
            var returnModel = new List<CreateDischargePlanItemDto>();

            returnModel.Add(new CreateDischargePlanItemDto
            {
                PlanItemId = 1
            });
            returnModel.Add(new CreateDischargePlanItemDto
            {
                PlanItemId = 2
            });
            returnModel.Add(new CreateDischargePlanItemDto
            {
                PlanItemId = 3
            });
            returnModel.Add(new CreateDischargePlanItemDto
            {
                PlanItemId = 4
            });
            returnModel.Add(new CreateDischargePlanItemDto
            {
                PlanItemId = 3
            });
            returnModel.Add(new CreateDischargePlanItemDto
            {
                PlanItemId = 4
            });
            returnModel.Add(new CreateDischargePlanItemDto
            {
                PlanItemId = 0
            });

            return returnModel;
        }
        public static EditDischargePlanItemDto GetEditDischargePlanItemRequest(long patientId = 1, long dischargeId = 1)
        {
            var returnModel = new List<CreateDischargePlanItemDto>();

            returnModel.Add(new CreateDischargePlanItemDto
            {
                PlanItemId = 1
            });
            returnModel.Add(new CreateDischargePlanItemDto
            {
                PlanItemId = 2
            });
            returnModel.Add(new CreateDischargePlanItemDto
            {
                PlanItemId = 3
            });
            returnModel.Add(new CreateDischargePlanItemDto
            {
                PlanItemId = 4
            });
            returnModel.Add(new CreateDischargePlanItemDto
            {
                PlanItemId = 3
            });
            returnModel.Add(new CreateDischargePlanItemDto
            {
                PlanItemId = 4
            });
            returnModel.Add(new CreateDischargePlanItemDto
            {
                PlanItemId = 0
            });

            var model = new EditDischargePlanItemDto()
            {
                DischargeId = dischargeId,
                patientId = patientId,
                PlanItems = returnModel
            };


            return model;
        }

        public static List<CreateDischargeMedicationDto> GetDischargePrescriptions(long patientId = 1)
        {
            var returnModel = new List<CreateDischargeMedicationDto>();

            returnModel.Add(new CreateDischargeMedicationDto()
            {
                MedicationId = 1,
            });
            returnModel.Add(new CreateDischargeMedicationDto()
            {
                MedicationId = 2,
            });
            returnModel.Add(new CreateDischargeMedicationDto()
            {
                MedicationId = 3,
            });
            returnModel.Add(new CreateDischargeMedicationDto()
            {
                MedicationId = 4,
            });

            return returnModel;
        }
        public static List<CreateDischargePlanItemDto> GetDischargePlanItems(long patientId = 1)
        {
            var returnModel = new List<CreateDischargePlanItemDto>();

            returnModel.Add(new CreateDischargePlanItemDto()
            {
                PlanItemId = 1,
            });
            returnModel.Add(new CreateDischargePlanItemDto()
            {
                PlanItemId = 2,
            });
            returnModel.Add(new CreateDischargePlanItemDto()
            {
                PlanItemId = 3,
            });
            returnModel.Add(new CreateDischargePlanItemDto()
            {
                PlanItemId = 4,
            });

            return returnModel;
        }

        public static AllInputs.Discharge GetNormalDischargeInstance(CreateDischargeDto command, int tenantId)
        {
            return new AllInputs.Discharge
            {
                Id = 1,
                TenantId = tenantId,
                IsFinalized = false,
                DischargeType = DischargeEntryType.NORMAL,
                status = DischargeStatus.INITIATED,
                IsBroughtInDead = false,
                CreatorUser = new User()
                {
                    Id = 1,
                    Name = "Test Person",
                    Title = TitleType.Mr,
                    Gender = GenderType.Male,
                    DateOfBirth =DateTime.Now.AddYears(-25),
                    PostCode = ""

                }

            };
        }

        public static AllInputs.DischargeNote GetDischargeNote(long dischargeId, int tenantId)
        {
            return new AllInputs.DischargeNote
            {
                Id = 1,
                TenantId = tenantId,
                DischargeId = dischargeId,
                Note = ""

            };
        }
        public static PatientCauseOfDeath GetPatientCauseOfDeath(long dischargeId, int tenantId)
        {
            return new PatientCauseOfDeath()
            {
                Id = 1,
                TenantId = tenantId,
                DischargeId = dischargeId,
                CausesOfDeath = ""
            };
        }
        public static AllInputs.Discharge GetDAMADischargeInstance(CreateDischargeDto command, int tenantId)
        {
            return new AllInputs.Discharge
            {
                Id = 2,
                TenantId = tenantId,
                PatientId = command.PatientId,
                AppointmentId = command.AppointmentId,
                IsFinalized = false,
                DischargeType = DischargeEntryType.DAMA,
                status = DischargeStatus.INITIATED,
                IsBroughtInDead = false,
                CreatorUser = new User()
                {
                    Id = 1,
                    Name = "Test Person",
                    Title = TitleType.Mr,
                    Gender = GenderType.Male,
                    DateOfBirth = DateTime.Now.AddYears(-25),
                    PostCode = ""

                }

            };
        }
        public static AllInputs.Discharge GetDeceasedDischargeInstance(CreateMarkAsDeceasedDischargeDto command, int tenantId)
        {
            return new AllInputs.Discharge
            {
                Id = 3,
                TenantId = tenantId,
                IsFinalized = false,
                DischargeType = DischargeEntryType.NORMAL,
                status = DischargeStatus.INITIATED,
                IsBroughtInDead = command.IsBroughtInDead,
                DateOfDeath = command.DateOfDeath,
                TimeOfDeath = command.TimeOfDeath,
                TimeCPRCommenced = command.TimeCPRCommenced,
                TimeCPREnded = command.TimeCPREnded,
                CreatorUser = new User()
                {
                    Id = 1,
                    Name = "Test Person",
                    Title = TitleType.Mr,
                    Gender = GenderType.Male,
                    DateOfBirth = DateTime.Now.AddYears(-25),
                    PostCode = ""

                }

            };
        }

        public static List<PatientMedicationForReturnDto> GetMedications(long patientId = 1)
        {
            var medications = new List<PatientMedicationForReturnDto>();
            medications.Add(new PatientMedicationForReturnDto
            {
                Id = 1,
                PatientId = patientId,
                ProductId = 1,
                ProductName = "Paracetamol",
                ProductSource = "In-house",
                DoseUnit = "250MG",
                Frequency = "Daily",
                Duration = "One Week",
                Direction = "2X Daily",
                Note = "Text Note",
                CreationTime = DateTime.Now
            });
            medications.Add(new PatientMedicationForReturnDto
            {
                Id = 2,
                PatientId = patientId,
                ProductId = 1,
                ProductName = "Vitamin C",
                ProductSource = "In-house",
                DoseUnit = "500MG",
                Frequency = "Daily",
                Duration = "One Week",
                Direction = "3X Daily",
                Note = "Text Note",
                CreationTime = DateTime.Now
            });
            return medications;
        }

        public static IQueryable<AllInputs.PlanItems> GetPatientPlanItems(int patientId, int? tenantId, bool isDeleted = false)
        {
            var planItems = new List<AllInputs.PlanItems>();
            planItems.Add(new AllInputs.PlanItems
            {
                Id = 1,
                TenantId = (int)tenantId,
                Stamp = 1,
                PatientId = patientId,
                PlanItemsSnowmedIds = new List<string>(),
                Description = "Text Note",
                CreationTime = DateTime.Now,
                IsDeleted = false
            });
            planItems.Add(new AllInputs.PlanItems
            {
                Id = 2,
                TenantId = (int)tenantId,
                Stamp = 1,
                PatientId = patientId,
                PlanItemsSnowmedIds = new List<string>(),
                Description = "Text Note",
                CreationTime = DateTime.Now,
                IsDeleted = false
            });
            planItems.Add(new AllInputs.PlanItems
            {
                Id = 3,
                TenantId = (int)tenantId,
                Stamp = 1,
                PatientId = patientId,
                PlanItemsSnowmedIds = new List<string>(),
                Description = "Text Note",
                CreationTime = DateTime.Now,
                IsDeleted = true
            });

            var query = (from a in planItems
                         where a.PatientId == patientId && a.IsDeleted == isDeleted
                         select a).AsQueryable();

            return query;
        }

        public static DischargePlanItem GetDischargePlanItemInstance(List<CreateDischargePlanItemDto> command, int tenantId, long patientId)
        {
            var result = new DischargePlanItem
            {
                Id = 1,
                TenantId = tenantId,
                DischargeId = 1,
                PlanItemId = command[0].PlanItemId,
                Discharge = new Discharge()
                {
                    Id = 1,
                    TenantId = tenantId,
                    PatientId = patientId,
                    AppointmentId = 1,
                    IsFinalized = false,
                    DischargeType = DischargeEntryType.NORMAL,
                    status = DischargeStatus.INITIATED,
                    IsBroughtInDead = false,
                    CreatorUser = new User()
                    {
                        Id = 1,
                        Name = "Test Person",
                        Title = TitleType.Mr,
                        Gender = GenderType.Male,
                        DateOfBirth = DateTime.Now.AddYears(-25),
                        PostCode = "1000027"

                    }
                }

            };
            return result;
        }
        public static DischargePlanItem GetEditDischargePlanItemInstance(EditDischargePlanItemDto command, int tenantId, long patientId)
        {
            var result = new DischargePlanItem
            {
                Id = 1,
                TenantId = tenantId,
                DischargeId = 1,
                PlanItemId = command.PlanItems[0].PlanItemId,
                Discharge = new Discharge()
                {
                    Id = 1,
                    TenantId = tenantId,
                    PatientId = patientId,
                    AppointmentId = 1,
                    IsFinalized = false,
                    DischargeType = DischargeEntryType.NORMAL,
                    status = DischargeStatus.INITIATED,
                    IsBroughtInDead = false,
                    CreatorUser = new User()
                    {
                        Id = 1,
                        Name = "Test Person",
                        Title = TitleType.Mr,
                        Gender = GenderType.Male,
                        DateOfBirth = DateTime.Now.AddYears(-25),
                        PostCode = "1000027"

                    }
                }

            };
            return result;
        }

        public static DischargeMedication GetDischargeMedicationInstance(EditDischargeMedicationDto command, int tenantId, long patientId)
        {
            var result = new DischargeMedication
            {
                Id = 1,
                TenantId = tenantId,
                DischargeId = 1,
                MedicationId = command.Medication[0].MedicationId,
                Discharge = new AllInputs.Discharge()
                {
                    Id = 1,
                    TenantId = tenantId,
                    PatientId = patientId,
                    AppointmentId = 1,
                    IsFinalized = false,
                    DischargeType = DischargeEntryType.NORMAL,
                    status = DischargeStatus.INITIATED,
                    IsBroughtInDead = false,
                    CreatorUser = new User()
                    {
                        Id = 1,
                        Name = "Test Person",
                        Title = TitleType.Mr,
                        Gender = GenderType.Male,
                        DateOfBirth = DateTime.Now.AddYears(-25),
                        PostCode = "1000027"

                    }
                }

            };
            return result;
        }

        public static List<DischargeMedication> GetListDischargeMedicationInstance(List<CreateDischargeMedicationDto> command, int tenantId, long dischargeId, long patientId)
        {
            var result = new List<AllInputs.DischargeMedication>();
            foreach (var item in command)
            {
                int i = 0;
                result.Add(new AllInputs.DischargeMedication
                {
                    Id = i+1,
                    TenantId = tenantId,
                    DischargeId = dischargeId,
                    MedicationId = item.MedicationId,
                    Discharge = new AllInputs.Discharge()
                    {
                        Id = i + 1,
                        TenantId = tenantId,
                        PatientId = patientId,
                        AppointmentId = 1,
                        IsFinalized = false,
                        DischargeType = DischargeEntryType.NORMAL,
                        status = DischargeStatus.INITIATED,
                        IsBroughtInDead = false,
                        CreatorUser = new User()
                        {
                            Id = 1,
                            Name = "Test Person",
                            Title = TitleType.Mr,
                            Gender = GenderType.Male,
                            DateOfBirth = DateTime.Now.AddYears(-25),
                            PostCode = "1000027"

                        }
                    }

                });
            }
            
            return result;
        }

        public static IQueryable<DischargeDto> GetPatientDischargeInformation(long patientId)
        {
            var result = new List<DischargeDto>();
            result.Add(new DischargeDto
            {
                Id = 1,
                PatientId = patientId,
                IsFinalized = false,
                DischargeType = DischargeEntryType.NORMAL,
                status = DischargeStatus.INITIATED,
                IsBroughtInDead = false,
            });
            result.Add(new DischargeDto
            {
                Id = 2,
                PatientId = patientId,
                IsFinalized = true,
                DischargeType = DischargeEntryType.DAMA,
                status = DischargeStatus.INITIATED,
                IsBroughtInDead = false,
            });
            result.Add(new DischargeDto
            {
                Id = 5,
                PatientId = 2,
                IsFinalized = false,
                DischargeType = DischargeEntryType.DECEASED,
                status = DischargeStatus.INITIATED,
                IsBroughtInDead = true,
                DateOfDeath = DateTime.Now,
                TimeOfDeath = "12:00AM",
                TimeCPRCommenced = "12:30AM",
                TimeCPREnded = "01:00AM"
            });

            return result.Where(s=>s.PatientId== patientId).AsQueryable();
        }


        public async Task<IQueryable<DischargeDto>> GetPatientDischargeInformationAsync(long patientId)
        {
            var result = new List<DischargeDto>();
            result.Add(new DischargeDto
            {
                Id = 1,
                PatientId = patientId,
                IsFinalized = false,
                DischargeType = DischargeEntryType.NORMAL,
                status = DischargeStatus.INITIATED,
                IsBroughtInDead = false,
            });
            result.Add(new DischargeDto
            {
                Id = 2,
                PatientId = patientId,
                IsFinalized = true,
                DischargeType = DischargeEntryType.DAMA,
                status = DischargeStatus.INITIATED,
                IsBroughtInDead = false,
            });
            result.Add(new DischargeDto
            {
                Id = 5,
                PatientId = 2,
                IsFinalized = false,
                DischargeType = DischargeEntryType.DECEASED,
                status = DischargeStatus.INITIATED,
                IsBroughtInDead = true,
                DateOfDeath = DateTime.Now,
                TimeOfDeath = "12:00AM",
                TimeCPRCommenced = "12:30AM",
                TimeCPREnded = "01:00AM",
            });

            return result.Where(s => s.PatientId == patientId).AsQueryable();
        }
        public static IQueryable<AppointmentBaseQuery> GetAppointmentsBaseQuery()
        {
            var appointment = new AppointmentBaseQuery()
            {
                Appointment = new PatientAppointment()
                {
                    Id = 1,
                    TenantId = 1,
                    PatientId = 1,
                    Title = "Next Appointment",
                    Duration = 1,
                    StartTime = DateTime.Now,
                    IsRepeat = false,
                    Notes = "",
                    RepeatType = AppointmentRepeatType.Monthly,
                    Status = AppointmentStatusType.Not_Arrived,
                    Type = AppointmentType.Consultation
                },
                Patient = new Patient()
                {
                    Id = 1,
                    UuId = Guid.NewGuid(),
                    GenderType = GenderType.Male,
                    FirstName = "Test",
                    LastName = "John",
                    PhoneNumber = "0909292922"

                },
                PatientCodeMapping = new PatientCodeMapping()
                {
                    Id = 1,
                },
                AttendingClinic = new OrganizationUnitExtended()
                {
                    Id = 1,
                },
                ReferringClinic = new OrganizationUnitExtended()
                {
                    Id = 2,
                },
                AttendingPhysician = new StaffMember()
                {
                    Id = 1,
                },
                ReferralDocument = new PatientReferralDocument()
                {
                    Id = 1,
                },
                StaffUser = new User() 
                { 
                    Id = 1,
                },
                PatientScanDocument = new PatientScanDocument()
                {
                    Id = 1,
                }
            };

            var appointments = new List<AppointmentBaseQuery>();
            appointments.Add(appointment);

            return appointments.AsQueryable();
        }
        public static List<PatientMedicationForReturnDto> GetPatientMedication(int patientId, int? tenantId)
        {
            var medications = GetMedications(patientId);
            return medications;
        }
        public static IQueryable<AllInputs.PlanItems> GetPatientPlanItemsBaseQuery(int patientId, long? procedureId = null, bool isDeleted = false)
        {
            return GetPatientPlanItems(patientId,1, isDeleted);
        }
        public static IQueryable<DischargeNote> GetDischargeNote(long dischargeId)
        {
            var list = new List<DischargeNote>();
            list.Add(new DischargeNote()
            {
                Id = 1,
                DischargeId = dischargeId,
                Note = "This is my note"
            });
            return list.AsQueryable();
        }

        public static IQueryable<PatientCauseOfDeath> GetCausesOfDealth(long dischargeId)
        {
            var list = new List<PatientCauseOfDeath>();
            list.Add(new PatientCauseOfDeath()
            {
                Id = 1,
                DischargeId = dischargeId,
                CausesOfDeath = "This is my note"
            });
            return list.AsQueryable();
        }
    }
}