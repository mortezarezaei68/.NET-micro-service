using Framework.Domain.Core;
using Microsoft.AspNetCore.Identity;

namespace UserManagement.Core.Domains;

public class User : IdentityUser<int>, IEntityAudit
{
    // private readonly List<UserRole> _userRoles = new();
    // public IReadOnlyCollection<UserRole> UserRoles => _userRoles;
    public string FirstName { get; private init; }
    public string LastName { get; private init; }
    public UserGenderType GenderType { get; private init; }

    public static User Add(UserGenderType genderType, string lastName, string firstName, string userName, string email)
    {
        return new User
        {
            GenderType = genderType,
            LastName = lastName,
            FirstName = firstName,
            UserName = userName,
            Email = email
        };
    }

    public DateTime CreatedAt { get; set; }
    public int CreatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime DeletedAt { get; set; }
}