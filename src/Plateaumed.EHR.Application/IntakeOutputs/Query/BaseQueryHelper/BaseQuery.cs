using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.Runtime.Session;
using MailKit.Search;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Plateaumed.EHR.AllInputs;
using Plateaumed.EHR.IntakeOutputs;
using Plateaumed.EHR.IntakeOutputs.Abstractions;
using Plateaumed.EHR.IntakeOutputs.Dtos;
using Plateaumed.EHR.Medication.Dtos;

namespace Plateaumed.EHR.IntakeOutputs.Query.BaseQueryHelper;

public class BaseQuery : IBaseQuery
{
    private readonly IRepository<IntakeOutputCharting, long> _IntakeOutputRepository;
    private readonly IRepository<Product, long> _productRepository;
    private readonly Medication.Abstractions.IBaseQuery _medicationQueryHandler;
    private readonly IAbpSession _abpSession;


    public BaseQuery(IRepository<IntakeOutputCharting, long> IntakeOutputRepository,
        Medication.Abstractions.IBaseQuery medicationQueryHandler, IRepository<Product, long> productRepository, IAbpSession abpSession)
    {
        _IntakeOutputRepository = IntakeOutputRepository;
        _medicationQueryHandler = medicationQueryHandler;
        _productRepository = productRepository;
        _abpSession = abpSession;
    }
    private List<SuggestedTextDto> GetOtherInfusions(List<SuggestedTextDto> patientInfusion)
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
    private List<SuggestedTextDto> GetOtherMedications(List<SuggestedTextDto> patientMedications)
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
    private async Task<List<SuggestedTextDto>> GetIntakeSuggestionText(long patientId)
    {
        var tenantId = _abpSession.TenantId.GetValueOrDefault();
        var intakeList = new List<SuggestedTextDto>();
        var allMedications = _medicationQueryHandler.GetPatientMedications((int)patientId, tenantId);
        var allProducts = _productRepository.GetAll();

        var infusions = (from med in allMedications
                         join product in allProducts on med.ProductId equals product.Id

                         where product.DoseFormName.ToLower().Contains("infusion") && med.PatientId == patientId && med.TenantId == tenantId && med.IsDeleted == false
                         select new AllInputs.Medication
                         {
                             Id = product.Id,
                             ProductName = product.ProductName,
                             Frequency = med.Frequency,
                             DoseUnit = med.DoseUnit,
                             ProductSource = "Product"
                         });

        //Get IV Fluids using patient infusion
        var patientInfusion = await infusions.Select(s => new SuggestedTextDto
        {
            Id = 0,
            SuggestedText = s.ProductName,
            VolumnInMls = 0,
            Frequency = s.Frequency
        }).ToListAsync();

        intakeList.AddRange(GetOtherInfusions(patientInfusion));


        var medications = (from med in allMedications
                           join product in allProducts on med.ProductId equals product.Id

                         where product.DoseFormName.ToLower().Contains("tablet") && med.PatientId == patientId && med.TenantId == tenantId && med.IsDeleted == false
                         select new AllInputs.Medication
                         {
                             Id = product.Id,
                             ProductName = product.ProductName,
                             Frequency = med.Frequency,
                             DoseUnit = med.DoseUnit,
                             ProductSource = "Product"
                         });

        //Get IV Medications using patient prescription in tablet
        var patientMedication = medications.Select(s => new SuggestedTextDto
        {
            Id = 0,
            SuggestedText = s.ProductName,
            VolumnInMls = 0,
            Frequency = s.Frequency
        }).ToList();
        intakeList.AddRange(GetOtherMedications(patientMedication));
        intakeList = intakeList.OrderBy(o=>o.Frequency).ToList();  //order by frequency

        return intakeList;
    }
    public async Task<PatientIntakeOutputDto> GetIntakesSuggestions(long patientId)
    {
        var result = new PatientIntakeOutputDto()
        {
            PatientId = patientId,
            Type = ChartingType.INTAKE,
            ChartingTypeText = "Intake",
            SuggestedText = await GetIntakeSuggestionText(patientId)
        };

        return result;
    }
    public async Task<List<PatientIntakeOutputDto>> GetPatientIntakeOutputHistory(long patientId, long? procedureId = null)
    {
        var tenantId = _abpSession.TenantId.GetValueOrDefault();
        //Get all patient saved Intakes
        var intakeDB =
            patientId == 0 || tenantId == 0 ? new List<SuggestedTextDto>() :
            await _IntakeOutputRepository.GetAll()
                .Where(s => s.PatientId == patientId && s.TenantId == tenantId
                    && s.Type == ChartingType.INTAKE && s.IsDeleted == false).Select(s => new SuggestedTextDto
                    {
                        Id = s.Id,
                        SuggestedText = s.SuggestedText,
                        VolumnInMls = s.VolumnInMls,
                        CreatedAt = s.CreationTime,
                        ProcedureId = s.ProcedureId,
                        ProcedureEntryType = s.ProcedureEntryType
                    })
                .WhereIf(procedureId.HasValue, v => v.ProcedureId == procedureId.Value)
                .OrderByDescending(x=>x.CreatedAt)
                .ToListAsync();

        //Get patient output
        var outputDB =
            patientId == 0 || tenantId == 0 ? new List<SuggestedTextDto>() :
            await _IntakeOutputRepository.GetAll()
                .Where(s => s.PatientId == patientId && s.TenantId == tenantId
                    && s.Type == ChartingType.OUTPUT && s.IsDeleted == false).Select(s => new SuggestedTextDto
                    {
                        Id = s.Id,
                        SuggestedText = s.SuggestedText,
                        VolumnInMls = s.VolumnInMls,
                        CreatedAt = s.CreationTime,
                        ProcedureId = s.ProcedureId,
                        ProcedureEntryType = s.ProcedureEntryType
                    })
                .WhereIf(procedureId.HasValue, v => v.ProcedureId == procedureId.Value)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();

        var result = new List<PatientIntakeOutputDto>();
        result.Add(new PatientIntakeOutputDto()
        {
            PatientId = patientId,
            Type = ChartingType.INTAKE,
            ChartingTypeText = "Intake",
            SuggestedText = intakeDB
        });
        result.Add(new PatientIntakeOutputDto()
        {
            PatientId = patientId,
            Type = ChartingType.OUTPUT,
            ChartingTypeText = "Output",
            SuggestedText = outputDB
        });

        return result;
    }

    public async Task<PatientIntakeOutputDto> GetOutputSuggestions(long patientId)
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

        return new PatientIntakeOutputDto()
        {
            PatientId = patientId,
            Type = ChartingType.OUTPUT,
            ChartingTypeText = "Output",
            SuggestedText = await Task.FromResult(outPutList)
        };
    }

    public async Task<IntakeOutputReturnDto> GetIntakeOutputById(long id)
    {

        //Get assigned Intakes
        var result = await _IntakeOutputRepository.GetAll().Where(s => s.Id == id && s.IsDeleted == false).Select(s => new IntakeOutputReturnDto
        {
            Id = s.Id,
            PatientId = s.PatientId,
            Type = s.Type,
            Pannel = s.Type == ChartingType.INTAKE ? "Intake" : "Output",
            SuggestedText = s.SuggestedText,
            VolumnInMls = s.VolumnInMls
        }).FirstOrDefaultAsync();

        return result;
    }

    public async Task<List<IntakeOutputReturnDto>> GetIntakeOutputByText(long patientId, string text)
    {

        //Get assigned Intakes
        var result = await _IntakeOutputRepository.GetAll().Where(s => s.PatientId == patientId && s.SuggestedText == text && s.IsDeleted == false).Select(s => new IntakeOutputReturnDto
        {
            Id = s.Id,
            PatientId = s.PatientId,
            Type = s.Type,
            Pannel = s.Type == ChartingType.INTAKE ? "Intake" : "Output",
            SuggestedText = s.SuggestedText,
            VolumnInMls = s.VolumnInMls
        }).ToListAsync();

        return result;
    }
}
