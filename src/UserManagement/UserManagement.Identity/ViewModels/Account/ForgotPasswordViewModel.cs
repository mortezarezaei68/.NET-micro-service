using System.ComponentModel.DataAnnotations;

namespace UserManagement.Identity.ViewModels.Account;

public class ForgotPasswordViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}
