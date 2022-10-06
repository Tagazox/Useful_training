namespace Useful_training.Applicative.Application.UseCases.Calculation.Get.ViewModels;

public class GetNeuralNetworkCalculationViewModel   
{
    public IList<double> ResultsOfTheCalculation { get; }
    public IList<double> InputsOfTheCalculation { get; }
        
    public GetNeuralNetworkCalculationViewModel(IList<double> resultsOfTheCalculation, IList<double> inputsOfTheCalculation)
    {
        ResultsOfTheCalculation = resultsOfTheCalculation;
        InputsOfTheCalculation = inputsOfTheCalculation;
    }
}