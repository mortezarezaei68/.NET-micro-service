namespace Framework.Patterns;

public interface IProduct
{
    Task Create(int test);
    Task Update(object test);
    Task Get(string test);
}