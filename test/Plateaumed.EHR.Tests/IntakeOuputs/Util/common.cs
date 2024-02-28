using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Plateaumed.EHR.AllInputs;
using Plateaumed.EHR.IntakeOutputs.Dtos;
using Plateaumed.EHR.IntakeOutputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Tests.IntakeOuputs.Util
{
    public class Common
    {
        public static List<CreateIntakeOutputDto> GetCreateIntakeOutputRequest(long patientId = 1)
        {
            var returnModel = new List<CreateIntakeOutputDto>();

            returnModel.Add(new CreateIntakeOutputDto
            {
                Id = 0,
                PatientId = patientId,
                Type  = IntakeOutputs.ChartingType.INTAKE,
                SuggestedText = "Antibiotics",
                VolumnInMls = 930
            });
            returnModel.Add(new CreateIntakeOutputDto
            {
                Id = 0,
                PatientId = patientId,
                Type = IntakeOutputs.ChartingType.INTAKE,
                SuggestedText = "Insulin",
                VolumnInMls = 930
            });
            returnModel.Add(new CreateIntakeOutputDto
            {
                Id = 0,
                PatientId = patientId,
                Type = IntakeOutputs.ChartingType.INTAKE,
                SuggestedText = "Capsules",
                VolumnInMls = 930
            });
            returnModel.Add(new CreateIntakeOutputDto
            {
                Id = 1,
                PatientId = patientId,
                Type = IntakeOutputs.ChartingType.INTAKE,
                SuggestedText = "Anitmalaria",
                VolumnInMls = 930
            });
            returnModel.Add(new CreateIntakeOutputDto
            {
                Id = 2,
                PatientId = patientId,
                Type = IntakeOutputs.ChartingType.INTAKE,
                SuggestedText = "Antibiotic Tablet",
                VolumnInMls = 930
            });
            returnModel.Add(new CreateIntakeOutputDto
            {
                Id = 0,
                PatientId = patientId,
                Type = IntakeOutputs.ChartingType.OUTPUT,
                SuggestedText = "Urine",
                VolumnInMls = 930
            });
            returnModel.Add(new CreateIntakeOutputDto
            {
                Id = 7,
                PatientId = patientId,
                Type = IntakeOutputs.ChartingType.OUTPUT,
                SuggestedText = "Wet Bed",
                VolumnInMls = 930
            });

            return returnModel;
        }
        public static List<IntakeOutputCharting> GetIntakeOutputDto(long patientId = 1)
        {
            var returnModel = new List<IntakeOutputCharting>();

            
            returnModel.Add(new IntakeOutputCharting
            {
                Id = 1,
                TenantId = 1,
                PatientId = patientId,
                Type = IntakeOutputs.ChartingType.INTAKE,
                SuggestedText = "Anitmalaria",
                VolumnInMls = 930
            });
            returnModel.Add(new IntakeOutputCharting
            {
                Id = 2,
                TenantId = 1,
                PatientId = patientId,
                Type = IntakeOutputs.ChartingType.INTAKE,
                SuggestedText = "Antibiotic Tablet",
                VolumnInMls = 930
            });

            return returnModel;
        }
        private static List<SuggestedTextDto> GetOtherInfusions(List<SuggestedTextDto> patientInfusion)
        {
            patientInfusion.Add(new SuggestedTextDto
            {
                Id = 0,
                SuggestedText = IntakeOutputConsts.WATER,
                VolumnInMls = 0,
                Frequency = string.Empty
            });
            patientInfusion.Add(new SuggestedTextDto
            {
                Id = 0,
                SuggestedText = IntakeOutputConsts.PAP,
                VolumnInMls = 0,
                Frequency = string.Empty
            });
            patientInfusion.Add(new SuggestedTextDto
            {
                Id = 0,
                SuggestedText = IntakeOutputConsts.CARBONATED_DRINKS,
                VolumnInMls = 0,
                Frequency = string.Empty
            });
            patientInfusion.Add(new SuggestedTextDto
            {
                Id = 0,
                SuggestedText = IntakeOutputConsts.ORAL_FLUIDS,
                VolumnInMls = 0,
                Frequency = string.Empty
            });

            return patientInfusion;
        }
        private static List<SuggestedTextDto> GetOtherMedications(List<SuggestedTextDto> patientMedications)
        {
            patientMedications.Add(new SuggestedTextDto
            {
                Id = 0,
                SuggestedText = IntakeOutputConsts.LINE_RESET,
                VolumnInMls = 0,
                Frequency = string.Empty
            });
            patientMedications.Add(new SuggestedTextDto
            {
                Id = 0,
                SuggestedText = IntakeOutputConsts.FLUIDS_INTERRUPTED,
                VolumnInMls = 0,
                Frequency = string.Empty
            });
            patientMedications.Add(new SuggestedTextDto
            {
                Id = 0,
                SuggestedText = IntakeOutputConsts.FLUIDS_NOT_AVAILABLE,
                VolumnInMls = 0,
                Frequency = string.Empty
            });
            patientMedications.Add(new SuggestedTextDto
            {
                Id = 0,
                SuggestedText = IntakeOutputConsts.TREATMENT_SENT,
                VolumnInMls = 0,
                Frequency = string.Empty
            });
            patientMedications.Add(new SuggestedTextDto
            {
                Id = 0,
                SuggestedText = IntakeOutputConsts.FLUIDS_NOT_SUPPLIED,
                VolumnInMls = 0,
                Frequency = string.Empty
            });

            return patientMedications;
        }

        private static List<SuggestedTextDto> GetIntakeSuggestionTest(long patientId, int? tenantId)
        {
            var type = ChartingType.INTAKE;
            var intakeList = new List<SuggestedTextDto>();
            intakeList.AddRange(GetOtherInfusions(intakeList));
            intakeList.AddRange(GetOtherMedications(intakeList));
            intakeList = intakeList.OrderBy(o => o.Frequency).ToList();

            //Get assigned Intakes
            var intakeDB =
                patientId == 0 || tenantId == 0 ? new List<SuggestedTextDto>() :
                GetIntakeOutputDto(1).Where(s => s.PatientId == patientId && s.TenantId == tenantId
                        && s.Type == type && s.IsDeleted == false).Select(s => new SuggestedTextDto
                        {
                            Id = 0,
                            SuggestedText = s.SuggestedText,
                            VolumnInMls = s.VolumnInMls
                        }).ToList();

            var result = (from _in in intakeList
                          join _db in intakeDB on _in.SuggestedText equals _db.SuggestedText into _output
                          from rec in _output.DefaultIfEmpty()
                          select new SuggestedTextDto
                          {
                              Id = rec?.Id ?? 0,
                              SuggestedText = _in.SuggestedText,
                              VolumnInMls = rec?.VolumnInMls ?? 0,
                              Frequency = _in.Frequency
                          }).ToList();

            return result;
        }

        public static PatientIntakeOutputDto GetPatientIntakes(long patientId, int? tenantId)
        {
            var result = new PatientIntakeOutputDto()
            {
                PatientId = patientId,
                Type = ChartingType.INTAKE,
                ChartingTypeText = "Intake",
                SuggestedText = GetIntakeSuggestionTest(patientId, tenantId)
            };

            return result;
        }
        public static PatientIntakeOutputDto GetPatientOutput(long patientId, int? tenantId)
        {
            var outPutList = new List<SuggestedTextDto>();
            outPutList.Add(new SuggestedTextDto
            {
                Id = 0,
                SuggestedText = IntakeOutputConsts.URINE,
                VolumnInMls = 0,
                Frequency = string.Empty
            });
            outPutList.Add(new SuggestedTextDto
            {
                Id = 0,
                SuggestedText = IntakeOutputConsts.WET_BED,
                VolumnInMls = 0,
                Frequency = string.Empty
            });
            outPutList.Add(new SuggestedTextDto
            {
                Id = 0,
                SuggestedText = IntakeOutputConsts.WET_DIAPER,
                VolumnInMls = 0,
                Frequency = string.Empty
            });
            outPutList.Add(new SuggestedTextDto
            {
                Id = 0,
                SuggestedText = IntakeOutputConsts.STOMA_BAG,
                VolumnInMls = 0,
                Frequency = string.Empty
            });
            //Get assigned Intakes


            var result = new PatientIntakeOutputDto()
            {
                PatientId = patientId,
                Type = ChartingType.OUTPUT,
                ChartingTypeText = "Output",
                SuggestedText = outPutList
            };

            return result;
        }


        public static List<PatientIntakeOutputDto> GetSavedHistory(long patientId)
        {
            var outPutList = new List<SuggestedTextDto>();
            outPutList.Add(new SuggestedTextDto
            {
                Id = 1,
                SuggestedText = IntakeOutputConsts.URINE,
                VolumnInMls = 0,
                CreatedAt = DateTime.Now
            });
            outPutList.Add(new SuggestedTextDto
            {
                Id = 2,
                SuggestedText = IntakeOutputConsts.WET_BED,
                VolumnInMls = 0,
                CreatedAt = DateTime.Now
            });
            outPutList.Add(new SuggestedTextDto
            {
                Id = 3,
                SuggestedText = IntakeOutputConsts.WET_DIAPER,
                VolumnInMls = 0,
                CreatedAt = DateTime.Now
            });
            outPutList.Add(new SuggestedTextDto
            {
                Id = 4,
                SuggestedText = IntakeOutputConsts.STOMA_BAG,
                VolumnInMls = 0,
                CreatedAt = DateTime.Now
            });
            //Get assigned Intakes

            var result = new List<PatientIntakeOutputDto>();

            var intakes = GetIntakeOutputDto(1).Select(s => new SuggestedTextDto()
            {
                Id = 4,
                SuggestedText = s.SuggestedText,
                VolumnInMls = s.VolumnInMls,
                CreatedAt = DateTime.Now
            }).ToList();

            result.Add(new PatientIntakeOutputDto()
            {
                PatientId = patientId,
                Type = ChartingType.INTAKE,
                ChartingTypeText = "Output",
                SuggestedText = intakes
            });

            result.Add(new PatientIntakeOutputDto()
            {
                PatientId = patientId,
                Type = ChartingType.OUTPUT,
                ChartingTypeText = "Output",
                SuggestedText = outPutList
            });
            return result;
        }


        public static IntakeOutputReturnDto GetIntakeOutputById(long id)
        {

            //Get assigned Intakes
            var result = GetCreateIntakeOutputRequest(id).Where(s => s.Id == id).Select(s => new IntakeOutputReturnDto
            {
                Id = id,
                PatientId = s.PatientId,
                Type = s.Type,
                Pannel = s.Type == ChartingType.INTAKE ? "Intake" : "Output",
                SuggestedText = s.SuggestedText,
                VolumnInMls = s.VolumnInMls
            }).FirstOrDefault();

            return result;
        }
        public static List<IntakeOutputReturnDto> GetIntakeOutputByText(long patientId, string text)
        {

            //Get assigned Intakes
            var result = GetCreateIntakeOutputRequest(1).Where(s => s.PatientId == patientId && s.SuggestedText== text).Select(s => new IntakeOutputReturnDto
            {
                Id = (long)s.Id,
                PatientId = s.PatientId,
                Type = s.Type,
                Pannel = s.Type == ChartingType.INTAKE ? "Intake" : "Output",
                SuggestedText = s.SuggestedText,
                VolumnInMls = s.VolumnInMls
            }).ToList();

            return string.IsNullOrEmpty(text) ? null : result;
        }
    }
}