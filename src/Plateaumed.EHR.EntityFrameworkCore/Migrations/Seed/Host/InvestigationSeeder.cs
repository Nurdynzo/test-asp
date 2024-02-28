using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.Investigations;
using Plateaumed.EHR.Misc.Json;
using Plateaumed.EHR.Misc.Json.Investigations;

namespace Plateaumed.EHR.Migrations.Seed.Host
{
    public static class InvestigationSeeder
    {
        public static void Seed(EHRDbContext dbContext)
        {
            var chemistryJson = ChemistryInvestigationsJson.jsonData;
            var chemistryInvestigations = JsonConvert.DeserializeObject<List<Investigation>>(chemistryJson);
            if (!dbContext.Investigations.Any(i => i.Type == InvestigationTypes.Chemistry))
            {
                chemistryInvestigations.ForEach(i => dbContext.Investigations.Add(i));
            }

            var haematologyJson = HaematologyInvestigationsJson.jsonData;
            var haematologyInvestigations = JsonConvert.DeserializeObject<List<Investigation>>(haematologyJson);
            if (!dbContext.Investigations.Any(i => i.Type == InvestigationTypes.Haematology))
            {
                haematologyInvestigations.ForEach(i => dbContext.Investigations.Add(i));
            }

            var microbiologyCommonJson = MicrobiologyCommonSuggestionsJson.jsonData;
            var microbiologyCommonSuggestions = JsonConvert.DeserializeObject<List<InvestigationSuggestion>>(microbiologyCommonJson);
            if (!dbContext.InvestigationSuggestions.Any(x => x.Category == SuggestionCategories.CommonMicrobiology))
            {
                microbiologyCommonSuggestions.ForEach(s => dbContext.InvestigationSuggestions.Add(s));
            }

            var cultureJson = CultureSuggestionsJson.jsonData;
            var cultureSuggestions = JsonConvert.DeserializeObject<List<InvestigationSuggestion>>(cultureJson);
            if (!dbContext.InvestigationSuggestions.Any(x => x.Category == SuggestionCategories.Culture))
            {
                cultureSuggestions.ForEach(s => dbContext.InvestigationSuggestions.Add(s));
            }

            var gramStainJson = GramStainSuggestionsJson.jsonData;
            var gramStainSuggestions = JsonConvert.DeserializeObject<List<InvestigationSuggestion>>(gramStainJson);
            if (!dbContext.InvestigationSuggestions.Any(x => x.Category == SuggestionCategories.GramStain))
            {
                gramStainSuggestions.ForEach(s => dbContext.InvestigationSuggestions.Add(s));
            }

            var blueStainJson = BlueStainSuggestionsJson.jsonData;
            var blueStainSuggestions = JsonConvert.DeserializeObject<List<InvestigationSuggestion>>(blueStainJson);
            if (!dbContext.InvestigationSuggestions.Any(x => x.Category == SuggestionCategories.BlueStain))
            {
                blueStainSuggestions.ForEach(s => dbContext.InvestigationSuggestions.Add(s));
            }

            var microscopyJson = MicroscopySuggestionsJson.jsonData;
            var microscopySuggestions = JsonConvert.DeserializeObject<List<InvestigationSuggestion>>(microscopyJson);
            if (!dbContext.InvestigationSuggestions.Any(x =>
                    x.Category == SuggestionCategories.Microscopy))
            {
                microscopySuggestions.ForEach(s => dbContext.InvestigationSuggestions.Add(s));
            }

            var antibioticsJson = AntibioticSensitivityJson.jsonData;
            var antibioticsSuggestions = JsonConvert.DeserializeObject<List<InvestigationSuggestion>>(antibioticsJson);
            if (!dbContext.InvestigationSuggestions.Any(x =>
                    x.Category == SuggestionCategories.AntibioticSensitivity))
            {
                antibioticsSuggestions.ForEach(s => dbContext.InvestigationSuggestions.Add(s));
            }

            var microbiologyJson = MicrobiologyInvestigationsJson.jsonData;
            var microbiologyInvestigations = JsonConvert.DeserializeObject<List<Investigation>>(microbiologyJson);
            if (!dbContext.Investigations.Any(i => i.Type == InvestigationTypes.Microbiology))
            {
                microbiologyInvestigations.ForEach(i => dbContext.Investigations.Add(i));
            }

            var serologyJson = SerologyInvestigationJson.jsonData;
            var serologyInvestigations = JsonConvert.DeserializeObject<List<Investigation>>(serologyJson);
            if (!dbContext.Investigations.Any(i => i.Type == InvestigationTypes.Serology))
            {
                serologyInvestigations.ForEach(i => dbContext.Investigations.Add(i));
            }

            var electrophysiologyJson = ElectrophysiologyJson.jsonData;
            var electrophysiologyInvestigations = JsonConvert.DeserializeObject<List<Investigation>>(electrophysiologyJson);
            if (!dbContext.Investigations.Any(i => i.Type == InvestigationTypes.Electrophysiology))
            {
                electrophysiologyInvestigations.ForEach(i => dbContext.Investigations.Add(i));
            }

            var radiologyAndPulmJson = RadiologyAndPulmJson.jsonData;
            var radiologyAndPulmInvestigations = JsonConvert.DeserializeObject<List<Investigation>>(radiologyAndPulmJson);
            if (!dbContext.Investigations.Any(i => i.Type == InvestigationTypes.RadiologyAndPulm))
            {
                radiologyAndPulmInvestigations.ForEach(i => dbContext.Investigations.Add(i));
            }

            var echocardiogramSuggestionsJson = EchocardiogramSuggestionsJson.jsonData;
            var echocardiogramSuggestions = JsonConvert.DeserializeObject<List<InvestigationSuggestion>>(echocardiogramSuggestionsJson);
            if (!dbContext.InvestigationSuggestions.Any(x => x.Category == RadiologyCategories.Echocardiogram))
            {
                echocardiogramSuggestions.ForEach(s => dbContext.InvestigationSuggestions.Add(s));
            }

            _ = dbContext.SaveChanges();
        }
    }
}
