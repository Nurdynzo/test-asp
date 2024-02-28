using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.Migrations.Seed.Tenants;

namespace Plateaumed.EHR.Migrations.Seed.Host;

public class MedicalDataSeeder
{
    public static void Seed(EHRDbContext context)
    {
        new DefaultSnowmedBodyPartsCreator(context).Create();
        new DefaultSnowmedSuggestionsCreator(context).Create();
        new DefaultMedicationProductsCreator(context).Create();

        VaccineSeeder.Seed(context);
        InvestigationSeeder.Seed(context);
        VitalSignSeeder.Seed(context);
        ChronicDiseaseSeeder.Seed(context);
        AllergyReactionExperiencedSeeder.Seed(context);
        AllergyTypeSeeder.Seed(context);
        PatientGynaecologicalIllnessSuggestionSeeder.Seed(context);
        TypeOfContraceptionSuggestionSeeder.Seed(context);
        PostmenopausalSymptomSeeder.Seed(context);
        PhysicalExaminationsSeeder.Seed(context);
        PatientGynaecologicProcedureSeeder.Seed(context);
        PatientImplantSeeder.Seed(context);
        ReviewOfSystemsSuggestionSeeder.Seed(context);
        DiagnosisSeeder.Seed(context);
        ProcedureSuggestionSeeder.Seed(context);
        RecreationDrugSuggestionSeeder.Seed(context);
        AlcoholTypesSeeder.Seed(context);
        CigaretteTypeSeeder.Seed(context);
        WardEmergencySeeder.Seed(context);
        NurseCarePlanSeeder.Seed(context);
        VaccinationSuggestionSeeder.Seed(context);
        FeedingSuggestionsSeeder.Seed(context);

        context.SaveChanges();
    }
}