using System.Threading.Tasks;

namespace UserManagement.Identity.Services;

public interface IEmailSender
{
    Task SendEmailAsync(string email, string subject, string message);
}
