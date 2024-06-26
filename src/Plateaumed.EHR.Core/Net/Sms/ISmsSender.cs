using System.Threading.Tasks;

namespace Plateaumed.EHR.Net.Sms
{
    public interface ISmsSender
    {
        Task SendAsync(string number, string message);
    }
}