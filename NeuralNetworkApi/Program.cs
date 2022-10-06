using Useful_training.Applicative.Application.UseCases.DataSetsLists.Create;
using Useful_training.Applicative.Application.UseCases.DataSetsLists.Get;
using Useful_training.Applicative.Application.UseCases.DataSetsLists.Get.Interfaces;
using Useful_training.Applicative.NeuralNetworkApi;
using Useful_training.Applicative.NeuralNetworkApi.Hubs;
using Useful_training.Applicative.Application.UseCases.NeuralNetworks.Create;
using Useful_training.Applicative.Application.UseCases.NeuralNetworks.Get;
using Useful_training.Core.NeuralNetwork.Factory;
using Useful_training.Core.NeuralNetwork.Factory.Interfaces;
using Useful_training.Core.NeuralNetwork.Warehouse.Interfaces;
using Useful_training.Infrastructure.FileManager.Warehouse;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("https://localhost:7096")
                .AllowAnyHeader()
                .WithMethods("GET", "POST")
                .AllowCredentials();
        });
});
builder.Services.AddSignalR();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<INeuralNetworkWarehouse>(neuralNetworkWarehouseProvider =>
    new NeuralNetworkFileWarehouse(InternalResources.NeuralNetworkWarehouseRootPath));
builder.Services.AddSingleton<IDataSetsListWarehouse>(dataSetsListWarehouseProvider =>
    new DataSetsListFileWarehouse(InternalResources.DatasetListWarehouseRootPath));
builder.Services.AddSingleton<INeuralNetworkBuilder, NeuralNetworkBuilder>();
builder.Services.AddTransient<CreateNeuralNetworkUseCase>();
builder.Services.AddTransient<SearchNeuralNetworkByNameUseCase>();
builder.Services.AddTransient<ISearchDataSetsListByNameUseCase,SearchDataSetsListByNameUseCase>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler("/error");
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors();

app.MapControllers();
app.MapHub<NeuralNetworkTrainingHub>("/NeuralNetworkTraining");

app.Run();