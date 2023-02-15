using Framework.Domain.Core;

namespace ProductManagement.Domains;

public class Product : AggregateRoot<int>
{
    public Product(string? name)
    {
        Name = name;
    }

    public void UpdateProductDetail(Dictionary<string, string> productDetailValues)
    {
        var productDetails = productDetailValues.Select(a => new ProductDetail(a.Key, a.Value)).ToList();
        _productDetails.Update(productDetails);
    }

    public string? Name { get; private init; }

    private readonly List<ProductDetail> _productDetails = new();

    public IReadOnlyCollection<ProductDetail> ProductDetails =>
        _productDetails;
}