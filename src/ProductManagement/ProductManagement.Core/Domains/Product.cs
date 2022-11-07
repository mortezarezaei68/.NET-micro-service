using Framework.Domain.Core;

namespace ProductManagement.Core.Domains;

public class Product:AggregateRoot<int>
{
    public Product(string? name)
    {
        Name = name;
    }

    public string? Name { get; private init; }
    
    private readonly List<ProductDetail> _productDetails = new();

    public IReadOnlyCollection<ProductDetail> ProductDetails =>
        _productDetails;
}