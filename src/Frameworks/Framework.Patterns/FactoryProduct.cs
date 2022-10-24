namespace Framework.Patterns;

public static class FactoryProduct

{
    public static IProduct CreateProduct(ProductType productType)
    {
        return productType switch
        {
            ProductType.Concrete1 => new ConcreteProduct1(),
            ProductType.Concrete2 => new ConcreteProduct2(),
            _ => throw new ArgumentOutOfRangeException(nameof(productType), productType, null)
        };
    }
}