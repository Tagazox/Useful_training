using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Useful_training.Applicative.NeuralNetworkApi.ViewModel;
using Useful_training.Infrastructure.FileManager.Exception;

namespace Useful_training.Applicative.NeuralNetworkApi.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorsController : ControllerBase
{
    [Route("error")]
    public ErrorResponse Error()
    {
        IExceptionHandlerFeature? context = HttpContext.Features.Get<IExceptionHandlerFeature>();
        Exception? exception = context?.Error; 
        int code = 500; 
        if (exception is CantFindException) code = 404; 

        Response.StatusCode = code;

        return new ErrorResponse(exception);
    }
}