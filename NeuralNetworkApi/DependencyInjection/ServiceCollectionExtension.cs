using Useful_training.Applicative.Application.Ports;
using Useful_training.Applicative.Application.UseCases.Calculation.Get;
using Useful_training.Applicative.Application.UseCases.Calculation.Get.Interfaces;
using Useful_training.Applicative.Application.UseCases.DataSetsLists.Create;
using Useful_training.Applicative.Application.UseCases.DataSetsLists.Create.Interfaces;
using Useful_training.Applicative.Application.UseCases.DataSetsLists.Get;
using Useful_training.Applicative.Application.UseCases.DataSetsLists.Get.Interfaces;
using Useful_training.Applicative.Application.UseCases.NeuralNetworks.Create;
using Useful_training.Applicative.Application.UseCases.NeuralNetworks.Create.Interfaces;
using Useful_training.Applicative.Application.UseCases.NeuralNetworks.Get;
using Useful_training.Applicative.Application.UseCases.NeuralNetworks.Get.Interfaces;
using Useful_training.Applicative.Application.UseCases.Training.Hub;
using Useful_training.Applicative.Application.UseCases.Training.Interfaces;
using Useful_training.Core.NeuralNetwork.Factory;
using Useful_training.Core.NeuralNetwork.Factory.Interfaces;
using Useful_training.Infrastructure.FileManager.Warehouse;

namespace Useful_training.Applicative.NeuralNetworkApi.DependencyInjection;

public static class ServiceCollectionExtension
{
        public static void AddNeuralNetworkServices(this IServiceCollection services,string neuralNetworkWarehouseRootPath, string datasetListWarehouseRootPath)
        {
                services.AddSingleton<INeuralNetworkWarehouse>(new NeuralNetworkFileWarehouse(neuralNetworkWarehouseRootPath));
                services.AddSingleton<IDataSetsListWarehouse>(new DataSetsListFileWarehouse(datasetListWarehouseRootPath));
                services.AddSingleton<INeuralNetworkBuilder, NeuralNetworkBuilder>();

                services.AddTransient<ICreateNeuralNetworkUseCase,CreateNeuralNetworkUseCase>();
                services.AddTransient<ISearchNeuralNetworkByNameUseCase,SearchNeuralNetworkByNameUseCase>();

                services.AddTransient<ISearchDataSetsListByNameUseCase,SearchDataSetsListByNameUseCase>();
                services.AddTransient<IGetDataSetsListByNameUseCase,GetDataSetsListByNameUseCase>();
                services.AddTransient<ICreateDataSetsListUseCase,CreateDataSetsListUseCase>();
                
                services.AddTransient<IGetNeuralNetworkCalculationUseCase,GetNeuralNetworkCalculationUseCase>();
                
                services.AddTransient<ITrainNeuralNetworkUseCase,TrainNeuralNetworkUseCase>();

        }
}