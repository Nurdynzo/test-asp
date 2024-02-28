namespace Plateaumed.EHR.MultiTenancy
{
    public class TenantConsts
    {
        public const string TenancyNameRegex = "^[a-zA-Z][a-zA-Z0-9_-]{1,}$";

        public const string DefaultTenantName = "Default";

        public const int MaxNameLength = 128;

        public const int DefaultTenantId = 1;

        public const int MinIndividualSpecializationLength = 0;

        public const int MaxIndividualSpecializationLength = 120;

        public const int MinIndividualGraduatingSchoolLength = 0;

        public const int MaxIndividualGraduatingSchoolLength = 120;

        public const int MinIndividualGraduatingYearLength = 4;

        public const int MaxIndividualGraduatingYearLength = 4;
    }
}
