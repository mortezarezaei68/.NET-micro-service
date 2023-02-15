using Microsoft.EntityFrameworkCore;
using ProductManagement.Domains;
using ProductManagement.Domains.RepositoriesInterfaces;

namespace ProductManagement.Configurations.Ef;

public class ProductRepository : IProductRepository
{
    private readonly ProductDbContext _context;

    public ProductRepository(ProductDbContext context)
    {
        _context = context;
    }

    public void Add(Product product)
    => _context.Products.Add(product);
    

    public void Update(Product product)
    => _context.Products.Update(product);

        public Task<Product?> GetByIdAsync(int id,CancellationToken cancellationToken)
    => _context.Products.FirstOrDefaultAsync(a => a.Id == id, cancellationToken: cancellationToken);
    

    public IAsyncEnumerator<Product> GetAsync(CancellationToken cancellationToken)
    => _context.Products.GetAsyncEnumerator(cancellationToken);
    
}