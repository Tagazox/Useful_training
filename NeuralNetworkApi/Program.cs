using Useful_training.Core.NeuralNetwork;
using Useful_training.Core.NeuralNetwork.Interfaces;
using Useful_training.Infrastructure.FileManager;
using Useful_training.Applicative.NeuralNetworkApi;
using Useful_training.Applicative.NeuralNetworkApi.Hubs;

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
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<INeuralNetworkWarehouse>(NeuralNetworkWarehouseProvider => new NeuralNetworkFileWarehouse(InternalResources.NeuralNetworkWarehouseRootPath));
builder.Services.AddSingleton<IDataSetsListWarehouse>(DataSetsListWarehouseProvider => new DataSetsListFileWarehouse(InternalResources.DatasetListWarehouseRootPath));
builder.Services.AddSingleton<INeuralNetworkBuilder>(NeuralNetworkBuilderProvider => new NeuralNetworkBuilder());

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
