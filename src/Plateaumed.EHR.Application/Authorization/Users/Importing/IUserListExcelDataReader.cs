using System.Collections.Generic;
using Plateaumed.EHR.Authorization.Users.Importing.Dto;
using Abp.Dependency;

namespace Plateaumed.EHR.Authorization.Users.Importing
{
    public interface IUserListExcelDataReader: ITransientDependency
    {
        List<ImportUserDto> GetUsersFromExcel(byte[] fileBytes);
    }
}
