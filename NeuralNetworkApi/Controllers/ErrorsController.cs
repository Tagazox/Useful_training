using FileManager.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NeuralNetworkApi.ViewModel;

namespace NeuralNetworkApi.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        [Route("error")]
        public ErrorResponse Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context.Error; 
            var code = 500; 
            if (exception is CantFindException) code = 404; 

            Response.StatusCode = code;

            return new ErrorResponse(exception);
        }
    }
}