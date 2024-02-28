using Abp.AutoMapper;
using Plateaumed.EHR.Authorization.Users.Dto;

namespace Plateaumed.EHR.Mobile.MAUI.Models.User
{
    [AutoMapFrom(typeof(UserListDto))]
    public class UserListModel : UserListDto
    {
        public string Photo { get; set; }

        public string FullName => Name + " " + Surname;
    }
}
