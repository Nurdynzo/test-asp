using System.Collections.Generic;
using Plateaumed.EHR.Authorization.Users.Importing.Dto;
using Plateaumed.EHR.Dto;

namespace Plateaumed.EHR.Authorization.Users.Importing
{
    public interface IInvalidUserExporter
    {
        FileDto ExportToFile(List<ImportUserDto> userListDtos);
    }
}
