using Framework.Domain.Core;

namespace ProductManagement.Core.Domains;

public class ProductDetail:ValueObject
{
    public ProductDetail(string? key, string? value)
    {
        Key = key;
        Value = value;
    }

    public string? Value { get; private init; }
    public string? Key { get; private init; }
}