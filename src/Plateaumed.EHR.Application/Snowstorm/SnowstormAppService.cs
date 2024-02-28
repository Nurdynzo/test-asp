using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plateaumed.EHR.Snowstorm.Abstractions;
using Plateaumed.EHR.Snowstorm.Dtos;

namespace Plateaumed.EHR.Snowstorm;

public class SnowstormAppService : EHRAppServiceBase, ISnowstormAppService
{
    private readonly IGetDiagnosisQueryHandler _getDiagnosisQueryHandler;
    private readonly IGetSnowmedSuggestionQueryHandler _getSnowmedSuggestionQueryHandler;
    
    /// <inheritdoc />
    public SnowstormAppService(IGetDiagnosisQueryHandler getDiagnosisQueryHandler, IGetSnowmedSuggestionQueryHandler getSnowmedSuggestionQueryHandler)
    {
        _getDiagnosisQueryHandler = getDiagnosisQueryHandler;
        _getSnowmedSuggestionQueryHandler = getSnowmedSuggestionQueryHandler;
    }
    
    /// <inheritdoc />
    public async Task<List<SnowstormSimpleResponseDto>> GetDiagnosisByTerm(string searchTerm)
    {
        var semanticTag = "finding";
        var semanticTag2 = "disorder";
        return await _getDiagnosisQueryHandler.Handle(new SnowstormRequestDto
        {
            Term = searchTerm,
            SemanticTag = semanticTag,
            SemanticTag2 = semanticTag2
        }); 
    }
    
    /// <inheritdoc />
    public async Task<List<SnowstormSimpleResponseDto>> GetSymptomByTerm(string searchTerm)
    {
        var semanticTag = "finding";
        var semanticTag2 = "disorder";
        return await _getDiagnosisQueryHandler.Handle(new SnowstormRequestDto
        {
            Term = searchTerm,
            SemanticTag = semanticTag,
            SemanticTag2 = semanticTag2
        }); 
    }
    
    /// <inheritdoc />
    public async Task<List<SnowstormSimpleResponseDto>> GetProcedureByTerm(string searchTerm)
    {
        var semanticTag = "procedure";
        var semanticTag2 = "regime/therapy";
        return await _getDiagnosisQueryHandler.Handle(new SnowstormRequestDto
        {
            Term = searchTerm,
            SemanticTag = semanticTag,
            SemanticTag2 = semanticTag2
        }); 
    }
    
    /// <inheritdoc />
    public async Task<List<SnowstormSimpleResponseDto>> GetMedicationByTerm(string searchTerm)
    {
        var semanticTag = "clinical drug";
        return await _getDiagnosisQueryHandler.Handle(new SnowstormRequestDto
        {
            Term = searchTerm,
            SemanticTag = semanticTag
        }); 
    }
    
    /// <inheritdoc />
    public async Task<List<SnowstormSimpleResponseDto>> GetBodyPartsByTerm(string searchTerm)
    {
        var semanticTag = "body structure";
        return await _getDiagnosisQueryHandler.Handle(new SnowstormRequestDto
        {
            Term = searchTerm,
            SemanticTag = semanticTag
        }); 
    }
    
    /// <inheritdoc />
    public async Task<List<SnowstormSimpleResponseDto>> GetSpecimenByTerm(string searchTerm)
    {
        var semanticTag = "specimen";
        return await _getDiagnosisQueryHandler.Handle(new SnowstormRequestDto
        {
            Term = searchTerm,
            SemanticTag = semanticTag
        }); 
    }
    
    /// <inheritdoc />
    public async Task<List<SnowstormSimpleResponseDto>> GetOrganismByTerm(string searchTerm)
    {
        var semanticTag = "organism";
        return await _getDiagnosisQueryHandler.Handle(new SnowstormRequestDto
        {
            Term = searchTerm,
            SemanticTag = semanticTag
        }); 
    }
    
    /// <inheritdoc />
    public async Task<List<SnowstormSimpleResponseDto>> GetSymptomSuggestion(long snowmedId, string inputType, string searchTerm)
    {
        return await _getSnowmedSuggestionQueryHandler.Handle(snowmedId, inputType, searchTerm);
    }
}