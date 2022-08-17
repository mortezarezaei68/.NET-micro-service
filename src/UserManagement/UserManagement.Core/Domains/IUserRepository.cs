namespace UserManagement.Core.Domains;

public interface IUserRepository
{
    void Add(User user);
    void Update(User user);
}