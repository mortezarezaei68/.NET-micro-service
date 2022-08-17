using Framework.Domain.Core;

namespace UserManagement.Core.Domains;

public class UserRole:ValueObject
{
    public UserRole(int roleId)
    {
        RoleId = roleId;
    }
    public int RoleId { get; private set; }
} 