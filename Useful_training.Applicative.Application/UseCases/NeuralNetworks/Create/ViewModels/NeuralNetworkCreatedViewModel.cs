namespace Useful_training.Applicative.Application.UseCases.NeuralNetworks.Create.ViewModels;

public class NeuralNetworkCreatedViewModel
{
    public NeuralNetworkCreatedViewModel(string finalNameOfTheNeuralNetwork)
    {
        FinalNameOfTheNeuralNetwork = finalNameOfTheNeuralNetwork;
    }
    public string FinalNameOfTheNeuralNetwork { get; }
}