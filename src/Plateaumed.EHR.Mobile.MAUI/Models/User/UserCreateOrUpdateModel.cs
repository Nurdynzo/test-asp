using Abp.AutoMapper;
using Plateaumed.EHR.Authorization.Users.Dto;

namespace Plateaumed.EHR.Mobile.MAUI.Models.User
{
    [AutoMapFrom(typeof(CreateOrUpdateUserInput))]
    public class UserCreateOrUpdateModel : CreateOrUpdateUserInput
    {

    }
}
