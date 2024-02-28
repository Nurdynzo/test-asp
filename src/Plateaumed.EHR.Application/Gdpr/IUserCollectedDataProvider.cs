using System.Collections.Generic;
using System.Threading.Tasks;
using Abp;
using Plateaumed.EHR.Dto;

namespace Plateaumed.EHR.Gdpr
{
    public interface IUserCollectedDataProvider
    {
        Task<List<FileDto>> GetFiles(UserIdentifier user);
    }
}
