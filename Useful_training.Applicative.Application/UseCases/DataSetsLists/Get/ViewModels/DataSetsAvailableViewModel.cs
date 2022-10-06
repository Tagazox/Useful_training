namespace Useful_training.Applicative.Application.UseCases.DataSetsLists.Get.ViewModels;

public class DataSetsAvailableViewModel
{
    public DataSetsAvailableViewModel(IEnumerable<string> nameOfTheFoundDataSets)
    {
        NameOfTheFoundDataSets = nameOfTheFoundDataSets;
    }
    public IEnumerable<string> NameOfTheFoundDataSets { get; }
}