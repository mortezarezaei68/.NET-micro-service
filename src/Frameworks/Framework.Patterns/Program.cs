// See https://aka.ms/new-console-template for more information

using Framework.Patterns;

var factory=FactoryProduct.CreateProduct(ProductType.Concrete2);
factory.Create(2);