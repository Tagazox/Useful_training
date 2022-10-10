using Microsoft.AspNetCore.Mvc;
using Useful_training.Applicative.Application.UseCases.Calculation.Get.Interfaces;
using Useful_training.Applicative.Application.UseCases.Calculation.Get.ViewModels;

namespace Useful_training.Applicative.NeuralNetworkApi.Controllers;

[ApiController]
[Route("[controller]/[action]/")]
public class CalculateController : ControllerBase
{

    [HttpGet("{name}/{inputs}", Name = "GetCalculationResult")]
    public GetNeuralNetworkCalculationViewModel Get([FromServices] IGetNeuralNetworkCalculationUseCase getNeuralNetworkCalculationUseCase,string name, [FromQuery] List<double> inputs)
    {
        return getNeuralNetworkCalculationUseCase.Execute(name,inputs);
    }

}