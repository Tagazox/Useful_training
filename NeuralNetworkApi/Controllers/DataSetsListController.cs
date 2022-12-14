using Useful_training.Applicative.Application.UseCases.DataSetsLists.Create.Interfaces;
using Useful_training.Applicative.Application.UseCases.DataSetsLists.Create.ViewModels;
using Useful_training.Applicative.Application.UseCases.DataSetsLists.Get.Interfaces;
using Useful_training.Applicative.Application.UseCases.DataSetsLists.Get.ViewModels;
using Useful_training.Applicative.Application.Adapter;
using Microsoft.AspNetCore.Mvc;

namespace Useful_training.Applicative.NeuralNetworkApi.Controllers;

[ApiController]
[Route("[controller]/[action]/")]
public class DataSetsListController : ControllerBase
{
    [HttpPost("{name}", Name = "PostDataSetsList")]
    public Task<DataSetListCreatedViewModel> Post([FromServices] ICreateDataSetsListUseCase createDataSetUseCase, string name, [FromBody] List<DataSetsListAdapter> dataSets)
    {
        return createDataSetUseCase.ExecuteAsync(name,dataSets);
    }

    [HttpGet("{name}",Name = "GetDataSetListByName")]
    public DataSetsListViewModel Get([FromServices] IGetDataSetsListByNameUseCase getDataSetsListByName,string name)
    {
        return getDataSetsListByName.Execute(name);
    }

    [HttpGet("{like}/{start:int}/{count:int}", Name = "SearchDataSetListByName")]
    public DataSetsAvailableViewModel Get([FromServices] ISearchDataSetsListByNameUseCase searchDataSetsListByName,string? like,int start=0,int count=10)
    {
        return searchDataSetsListByName.Execute(like, start, count);
    }
}