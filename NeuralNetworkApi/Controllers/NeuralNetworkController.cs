using Useful_training.Applicative.Application.UseCases.NeuralNetworks.Create;
using Useful_training.Applicative.Application.UseCases.NeuralNetworks.Create.ViewModels;
using Useful_training.Applicative.Application.UseCases.NeuralNetworks.Get;
using Useful_training.Applicative.Application.UseCases.NeuralNetworks.Get.ViewModels;
using Useful_training.Core.NeuralNetwork.Neurons.Type.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Useful_training.Applicative.NeuralNetworkApi.Controllers;

[ApiController]
[Route("[controller]/[action]/")]
public class NeuralNetworkController : ControllerBase
{

    [HttpPost(Name = "PostNeuralNetwork")]
    public Task<NeuralNetworkCreatedViewModel> Post([FromServices] CreateNeuralNetworkUseCase createNeuralNetworkUseCase , string name, uint numberOfInput, uint numberOfOutputs, uint numberOfHiddenLayer, uint numberOfNeuronByHiddenLayer, double learnRate, double momentum, NeuronType typeOfNeuron)
    {
        return createNeuralNetworkUseCase.ExecuteAsync(name, numberOfInput, numberOfOutputs, numberOfHiddenLayer, numberOfNeuronByHiddenLayer, learnRate, momentum, typeOfNeuron);
    }

    [HttpGet("{like}/{start:int}/{count:int}", Name = "SearchNeuralNetworkByName")]
    public NeuralNetworksFoundViewModel Get([FromServices] SearchNeuralNetworkByNameUseCase searchNeuralNetworkByNameUseCase ,string? like, int start = 0, int count = 10)
    {
        return searchNeuralNetworkByNameUseCase.Execute(like, start, count);
    }


}