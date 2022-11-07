using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProductManagement.Core.Domains;
using ProductManagement.Core.Domains.RepositoriesInterfaces;

namespace ProductManagement.Core.PersistenceInfrastructureEfCore;

public class ProductRepository : IProductRepository
{
    private readonly ProductDbContext _context;

    public ProductRepository(ProductDbContext context)
    {
        _context = context;
    }

    public ValueTask<EntityEntry<Product>> AddAsync(Product product,CancellationToken cancellationToken)
    => _context.Products.AddAsync(product, cancellationToken);
    

    public void Update(Product product)
    => _context.Products.Update(product);

        public Task<Product?> GetByIdAsync(int id,CancellationToken cancellationToken)
    => _context.Products.FirstOrDefaultAsync(a => a.Id == id, cancellationToken: cancellationToken);
    

    public IAsyncEnumerator<Product> GetAsync(CancellationToken cancellationToken)
    => _context.Products.GetAsyncEnumerator(cancellationToken);
    
}