using Useful_training.Applicative.Application.UseCases.Calculation.Get.Interfaces;
using Useful_training.Applicative.Application.UseCases.Calculation.Get.ViewModels;
using Useful_training.Core.NeuralNetwork.NeuralNetwork;
using Useful_training.Core.NeuralNetwork.Warehouse.Interfaces;

namespace Useful_training.Applicative.Application.UseCases.Calculation.Get;

public class GetNeuralNetworkCalculationUseCase : IGetNeuralNetworkCalculationUseCase
{
    private readonly INeuralNetworkWarehouse NeuralNetworkWarehouse;

    public GetNeuralNetworkCalculationUseCase(INeuralNetworkWarehouse neuralNetworkWarehouse)
    {
        NeuralNetworkWarehouse = neuralNetworkWarehouse;
    }

    public GetNeuralNetworkCalculationViewModel Execute(string name, List<double> inputs)
    {
        return new GetNeuralNetworkCalculationViewModel(
            NeuralNetworkWarehouse.Retrieve<NeuralNetwork>(name).Calculate(inputs), inputs);
    }
}