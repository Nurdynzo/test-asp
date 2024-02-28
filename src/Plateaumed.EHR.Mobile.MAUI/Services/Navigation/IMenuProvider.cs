using Plateaumed.EHR.Models.NavigationMenu;

namespace Plateaumed.EHR.Services.Navigation
{
    public interface IMenuProvider
    {
        List<NavigationMenuItem> GetAuthorizedMenuItems(Dictionary<string, string> grantedPermissions);
    }
}