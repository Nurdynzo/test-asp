using System.Threading.Tasks;

namespace Plateaumed.EHR.Security.Recaptcha
{
    public interface IRecaptchaValidator
    {
        Task ValidateAsync(string captchaResponse);
    }
}