using Useful_training.Applicative.Application.UseCases.DataSetsLists.Get.ViewModels;

namespace Useful_training.Applicative.Application.UseCases.DataSetsLists.Get.Interfaces;

public interface IGetDataSetsListByNameUseCase
{
    public DataSetsListViewModel Execute(string name);
}