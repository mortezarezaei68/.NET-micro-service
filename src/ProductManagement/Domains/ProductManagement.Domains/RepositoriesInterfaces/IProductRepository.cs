using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ProductManagement.Domains.RepositoriesInterfaces;

public interface IProductRepository
{
    void Add(Product product);
    void Update(Product product);
    Task<Product?> GetByIdAsync(int id,CancellationToken cancellationToken);
    IAsyncEnumerator<Product> GetAsync(CancellationToken cancellationToken);
}