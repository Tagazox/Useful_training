using Useful_training.Applicative.Application.UseCases.Calculation.Get.ViewModels;

namespace Useful_training.Applicative.Application.UseCases.Calculation.Get.Interfaces;

public interface IGetNeuralNetworkCalculationUseCase
{
    public GetNeuralNetworkCalculationViewModel Execute(string name, List<double> inputs);
}