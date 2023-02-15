using BasketManagement.Core;
using Framework.Controller.Extensions;
using Framework.Exception.DataAccessConfig;
using Framework.Exception.Exceptions;
using Framework.Exception.Exceptions.Enum;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace BasketManagement.Presenter.Controllers;

[ApiController]    
[ApiResultFilter]
// [ApiExplorerSettings(GroupName = "v1")]
// [ApiVersion("1.0")]
// [Route("api/[controller]")]
[Route("api/v{version:apiVersion}/[controller]")]
public class TestController: BaseControllerV1
{
    private readonly ILogger<TestController> _logger;

    public TestController( ILogger<TestController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync()
    {
         
        //var test=IntToStringDel;
        await DoWork(() => Task.Run(() => Console.WriteLine("Test")), 3,_logger);
        return Ok();

        // throw new InternalServerException("test", ResultCode.BadRequest);
        // var test=new List<NestedTestModel>()
        // {
        //     new()
        //     {
        //         Family = "Rezaei",
        //         Name = "Morteza"
        //     },
        //     new()
        //     {
        //         Family = "Mazloomi",
        //         Name = "Ramin"
        //     }
        // };
        // var testModel = new TestModel(ResultStatus.Duplicate, test);
        // return Ok(testModel);
    }
    

    async Task DoWork(Func<Task> work, int iterationCount, ILogger<TestController> logger)
    {
        int count = 0;
        while (count<iterationCount)
        {
            try
            {
                Task.Run(async () =>
                {
                    await work();
                    count++;
                });
            }
            catch (Exception e)
            {
                logger.LogTrace($"{count}:test3");
                throw;
            }
            finally
            {
                logger.LogTrace($"{count}:test1");

            }
        }
        logger.LogTrace($"{count}:test2");
    }
    

    // public static int GetValues()
    // {
    //     var values = new int[] { 1, 2, 3 };
    //     try
    //     {
    //         return values[5];
    //     }
    //     catch (Exception e)
    //     {
    //         Console.WriteLine(e);
    //         throw;
    //     }
    //     finally
    //     {
    //         return -1;
    //     }
    // }
    //
    // [HttpPost]
    // public  ApiResult<List<NestedTestModel> >CreateTestAsync()
    // {
    //     // throw new InternalServerException("test", ResultCode.BadRequest);
    //     var test=new List<NestedTestModel>()
    //     {
    //         new()
    //         {
    //             Family = "Rezaei",
    //             Name = "Morteza"
    //         },
    //         new()
    //         {
    //             Family = "Mazloomi",
    //             Name = "Ramin"
    //         }
    //     };
    //     var testModel = new TestModel(ResultStatus.Duplicate, test);
    // }

    public class TestModel : ResponseCommand
    {
        public TestModel(ResultStatus status, List<NestedTestModel>? result) : base(status, result)
        {
        }
    }

    public class NestedTestModel
    {
        public string Name { get; set; }
        public string Family { get; set; }
    }
}