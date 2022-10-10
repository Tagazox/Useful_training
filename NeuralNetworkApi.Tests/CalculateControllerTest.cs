using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Useful_training.Applicative.Application.UseCases.Calculation.Get.ViewModels;
using Useful_training.Applicative.Application.UseCases.NeuralNetworks.Create.ViewModels;

namespace Useful_training.Applicative.NeuralNetworkApi.Tests;

public class CalculateControllerTest
{
    private readonly HttpClient _httpClient;
    private readonly string _testName;
    private readonly string _rootUrl;

    public CalculateControllerTest()
    {
        _rootUrl = "Calculate";
        _testName = Guid.NewGuid().ToString();

        WebApplicationFactory<Program> application =
            new WebApplicationFactory<Program>().WithWebHostBuilder(_ => { });
            
        _httpClient = application.CreateClient();
    }

    private Task<NeuralNetworkCreatedViewModel?> CreateNeuralNetwork()
    {
        string url =
            $"NeuralNetwork/{Method.Post}?Name={_testName}&numberOfInput=10&numberOfOutputs=1&numberOfHiddenLayer=3&numberOfNeuronByHiddenLayer=3&learnRate=0.05&momentum=0.05&typeOfNeuron=6";

        return HttpRequestHandler.SendRequestsToTheApiAndParseResponse<NeuralNetworkCreatedViewModel>(_httpClient,
            url,
            Method.Post);
    }

    [Fact]
    public async Task CalculateGetShouldBeOk()
    {
        NeuralNetworkCreatedViewModel? neuralNetworkCreated = await CreateNeuralNetwork();
        string url =
            $"{_rootUrl}/{Method.Get}/{neuralNetworkCreated?.FinalNameOfTheNeuralNetwork}/%20?Inputs=1&Inputs=0&Inputs=0&Inputs=0&Inputs=0&Inputs=0&Inputs=0&Inputs=0&Inputs=0&Inputs=0";

        GetNeuralNetworkCalculationViewModel? viewModel = await
            HttpRequestHandler.SendRequestsToTheApiAndParseResponse<GetNeuralNetworkCalculationViewModel>(
                _httpClient, url,
                Method.Get);

        viewModel?.InputsOfTheCalculation.Should().HaveCount(10);
        viewModel?.ResultsOfTheCalculation.Should().HaveCount(1);
    }

    [Fact]
    public async Task CalculateGetShouldThrow404()
    {
        string nonExtantTestName = Guid.NewGuid().ToString();
        string url = $"{_rootUrl}/{Method.Get}/{nonExtantTestName}/%20?Inputs=1&Inputs=0";

        HttpResponseMessage httpResponseMessage =
            await HttpRequestHandler.SendRequestsToTheApi(_httpClient, url, Method.Get);

        httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}