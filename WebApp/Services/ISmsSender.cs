using System.Threading.Tasks;

namespace RealEstateCrm.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
