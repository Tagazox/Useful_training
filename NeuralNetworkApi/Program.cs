using Useful_training.Applicative.Application.Adapter;
using Useful_training.Applicative.NeuralNetworkApi;
using Useful_training.Applicative.NeuralNetworkApi.Hubs;
using Useful_training.Presentation.NeuralNetworkApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("https://localhost:44331")
                .AllowAnyHeader()
                .WithMethods("GET", "POST")
                .AllowCredentials();
        });
});
builder.Services.AddSignalR();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddNeuralNetworkApplication(InternalResources.NeuralNetworkWarehouseRootPath,InternalResources.DatasetListWarehouseRootPath);


WebApplication app = builder.Build();

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