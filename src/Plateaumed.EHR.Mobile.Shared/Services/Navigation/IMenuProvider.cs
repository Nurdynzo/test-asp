using System.Collections.Generic;
using MvvmHelpers;
using Plateaumed.EHR.Models.NavigationMenu;

namespace Plateaumed.EHR.Services.Navigation
{
    public interface IMenuProvider
    {
        ObservableRangeCollection<NavigationMenuItem> GetAuthorizedMenuItems(Dictionary<string, string> grantedPermissions);
    }
}