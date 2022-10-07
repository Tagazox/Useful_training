using Microsoft.Extensions.DependencyInjection;
using Useful_training.Applicative.Application.UseCases.Calculation.Get;
using Useful_training.Applicative.Application.UseCases.Calculation.Get.Interfaces;
using Useful_training.Applicative.Application.UseCases.DataSetsLists.Create;
using Useful_training.Applicative.Application.UseCases.DataSetsLists.Create.Interfaces;
using Useful_training.Applicative.Application.UseCases.DataSetsLists.Get;
using Useful_training.Applicative.Application.UseCases.DataSetsLists.Get.Interfaces;
using Useful_training.Applicative.Application.UseCases.NeuralNetworks.Create;
using Useful_training.Applicative.Application.UseCases.NeuralNetworks.Get;
using Useful_training.Applicative.Application.UseCases.Training.Hub;
using Useful_training.Applicative.Application.UseCases.Training.Interfaces;
using Useful_training.Core.NeuralNetwork.Factory;
using Useful_training.Core.NeuralNetwork.Factory.Interfaces;
using Useful_training.Core.NeuralNetwork.Warehouse.Interfaces;
using Useful_training.Infrastructure.FileManager.Warehouse;

namespace Useful_training.Applicative.Application.Adapter;

public static class ServiceCollectionExtension
{
        public static void AddNeuralNetworkApplication(this IServiceCollection services,string neuralNetworkWarehouseRootPath, string datasetListWarehouseRootPath)
        {
                
                services.AddSingleton<INeuralNetworkWarehouse>(new NeuralNetworkFileWarehouse(neuralNetworkWarehouseRootPath));
                services.AddSingleton<IDataSetsListWarehouse>(new DataSetsListFileWarehouse(datasetListWarehouseRootPath));
                services.AddSingleton<INeuralNetworkBuilder, NeuralNetworkBuilder>();

                services.AddTransient<CreateNeuralNetworkUseCase>();
                services.AddTransient<SearchNeuralNetworkByNameUseCase>();

                services.AddTransient<ISearchDataSetsListByNameUseCase,SearchDataSetsListByNameUseCase>();
                services.AddTransient<IGetDataSetsListByNameUseCase,GetDataSetsListByNameUseCase>();
                services.AddTransient<ICreateDataSetsListUseCase,CreateDataSetsListUseCase>();
                
                services.AddTransient<IGetNeuralNetworkCalculationUseCase,GetNeuralNetworkCalculationUseCase>();
                
                services.AddTransient<ITrainNeuralNetworkUseCase,TrainNeuralNetworkUseCase>();

        }
}