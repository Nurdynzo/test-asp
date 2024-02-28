using System.Collections.Generic;
using Plateaumed.EHR.Authorization.Users.Dto;
using Plateaumed.EHR.Dto;

namespace Plateaumed.EHR.Authorization.Users.Exporting
{
    public interface IUserListExcelExporter
    {
        FileDto ExportToFile(List<UserListDto> userListDtos);
    }
}