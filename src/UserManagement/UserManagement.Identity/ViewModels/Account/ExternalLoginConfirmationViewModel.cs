using System.ComponentModel.DataAnnotations;

namespace UserManagement.Identity.ViewModels.Account;

public class ExternalLoginConfirmationViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}
