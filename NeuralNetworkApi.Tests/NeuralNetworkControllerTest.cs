using System.Net;
using static System.Guid;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Useful_training.Applicative.Application.UseCases.NeuralNetworks.Create.ViewModels;
using Useful_training.Applicative.Application.UseCases.NeuralNetworks.Get.ViewModels;

namespace Useful_training.Applicative.NeuralNetworkApi.Tests;

public class NeuralNetworkControllerTest
{
    private readonly HttpClient _httpClient;
    private readonly string _testName;
    private readonly string _rootUrl;

    public NeuralNetworkControllerTest()
    {
        _rootUrl = "NeuralNetwork";
        WebApplicationFactory<Program> application = new WebApplicationFactory<Program>().WithWebHostBuilder(_ => { });
        _httpClient = application.CreateClient();
        _testName = NewGuid().ToString();
    }

    [Fact]
    public async Task PostNeuralNetworkShouldBeOk()
    {
        string url =
            $"{_rootUrl}/{Method.Post}?Name={_testName}&numberOfInput=10&numberOfOutputs=1&numberOfHiddenLayer=3&numberOfNeuronByHiddenLayer=3&learnRate=0.05&momentum=0.05&typeOfNeuron=6";

        NeuralNetworkCreatedViewModel? viewModel = await
            HttpRequestHandler.SendRequestsToTheApiAndParseResponse<NeuralNetworkCreatedViewModel>(_httpClient, url,
                Method.Post);

        viewModel?.FinalNameOfTheNeuralNetwork.Should().Contain(_testName);
    }

    [Fact]
    public async Task PostNeuralNetworkShouldThrow500()
    {
        string urlWithBadMomentum =
            $"{_rootUrl}/{Method.Post}?Name={_testName}&numberOfInput=2&numberOfOutputs=1&numberOfHiddenLayer=3&numberOfNeuronByHiddenLayer=3&learnRate=0.05&momentum=-0.05&typeOfNeuron=6";

        HttpResponseMessage httpResponseMessage = await
            HttpRequestHandler.SendRequestsToTheApi(_httpClient, urlWithBadMomentum,
                Method.Post);

        httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task SearchShouldBeOk()
    {
        string url = $"{_rootUrl}/{Method.Get}/{_testName}/0/10";
        await PostNeuralNetworkShouldBeOk();
        NeuralNetworksFoundViewModel? viewModel = await
            HttpRequestHandler.SendRequestsToTheApiAndParseResponse<NeuralNetworksFoundViewModel>(_httpClient, url,
                Method.Get);

        viewModel.Should().NotBeNull();
        if (viewModel != null)
        {
            viewModel.NamesList.Should().NotBeNullOrEmpty();
            viewModel.NamesList.Count().Should().BeGreaterThan(0);
            viewModel.NamesList.Any(s => s.Contains(_testName)).Should().BeTrue();
        }
    }
}