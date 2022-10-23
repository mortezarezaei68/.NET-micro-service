using System.Threading.Tasks;

namespace UserManagement.Identity.Services;

public interface ISmsSender
{
    Task SendSmsAsync(string number, string message);
}
