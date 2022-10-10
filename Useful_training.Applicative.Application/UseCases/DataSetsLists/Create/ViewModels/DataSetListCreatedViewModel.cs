namespace Useful_training.Applicative.Application.UseCases.DataSetsLists.Create.ViewModels;

public class DataSetListCreatedViewModel
{
    public DataSetListCreatedViewModel(string dataSetsListName)
    {
        DataSetsListName = dataSetsListName;
    }

    public string DataSetsListName { get; }
}