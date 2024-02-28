using System.Threading.Tasks;
using Plateaumed.EHR.Security.Recaptcha;

namespace Plateaumed.EHR.Test.Base.Web
{
    public class FakeRecaptchaValidator : IRecaptchaValidator
    {
        public Task ValidateAsync(string captchaResponse)
        {
            return Task.CompletedTask;
        }
    }
}
