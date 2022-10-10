namespace Useful_training.Applicative.Application.UseCases.NeuralNetworks.Get.ViewModels;

public class NeuralNetworksFoundViewModel
{
    public NeuralNetworksFoundViewModel(IEnumerable<string> namesList)
    {
        NamesList = namesList;
    }

    public IEnumerable<string> NamesList { get; }
}