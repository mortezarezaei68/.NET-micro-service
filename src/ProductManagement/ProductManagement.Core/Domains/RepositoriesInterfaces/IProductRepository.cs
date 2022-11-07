using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ProductManagement.Core.Domains.RepositoriesInterfaces;

public interface IProductRepository
{
    ValueTask<EntityEntry<Product>> AddAsync(Product product,CancellationToken cancellationToken);
    void Update(Product product);
    Task<Product?> GetByIdAsync(int id,CancellationToken cancellationToken);
    IAsyncEnumerator<Product> GetAsync(CancellationToken cancellationToken);
}