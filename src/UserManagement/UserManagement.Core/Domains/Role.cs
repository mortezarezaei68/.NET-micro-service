using Framework.Domain.Core;

namespace UserManagement.Core.Domains;

public class Role:AggregateRoot<int>
{
    public string Title { get; set; }
    public string Description { get; set; }
}