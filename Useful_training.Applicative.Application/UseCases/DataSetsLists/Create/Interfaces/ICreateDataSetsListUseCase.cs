using Useful_training.Applicative.Application.UseCases.DataSetsLists.Create.ViewModels;
using Useful_training.Core.NeuralNetwork.ValueObject;

namespace Useful_training.Applicative.Application.UseCases.DataSetsLists.Create.Interfaces;

public interface ICreateDataSetsListUseCase
{
    Task<DataSetListCreatedViewModel> ExecuteAsync(string name, List<DataSet> dataSets);
}