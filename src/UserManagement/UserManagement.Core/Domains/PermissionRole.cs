using Framework.Domain.Core;

namespace UserManagement.Core.Domains;

public class PermissionRole:ValueObject
{
    public int PermissionId { get; set; }
}