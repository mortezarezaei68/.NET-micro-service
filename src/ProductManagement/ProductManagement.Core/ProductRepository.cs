namespace ProductManagement.Core;

public class ProductRepository : IProductRepository
{
    private readonly ProductDbContext _context;

    public ProductRepository(ProductDbContext context)
    {
        _context = context;
    }

    public void Add(Product product)
    {
        _context.Add(product);
    }

    public void SaveChange()
    {
        _context.SaveChanges();
    }

}