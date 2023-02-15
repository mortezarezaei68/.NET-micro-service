using Mapster;
using ProductManagement.Core.HandlerCommands;
using ProductManagement.Presenter.ViewModels;

namespace ProductManagement.Presenter.ViewModelMappers;

public class CreateProductDetailMappers:IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(string id, CreateProductDetailModel createProductDetail), CreateProductDetailCommandRequest>()
            .Map(dest => dest.ProductId, src => src.id)
            .Map(dest => dest.ProductDetails, src => src.createProductDetail.ProductDetails);
    }
}