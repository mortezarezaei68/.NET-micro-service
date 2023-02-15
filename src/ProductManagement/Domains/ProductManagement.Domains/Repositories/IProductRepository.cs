namespace ProductManagement.Domains.Repositories;

public interface IProductRepository
{
    void Add(Product product);
    void Update(Product product);
    Task<Product?> GetByIdAsync(int id,CancellationToken cancellationToken);
    IAsyncEnumerator<Product> GetAsync(CancellationToken cancellationToken);
}