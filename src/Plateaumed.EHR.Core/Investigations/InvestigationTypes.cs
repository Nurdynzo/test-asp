namespace Plateaumed.EHR.Investigations;

public static class InvestigationTypes
{
    public const string Chemistry = "Chemistry";
    public const string Haematology = "Haematology";
    public const string Microbiology = "Microbiology";
    public const string Serology = "Serology";
    public const string RadiologyAndPulm = "Radiology + Pulm";
    public const string Electrophysiology = "Electrophysiology";
    public const string Others = "Others";
}

public static class SuggestionCategories
{
    public const string Microscopy = "Microscopy";
    public const string CommonMicrobiology = "Microbiology";
    public const string BlueStain = "Blue Stain";
    public const string GramStain = "Gram Stain";
    public const string Culture = "Culture";
    public const string AntibioticSensitivity = "AntibioticSensitivity";
    public const string Macroscopy = "Macroscopy";
}

public static class RadiologyCategories
{
    public const string Views = "Views";
    public const string BodyParts = "BodyParts";
    public const string Dimension = "Dimension";
    public const string Echocardiogram = "Echocardiogram";
}
