using Framework.Domain.Core;
using Microsoft.AspNetCore.Identity;

namespace UserManagement.Core.Domains;

public class Role:IdentityRole<int>,IEntityAudit
{
    public Role(string title, string description)
    {
        Title = title;
        Description = description;
    }
    public string Title { get; private set; }
    public string Description { get; private set; }
    
    public DateTime CreatedAt { get; set; }
    public int CreatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime DeletedAt { get; set; }
}