using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Useful_training.Applicative.Application.UseCases.Calculation.Get.ViewModels;
using Useful_training.Applicative.Application.UseCases.NeuralNetworks.Create.ViewModels;

namespace Useful_training.Applicative.NeuralNetworkApi.Tests
{
    public class CalculateControllerTest
    {
        private readonly HttpClient HttpClient;
        private readonly string TestName;
        private readonly string RootUrl;

        public CalculateControllerTest()
        {
            RootUrl = "Calculate";
            TestName = Guid.NewGuid().ToString();

            WebApplicationFactory<Program> application =
                new WebApplicationFactory<Program>().WithWebHostBuilder(_ => { });
            
            HttpClient = application.CreateClient();
        }

        private Task<NeuralNetworkCreatedViewModel?> CreateNeuralNetwork()
        {
            string url =
                $"NeuralNetwork/{Method.POST}?Name={TestName}&numberOfInput=2&numberOfOutputs=1&numberOfHiddenLayer=3&numberOfNeuronByHiddenLayer=3&learnRate=0.05&momentum=0.05&typeOfNeuron=6";

            return HttpRequestHandler.SendRequestsToTheApiAndParseResponse<NeuralNetworkCreatedViewModel>(HttpClient,
                url,
                Method.POST);
        }

        [Fact]
        public async Task CalculateGetShouldBeOk()
        {
            NeuralNetworkCreatedViewModel? neuralNetworkCreated = await CreateNeuralNetwork();
            string url =
                $"{RootUrl}/{Method.GET}/{neuralNetworkCreated?.FinalNameOfTheNeuralNetwork}/%20?Inputs=1&Inputs=0";

            GetNeuralNetworkCalculationViewModel? viewModel = await
                HttpRequestHandler.SendRequestsToTheApiAndParseResponse<GetNeuralNetworkCalculationViewModel>(
                    HttpClient, url,
                    Method.GET);

            viewModel?.InputsOfTheCalculation.Should().HaveCount(2);
            viewModel?.ResultsOfTheCalculation.Should().HaveCount(1);
        }

        [Fact]
        public async Task CalculateGetShouldThrow404()
        {
            string nonExtantTestName = Guid.NewGuid().ToString();
            string url = $"{RootUrl}/{Method.GET}/{nonExtantTestName}/%20?Inputs=1&Inputs=0";

            HttpResponseMessage httpResponseMessage =
                await HttpRequestHandler.SendRequestsToTheApi(HttpClient, url, Method.GET);

            httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}