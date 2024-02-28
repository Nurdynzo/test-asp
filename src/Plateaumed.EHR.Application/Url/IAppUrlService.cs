namespace Plateaumed.EHR.Url
{
    public interface IAppUrlService
    {
        string CreateEmailActivationUrlFormat(int? tenantId);

        string CreateSharedRegistrationUrlFormat(string tenancyName);

        string CreatePasswordResetUrlFormat(int? tenantId);

        string CreateEmailActivationUrlFormat(string tenancyName);

        string CreatePasswordResetUrlFormat(string tenancyName);

        string CreateFacilityURL(string tenancyName);
    }
}
