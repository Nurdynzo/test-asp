using System.Threading.Tasks;
using Abp.Application.Services;

namespace Plateaumed.EHR.Facilities
{
    public interface IWardBedsManager : IApplicationService
	{
        Task OccupyWardBed(long wardBedId, long encounterId);
        Task DeOccupyWardBed(long? wardBedId);
    }
}