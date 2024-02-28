using System.Linq;
using Abp.Application.Features;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Editions;
using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.Features;

namespace Plateaumed.EHR.Migrations.Seed.Host;

public class DefaultSubscribableEditionCreator
{
    private readonly EHRDbContext _context;

    public DefaultSubscribableEditionCreator(EHRDbContext context)
    {
        _context = context;
    }

    public void Create()
    {
        CreateSubscribableEditions();
    }
    
    private void CreateSubscribableEditions()
    { 
        // Add the default free trail edition
        var freeTrialEdition = _context.SubscribableEditions.IgnoreQueryFilters().FirstOrDefault(e => e.Name == EditionManager.FreeTrialEditionName);
        if (freeTrialEdition == null)
        { 
            freeTrialEdition = new SubscribableEdition { Name = EditionManager.FreeTrialEditionName, DisplayName = EditionManager.FreeTrialEditionName };
            _context.SubscribableEditions.Add(freeTrialEdition);
            _context.SaveChanges();  
            
            // add features to edition
            CreateFeatureIfNotExists(freeTrialEdition.Id, AppFeatures.Hospital, true);
            CreateFeatureIfNotExists(freeTrialEdition.Id, AppFeatures.LaboratoryEnabledHospitalFeature, true);
            CreateFeatureIfNotExists(freeTrialEdition.Id, AppFeatures.PharmarcyEnabledHospitalFeature, true);
            CreateFeatureIfNotExists(freeTrialEdition.Id, AppFeatures.Laboratory, true);
            CreateFeatureIfNotExists(freeTrialEdition.Id, AppFeatures.Pharmacy, true);
        } 
    }
    
    private void CreateFeatureIfNotExists(int editionId, string featureName, bool isEnabled)
    {
        var defaultEditionChatFeature = _context.EditionFeatureSettings.IgnoreQueryFilters()
            .FirstOrDefault(ef => ef.EditionId == editionId && ef.Name == featureName);

        if (defaultEditionChatFeature == null)
        {
            _context.EditionFeatureSettings.Add(new EditionFeatureSetting
            {
                Name = featureName,
                Value = isEnabled.ToString().ToLower(),
                EditionId = editionId
            });
        }
    }
}