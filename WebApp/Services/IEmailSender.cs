using System.Threading.Tasks;

namespace RealEstateCrm.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
