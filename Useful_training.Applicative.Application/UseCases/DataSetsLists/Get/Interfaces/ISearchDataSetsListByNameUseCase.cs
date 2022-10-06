using Useful_training.Applicative.Application.UseCases.DataSetsLists.Get.ViewModels;

namespace Useful_training.Applicative.Application.UseCases.DataSetsLists.Get.Interfaces;

public interface ISearchDataSetsListByNameUseCase
{
    DataSetsAvailableViewModel Execute(string? like,int start=0,int count=10);
}