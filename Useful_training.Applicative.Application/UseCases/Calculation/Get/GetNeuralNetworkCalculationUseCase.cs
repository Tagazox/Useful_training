using Useful_training.Applicative.Application.Ports;
using Useful_training.Applicative.Application.UseCases.Calculation.Get.Interfaces;
using Useful_training.Applicative.Application.UseCases.Calculation.Get.ViewModels;
using Useful_training.Core.NeuralNetwork.NeuralNetwork;

namespace Useful_training.Applicative.Application.UseCases.Calculation.Get;

public class GetNeuralNetworkCalculationUseCase : IGetNeuralNetworkCalculationUseCase
{
    private readonly INeuralNetworkWarehouse _neuralNetworkWarehouse;

    public GetNeuralNetworkCalculationUseCase(INeuralNetworkWarehouse neuralNetworkWarehouse)
    {
        _neuralNetworkWarehouse = neuralNetworkWarehouse;
    }

    public GetNeuralNetworkCalculationViewModel Execute(string name, List<double> inputs)
    {
        return new GetNeuralNetworkCalculationViewModel(
            _neuralNetworkWarehouse.Retrieve<NeuralNetwork>(name).Calculate(inputs), inputs);
    }
}