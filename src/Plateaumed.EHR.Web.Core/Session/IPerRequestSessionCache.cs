using System.Threading.Tasks;
using Plateaumed.EHR.Sessions.Dto;

namespace Plateaumed.EHR.Web.Session
{
    public interface IPerRequestSessionCache
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformationsAsync();
    }
}
