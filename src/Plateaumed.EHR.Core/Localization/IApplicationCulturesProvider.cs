using System.Globalization;

namespace Plateaumed.EHR.Localization
{
    public interface IApplicationCulturesProvider
    {
        CultureInfo[] GetAllCultures();
    }
}