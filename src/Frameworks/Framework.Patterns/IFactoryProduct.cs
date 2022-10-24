namespace Framework.Patterns;

public interface IFactoryProduct
{
    IProduct CreateProduct(ProductType productType);
}