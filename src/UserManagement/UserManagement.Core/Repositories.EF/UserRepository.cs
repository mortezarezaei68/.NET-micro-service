using UserManagement.Core.Domains;
using UserManagement.Core.UserManagementContextConcept;

namespace UserManagement.Core.Repositories.EF;

public class UserRepository:IUserRepository
{
    private readonly UserManagementContext _context;

    public UserRepository(UserManagementContext context)
    {
        _context = context;
    }

    public void Add(User user)
    {
        _context.Users.Add(user);
    }

    public void Update(User user)
    {
        _context.Users.Update(user);
    }
}