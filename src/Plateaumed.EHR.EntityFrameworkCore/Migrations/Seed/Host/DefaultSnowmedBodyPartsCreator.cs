using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Plateaumed.EHR.AllInputs;
using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.Misc.Json;
using Plateaumed.EHR.Misc.Json.Snomed;

namespace Plateaumed.EHR.Migrations.Seed.Host;

public class DefaultSnowmedBodyPartsCreator
{ 
    private readonly EHRDbContext _context;
    
    public DefaultSnowmedBodyPartsCreator(EHRDbContext context)
    {
        _context = context;
    }

    public void Create()
    {
        CreateSnowmedBodyParts();
    }

    private void CreateSnowmedBodyParts()
    {
        if(!_context.SnowmedBodyParts.ToList().Any())
        {
            var bodyParts = GetSnowmedBodyParts();
            foreach (var bodyPart in bodyParts)
                _context.SnowmedBodyParts.Add(bodyPart);
                
            _context.SaveChanges();
        }
    }

    private List<SnowmedBodyPart> GetSnowmedBodyParts()
    { 
        var bodyParts = System.Text.Json.JsonSerializer.Deserialize<List<SnowmedBodyPart>>(SnowmedBodyparts.jsonData, 
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        
        return bodyParts;
    }
}