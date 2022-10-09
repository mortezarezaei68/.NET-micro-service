namespace ProductManagement.Core;

public interface IProductRepository
{
    void Add(Product product);
    void SaveChange();
}