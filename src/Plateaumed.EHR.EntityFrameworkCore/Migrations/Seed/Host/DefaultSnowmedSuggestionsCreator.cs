using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Plateaumed.EHR.AllInputs;
using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.Misc.Json;
using Plateaumed.EHR.Misc.Json.Snomed;
using Plateaumed.EHR.Symptom;

namespace Plateaumed.EHR.Migrations.Seed.Host;

public class DefaultSnowmedSuggestionsCreator
{
    private readonly EHRDbContext _context;
    
    public DefaultSnowmedSuggestionsCreator(EHRDbContext context)
    {
        _context = context;
    }

    public void Create()
    {
        CreateSuggestionsBasedOnInputType();
    }

    private void CreateSuggestionsBasedOnInputType()
    {
        if(!_context.SnowmedSuggestions.ToList().Any(v => v.Type == AllInputType.Character))
        {
            var characters = getSnowmedSuggestions_Character();
            foreach (var item in characters)
                _context.SnowmedSuggestions.Add(item);
                
            _context.SaveChanges();
        }
        
        if(!_context.SnowmedSuggestions.ToList().Any(v => v.Type == AllInputType.Associations))
        {
            var associations = getSnowmedSuggestions_Associations();
            foreach (var item in associations)
                _context.SnowmedSuggestions.Add(item);
                
            _context.SaveChanges();
        }
        
        if(!_context.SnowmedSuggestions.ToList().Any(v => v.Type == AllInputType.Exacerbating))
        {
            var exacerbating = getSnowmedSuggestions_Exacerbating();
            foreach (var item in exacerbating)
                _context.SnowmedSuggestions.Add(item);
                
            _context.SaveChanges();
        }
        
        if(!_context.SnowmedSuggestions.ToList().Any(v => v.Type == AllInputType.BedMaking))
        {
            var bedMaking = getSnowmedSuggestions_BedMaking();
            foreach (var item in bedMaking)
                _context.SnowmedSuggestions.Add(item);
                
            _context.SaveChanges();
        }
        
        if(!_context.SnowmedSuggestions.ToList().Any(v => v.Type == AllInputType.Procedure))
        {
            var procedures = getSnowmedSuggestions_Procedures();
            foreach (var item in procedures)
                _context.SnowmedSuggestions.Add(item);
                
            _context.SaveChanges();
        }
        
        if(!_context.SnowmedSuggestions.ToList().Any(v => v.Type == AllInputType.PlanItems))
        {
            var planItems = getSnowmedSuggestions_PlanItems();
            foreach (var item in planItems)
                _context.SnowmedSuggestions.Add(item);
                
            _context.SaveChanges();
        }
        
        if(!_context.SnowmedSuggestions.ToList().Any(v => v.Type == AllInputType.InputNotes))
        {
            var inputNotes = getSnowmedSuggestions_InputNotes();
            foreach (var item in inputNotes)
                _context.SnowmedSuggestions.Add(item);
                
            _context.SaveChanges();
        }
        
        if(!_context.SnowmedSuggestions.ToList().Any(v => v.Type == AllInputType.WoundDressing))
        {
            var woundDressing = getSnowmedSuggestions_WoundDressing();
            foreach (var item in woundDressing)
                _context.SnowmedSuggestions.Add(item);
                
            _context.SaveChanges();
        }
        
        if(!_context.SnowmedSuggestions.ToList().Any(v => v.Type == AllInputType.Meals))
        {
            var meals = getSnowmedSuggestions_Meals();
            foreach (var item in meals)
                _context.SnowmedSuggestions.Add(item);
                
            _context.SaveChanges();
        }
    }
    
    private List<SnowmedSuggestion> getSnowmedSuggestions_Character()
    { 
        var data = System.Text.Json.JsonSerializer.Deserialize<List<SnowmedSuggestion>>(SnowmedCharacterSuggestions.jsonData, 
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return data;
    }
    
    private List<SnowmedSuggestion> getSnowmedSuggestions_Associations()
    { 
        var data = System.Text.Json.JsonSerializer.Deserialize<List<SnowmedSuggestion>>(SnowmedAssociationSuggestions.jsonData, 
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return data;
    }
    
    private List<SnowmedSuggestion> getSnowmedSuggestions_Exacerbating()
    { 
        var data = System.Text.Json.JsonSerializer.Deserialize<List<SnowmedSuggestion>>(SnowmedExacerbatingSuggestions.jsonData, 
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return data;
    }
    
    private List<SnowmedSuggestion> getSnowmedSuggestions_BedMaking()
    { 
        var data = JsonSerializer.Deserialize<List<SnowmedSuggestion>>(SnowmedBedMakingSuggestions.jsonData, 
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return data;
    }
    
    private List<SnowmedSuggestion> getSnowmedSuggestions_Procedures()
    { 
        var data = JsonSerializer.Deserialize<List<SnowmedSuggestion>>(SnowmedProcedureSuggestions.jsonData, 
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return data;
    }
    
    private List<SnowmedSuggestion> getSnowmedSuggestions_PlanItems()
    {
        var data = JsonSerializer.Deserialize<List<SnowmedSuggestion>>(SnowmedPlanItemsSuggestions.jsonData, 
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return data;
    }
    
    private List<SnowmedSuggestion> getSnowmedSuggestions_InputNotes()
    {
        var data = JsonSerializer.Deserialize<List<SnowmedSuggestion>>(SnowmedInputNotesSuggestions.jsonData, 
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return data;
    }
   
    private List<SnowmedSuggestion> getSnowmedSuggestions_WoundDressing()
    {
        var data = JsonSerializer.Deserialize<List<SnowmedSuggestion>>(SnowmedWoundDressingSuggestions.jsonData, 
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return data;
    }
    
    private List<SnowmedSuggestion> getSnowmedSuggestions_Meals()
    {
        var data = JsonSerializer.Deserialize<List<SnowmedSuggestion>>(SnowmedMealsSuggestions.jsonData, 
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return data;
    }
    
}