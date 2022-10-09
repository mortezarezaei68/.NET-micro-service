using Microsoft.AspNetCore.Mvc;
using ProductManagement.Core;

namespace ProductManagement.Presenter.Controllers;

[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public TestController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IActionResult> CreateAsync([FromBody] Product product)
    {
        for (var i = 0; i < 1000; i++)
        {
            _productRepository.Add(product);
            _productRepository.SaveChange();
        }

        return Ok();
    }
}