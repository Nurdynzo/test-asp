using System.Threading.Tasks;

namespace Plateaumed.EHR.Security
{
    public interface IPasswordComplexitySettingStore
    {
        Task<PasswordComplexitySetting> GetSettingsAsync();
    }
}
